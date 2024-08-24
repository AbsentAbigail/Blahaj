using Deadpan.Enums.Engine.Components.Modding;
using static StatusEffectApplyX;

namespace Blahaj
{
    public partial class BlahajMod
    {
        private string UnitAftonsparv => "Aftonsparv";
        private string ItemUFO => "UFO";
        private string StatusAbducted => "Abducted";
        private string StatusSummonUFO => "Summon UFO";
        private string StatusInstantSummonUFOInHand => "Instant Summon UFO In Hand";
        private string StatusOnTurnSummonUFOInHand => "On Turn Summon UFO In Hand";
        private string KeywordAbduct => "abduct";
        private string KeywordTagAbduct => "<keyword=" + GUID + "." + KeywordAbduct + ">";

        private void AddAftonsparvStatusEffects()
        {
            assets.Add(
                new StatusEffectDataBuilder(this)
                    .Create<StatusAbducted>(StatusAbducted)
                    .WithText("Apply " + KeywordTagAbduct)
                    .SubscribeToAfterAllBuildEvent(data =>
                    {
                        var realData = data as StatusAbducted;

                        realData.effectToApply = TryGet<StatusEffectData>("Snow");
                        realData.applyToFlags = ApplyToFlags.Self;
                    })
            );

            assets.Add(
                StatusCopy("Summon Junk", StatusSummonUFO)
                    .SubscribeToAfterAllBuildEvent(data =>
                    {
                        (data as StatusEffectSummon).summonCard = TryGet<CardData>(ItemUFO);
                    })
            );

            assets.Add(
                StatusCopy("Instant Summon Junk In Hand", StatusInstantSummonUFOInHand)
                    .SubscribeToAfterAllBuildEvent(data =>
                    {
                        (data as StatusEffectInstantSummon).targetSummon = TryGet<StatusEffectData>(StatusSummonUFO) as StatusEffectSummon;
                    })
            );

            assets.Add(
                new StatusEffectDataBuilder(this)
                    .Create<StatusEffectApplyXOnTurn>(StatusOnTurnSummonUFOInHand)
                    .WithText("Add <{a}> {0} to your hand")
                    .WithStackable(true)
                    .WithCanBeBoosted(true)
                    .WithTextInsert("<card=" + GUID + "." + ItemUFO + ">")
                    .SubscribeToAfterAllBuildEvent(data =>
                    {
                        var realData = data as StatusEffectApplyX;

                        realData.effectToApply = TryGet<StatusEffectData>(StatusInstantSummonUFOInHand);
                        realData.applyToFlags = ApplyToFlags.Self;
                    })
            );
        }

        private void AddAftonsparvKeyword()
        {
            assets.Add(
                new KeywordDataBuilder(this)
                    .Create(KeywordAbduct)
                    .WithTitle("Abducted")
                    .WithTitleColour(Color(74, 57, 148)) // Dark Blue
                    .WithShowName(true)
                    .WithDescription($"Untargetable and <keyword=snow>'d for one turn")
                    .WithBodyColour(Color(100, 67, 158)) // Light Purple
                    .WithNoteColour(Color(255, 255, 255)) // White
            );
        }

        private void AddAftonsparvCards()
        {
            assets.Add(
                new CardDataBuilder(this)
                    .CreateUnit(UnitAftonsparv, "Aftonsparv")
                    .SetSprites("Aftonsparv.png", "AftonsparvBG.png")
                    .SetStats(7, null, 3)
                    .WithCardType("Friendly")
                    .WithBloodProfile("Blood Profile Pink Wisp")
                    .AddPool("GeneralUnitPool")
                    .WithFlavour("Alien Friend")
                    .WithValue(50)
                    .SubscribeToAfterAllBuildEvent(data =>
                    {
                        data.startWithEffects = new CardData.StatusEffectStacks[]
                        {
                            SStack(StatusOnTurnSummonUFOInHand, 1)
                        };
                        data.greetMessages = new string[] {
                            "A soft alien is the best buddy to bring on an imaginary flight in space. What will your child get up to this time with their superhero?",
                            "Gnarp Gnarp from space"
                        };
                    })
            );

            assets.Add(
                new CardDataBuilder(this)
                    .CreateItem(ItemUFO, "Rescue UFO")
                    .SetSprites("AftonsparvUFO.png", "AftonsparvBG.png")
                    .SetStats()
                    .SetTraits(
                        TStack("Consume", 1),
                        TStack("Zoomlin", 1)
                    )
                    .AddPool("GeneralItemPool")
                    .WithValue(40)
                    .SubscribeToAfterAllBuildEvent(data =>
                    {
                        data.attackEffects = new CardData.StatusEffectStacks[]
                        {
                            SStack(StatusAbducted, 1)
                        };
                    })
            );
        }
    }
}