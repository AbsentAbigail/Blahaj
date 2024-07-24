using Deadpan.Enums.Engine.Components.Modding;
using System.Collections.Generic;

namespace BlahajCard
{
    public class BlahajMod : WildfrostMod
    {
        public BlahajMod(string modDirectory) : base(modDirectory)
        {
        }

        public override string GUID => "absentabigail.wildfrost.blahaj";

        public override string[] Depends => new string[0];

        public override string Title => "Blahaj";

        public override string Description => "Add your favourite Ikea plushie to the game!";

        protected override void Load()
        {
            base.Load();
            Events.OnCardDataCreated += BigBooshu;
        }

        protected override void Unload()
        {
            base.Unload();
            Events.OnCardDataCreated -= BigBooshu;
        }


        private void BigBooshu(CardData card)
        {
            if (card.name != "BerryPet")
                return;

            card.hp = 10;
            card.startWithEffects = CardData.StatusEffectStacks.Stack(card.startWithEffects, new CardData.StatusEffectStacks[1]
                    {
                        new CardData.StatusEffectStacks( Get<StatusEffectData>("MultiHit"), 1)
                    });
        }

        private List<CardDataBuilder> AddCards()
        {
            var list = new List<CardDataBuilder>
            {
                new CardDataBuilder(this)
                    .CreateUnit("blahaj", "Blahaj")
                    .SetStats(6, null, 2)
                    .SetSprites("blahaj.png", "blahajBG.png")
                    .SetStartWithEffect(new CardData.StatusEffectStacks(Get<StatusEffectData>("On Turn Heal Allies"), 3)),
            };

            return list;
        }
    }
}
