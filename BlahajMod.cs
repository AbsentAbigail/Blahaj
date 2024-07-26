using Blahaj.Calm;
using Deadpan.Enums.Engine.Components.Modding;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static StatusEffectApplyX;

namespace Blahaj
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

        private string CalmKeyword => $"<keyword={GUID}.calm>";
        private string StatusCalm => "Calm";
        private string StatusApplyCalmToAllyInFront => "On Turn Apply Calm To Ally In Front";
        private string StatusHealAllyInFront => "On Turn Heal Ally in Front";

        private List<CardDataBuilder> cards;                 //The list of custom CardData(Builder)
        private List<StatusEffectDataBuilder> statusEffects; //The list of custom StatusEffectData(Builder)
        private List<KeywordDataBuilder> keywords;
        private bool preLoaded = false;                      //Used to prevent redundantly reconstructing our data. Not truly necessary.

        private void CreateModAssets()
        {
            statusEffects = new List<StatusEffectDataBuilder>()
            {
                new StatusEffectDataBuilder(this)
                    .Create<StatusEffectCalm>(StatusCalm)
                    .WithCanBeBoosted(false)
                    //.WithIconGroupName("health")
                    .WithText("<{a}> Calm")
                    .WithIsStatus(true)
                    .WithStackable(true)
                    .WithVisible(true)
                    .FreeModify<StatusEffectCalm>(delegate(StatusEffectCalm data)
                    {
                        data.applyToFlags = ApplyToFlags.Self;
                        data.whenAppliedTypes = new string[1] { StatusCalm };
                        data.effectToApply = Get<StatusEffectData>("Reduce Max Counter");
                        data.applyEqualAmount = false;
                    }),

                new StatusEffectDataBuilder(this)
                    .Create<StatusEffectApplyXOnTurn>(StatusApplyCalmToAllyInFront)
                    .WithCanBeBoosted(true)
                    .WithText($"Apply <{{a}}> {CalmKeyword} to ally in front.")
                    .WithStackable(true)
                    .WithType("")
                    .FreeModify<StatusEffectApplyXOnTurn>(delegate(StatusEffectApplyXOnTurn data)
                    {
                        data.applyToFlags = ApplyToFlags.AllyInFrontOf;
                        data.effectToApply = Get<StatusEffectData>(StatusCalm);
                    }),

                new StatusEffectDataBuilder(this)
                    .Create<StatusEffectApplyXOnTurn>(StatusHealAllyInFront)
                    .WithCanBeBoosted(true)
                    .WithText($"Restore <{{a}}><keyword=health> to ally in front.")
                    .WithStackable(true)
                    .WithType("heal")
                    .FreeModify<StatusEffectApplyXOnTurn>(delegate(StatusEffectApplyXOnTurn data)
                    {
                        data.applyToFlags = ApplyToFlags.AllyInFrontOf;
                        data.effectToApply = Get<StatusEffectData>("Heal (No Ping)");
                    }),
            };
            LogHelper.Log("Status effects added");

            cards = new List<CardDataBuilder>
            {
                new CardDataBuilder(this)
                    .CreateUnit("blahaj", "Blahaj")
                    .SetSprites("blahaj.png", "blahajBG.png")
                    .SetStats(6, null, 2)
                    .WithCardType("Friendly")
                    .WithBloodProfile("Blood Profile Pink Wisp")
                    .AddPool("GeneralUnitPool")
                    .SubscribeToAfterAllBuildEvent(
                        delegate (CardData card)
                        {
                            card.startWithEffects = new CardData.StatusEffectStacks[2]
                            {
                                SStack(StatusHealAllyInFront, 1),
                                SStack(StatusApplyCalmToAllyInFront, 1)
                            };
                        }),
            };
            LogHelper.Log("Blahaj added");

            keywords = new List<KeywordDataBuilder>()
            {
                new KeywordDataBuilder(this)
                .Create("calm")                               //The internal name for the upgrade.
                .WithTitle("Calm")                            //The in-game name for the upgrade.
                .WithTitleColour(new Color(0.85f, 0.44f, 0.85f)) //Light purple on the title of the keyword pop-up
                .WithShowName(true)                              //Shows name in Keyword box (as opposed to a nonexistant icon).
                .WithDescription($"Reduce <keyword=counter> by one after gaining three {CalmKeyword}.|Removed upon activating") //Format is body|note.
                .WithNoteColour(new Color(0.85f, 0.44f, 0.85f))  //Somewhat teal
                .WithBodyColour(new Color(0.2f, 0.5f, 0.5f))       //Cyan-ish
                .WithCanStack(true),
                
            };
            LogHelper.Log("Keywords added");

            preLoaded = true;
        }

        protected override void Load()
        {
            if (!preLoaded)
            {
                CreateModAssets();

            }
            base.Load();
        }

        protected override void Unload()
        {
            base.Unload();
        }

        public override List<T> AddAssets<T, Y>()           //This method is called 6-7 times in base.Load() for each Builder. Can you name them all?
        {
            var typeName = typeof(Y).Name;
            switch (typeName)                                //Checks what the current builder is
            {
                case nameof(CardData):
                    return cards.Cast<T>().ToList();         //Loads our cards
                case nameof(StatusEffectData):
                    return statusEffects.Cast<T>().ToList(); //Loads our status effects
                case nameof(KeywordData):
                    return keywords.Cast<T>().ToList();

                default:
                    return null;
            }
        }

        private CardData.StatusEffectStacks SStack(string name, int amount) => new CardData.StatusEffectStacks(Get<StatusEffectData>(name), amount);

        private StatusEffectDataBuilder StatusCopy(string oldName, string newName)
        {
            StatusEffectData data = Get<StatusEffectData>(oldName);
            data.name = newName;
            StatusEffectDataBuilder builder = data.Edit<StatusEffectData, StatusEffectDataBuilder>();
            builder.Mod = this;
            return builder;
        }
    }
}