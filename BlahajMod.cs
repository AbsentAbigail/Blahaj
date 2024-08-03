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

        private string StatusCalm => "Calm";
        private string StatusApplyCalmToAllyInFront => "On Turn Apply Calm To AllyInFrontOf";
        private string StatusOnKillApplyCalmToSelf => "On Kill Apply Calm To Self";
        private string KeywordCalm => "calm";
        private string KeywordTagCalm => "<keyword=" + GUID + "." + KeywordCalm + ">";

        private List<CardDataBuilder> cards;
        private List<StatusEffectDataBuilder> statusEffects;
        private List<KeywordDataBuilder> keywords;
        private List<CardUpgradeDataBuilder> cardUpgrades;
        private bool preLoaded = false;

        private void CreateModAssets()
        {
            statusEffects = new List<StatusEffectDataBuilder>()
            {
                new StatusEffectDataBuilder(this)
                    .Create<StatusEffectCalm>(StatusCalm)
                    .WithCanBeBoosted(false)
                    .WithText("{a} " + KeywordTagCalm)
                    .WithType("")
                    .WithStackable(true)
                    .FreeModify((data) =>
                    {
                        ((StatusEffectCalm)data).applyToFlags = ApplyToFlags.Self;
                        ((StatusEffectCalm)data).effectToApply = Get<StatusEffectData>("Reduce Max Counter");
                        ((StatusEffectCalm)data).effectToApply2 = Get<StatusEffectData>("Increase Max Counter");
                        ((StatusEffectCalm)data).applyEqualAmount = true;
                    }),

                new StatusEffectDataBuilder(this)
                    .Create<StatusEffectApplyXOnTurn>(StatusApplyCalmToAllyInFront)
                    .WithCanBeBoosted(true)
                    .WithText("Apply <{a}> " + KeywordTagCalm + " to ally in front.")
                    .WithType("")
                    .WithStackable(true)
                    .SubscribeToAfterAllBuildEvent(delegate (StatusEffectData data)
                    {
                        ((StatusEffectApplyXOnTurn)data).applyToFlags = ApplyToFlags.AllyInFrontOf;
                        ((StatusEffectApplyXOnTurn)data).effectToApply = Get<StatusEffectData>(StatusCalm);
                    }),

                new StatusEffectDataBuilder(this)
                    .Create<StatusEffectApplyXOnKill>(StatusOnKillApplyCalmToSelf)
                    .WithCanBeBoosted(false)
                    .WithText("Gain {a} " + KeywordTagCalm + " on kill.")
                    .WithType("")
                    .WithStackable(true)
                    .SubscribeToAfterAllBuildEvent((data) => 
                    {
                        ((StatusEffectApplyXOnKill)data).applyToFlags = ApplyToFlags.Self;
                        ((StatusEffectApplyXOnKill)data).effectToApply = Get<StatusEffectData>(StatusCalm);
                    }),
            };
            LogHelper.Log("Status Effects added");

            keywords = new List<KeywordDataBuilder>
            {
                new KeywordDataBuilder(this)
                    .Create(KeywordCalm)
                    .WithTitle("Calm")
                    .WithTitleColour(Color(91, 206, 250)) // Light Blue
                    .WithShowName(true)
                    .WithDescription($"Reduce max <keyword=counter> for every three {KeywordTagCalm}.|Halves when damaged")
                    .WithBodyColour(Color(245, 169, 184)) // Pink
                    .WithNoteColour(Color(255, 255, 255)) // White
            };
            LogHelper.Log("Calm Keyword added");

            cards = new List<CardDataBuilder>
            {
                new CardDataBuilder(this)
                    .CreateUnit("blahaj", "Blahaj")
                    .SetSprites("blahaj.png", "blahajBG.png")
                    .SetStats(12, null, 3)
                    .WithCardType("Friendly")
                    .WithBloodProfile("Blood Profile Pink Wisp")
                    .AddPool("GeneralUnitPool")
                    .WithFlavour("Accepts and loves you <3")
                    .SubscribeToAfterAllBuildEvent((data) =>
                    {
                        data.startWithEffects = new CardData.StatusEffectStacks[1]
                        {
                            SStack(StatusApplyCalmToAllyInFront, 2)
                        };
                    })
            };
            LogHelper.Log("Blahaj added");

            cardUpgrades = new List<CardUpgradeDataBuilder>()
            {
                new CardUpgradeDataBuilder(this)
                    .CreateCharm("CardUpgradeShark")
                    .WithImage("SharkCharm.png")
                    .WithTitle("Shark Charm")
                    .WithText("Gain 1 " + KeywordTagCalm + " on kill.")
                    .WithTier(2)
                    .SetConstraints(new TargetConstraintMaxCounterMoreThan())
                    .SubscribeToAfterAllBuildEvent((data) =>
                    {
                        data.effects = new CardData.StatusEffectStacks[1] { SStack(StatusOnKillApplyCalmToSelf, 1) };
                    })
            };
            LogHelper.Log("Shark charm added");

            preLoaded = true;
        }

        public override void Load()
        {
            if (!preLoaded)
            {
                CreateModAssets();
            }
            base.Load();
        }

        public override void Unload()
        {
            base.Unload();
        }

        public override List<T> AddAssets<T, Y>()
        {
            var typeName = typeof(Y).Name;
            switch (typeName)
            {
                case nameof(CardData):
                    return cards.Cast<T>().ToList();

                case nameof(StatusEffectData):
                    return statusEffects.Cast<T>().ToList();

                case nameof(KeywordData):
                    return keywords.Cast<T>().ToList();

                case nameof(CardUpgradeData):
                    return cardUpgrades.Cast<T>().ToList();

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

        private Color Color(int r, int b, int g)
        {
            Color color = new Color(
                r / 255F,
                b / 255F,
                g / 255F
            );

            return color;
        }
    }
}