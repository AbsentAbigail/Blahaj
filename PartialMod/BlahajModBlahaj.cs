using Deadpan.Enums.Engine.Components.Modding;
using static StatusEffectApplyX;

namespace Blahaj
{
    public partial class BlahajMod
    {
        private string UnitBlahaj => "Blahaj";
        private string StatusCalm => "Calm";
        private string StatusApplyCalmToAllyInFront => "On Turn Apply Calm To AllyInFrontOf";
        private string StatusOnKillApplyCalmToSelf => "On Kill Apply Calm To Self";
        private string KeywordCalm => "calm";
        private string KeywordTagCalm => "<keyword=" + GUID + "." + KeywordCalm + ">";

        private void AddBlahajStatusEffects()
        {
            assets.Add(new StatusEffectDataBuilder(this)
                .Create<StatusCalm>(StatusCalm)
                .WithCanBeBoosted(false)
                .WithText("{a} " + KeywordTagCalm)
                .WithType("")
                .WithStackable(true)
                .FreeModify(data =>
                {
                    var realData = data as StatusCalm;

                    realData.applyToFlags = ApplyToFlags.Self;
                    realData.effectToApply = Get<StatusEffectData>("Reduce Max Counter");
                    realData.effectToApply2 = Get<StatusEffectData>("Increase Max Counter");
                    realData.applyEqualAmount = true;
                    realData.eventPriority = -10;
                }));

            assets.Add(new StatusEffectDataBuilder(this)
                .Create<StatusEffectApplyXOnTurn>(StatusApplyCalmToAllyInFront)
                .WithCanBeBoosted(true)
                .WithText("Apply <{a}> " + KeywordTagCalm + " to ally in front")
                .WithType("")
                .WithStackable(true)
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    var realData = data as StatusEffectApplyXOnTurn;

                    realData.applyToFlags = ApplyToFlags.AllyInFrontOf;
                    realData.effectToApply = Get<StatusEffectData>(StatusCalm);
                }));

            assets.Add(new StatusEffectDataBuilder(this)
                .Create<StatusEffectApplyXOnKill>(StatusOnKillApplyCalmToSelf)
                .WithCanBeBoosted(false)
                .WithText("Gain {a} " + KeywordTagCalm + " on kill")
                .WithType("")
                .WithStackable(true)
                .SubscribeToAfterAllBuildEvent(data =>
                {
                    var realData = data as StatusEffectApplyXOnKill;

                    realData.applyToFlags = ApplyToFlags.Self;
                    realData.effectToApply = Get<StatusEffectData>(StatusCalm);
                }));
            LogHelper.Log("Blahaj effects added");
        }

        private void AddBlahajKeyword()
        {
            assets.Add(
                new KeywordDataBuilder(this)
                    .Create(KeywordCalm)
                    .WithTitle("Calm")
                    .WithTitleColour(Color(91, 206, 250)) // Light Blue
                    .WithShowName(true)
                    .WithDescription($"Reduce max <keyword=counter> for every three {KeywordTagCalm}|Halves when damaged")
                    .WithBodyColour(Color(245, 169, 184)) // Pink
                    .WithNoteColour(Color(255, 255, 255)) // White
            );
        }

        private void AddBlahajCard()
        {
            assets.Add(new CardDataBuilder(this)
                    .CreateUnit(UnitBlahaj, "Blåhaj")
                    .SetSprites("blahaj.png", "blahajBG.png")
                    .SetStats(12, null, 3)
                    .WithCardType("Friendly")
                    .WithBloodProfile("Blood Profile Pink Wisp")
                    .AddPool("GeneralUnitPool")
                    .WithFlavour("Accepts and loves you <3")
                    .WithValue(50)
                    .SubscribeToAfterAllBuildEvent(data =>
                    {
                        data.startWithEffects = new CardData.StatusEffectStacks[]
                        {
                            SStack(StatusApplyCalmToAllyInFront, 2)
                        };
                        data.greetMessages = new string[] {
                            "Big and safe to have by your side if you want to discover the world below the surface of the ocean. The blue shark can swim very far, dive really deep and hear noises from almost 250 metres away.",
                            "Accepts and loves you <3"
                        };
                    }));
            LogHelper.Log("Blahaj card added");
        }

        private void AddBlahajCharm()
        {
            assets.Add(
                new CardUpgradeDataBuilder(this)
                    .CreateCharm("CardUpgradeShark")
                    .WithImage("SharkCharm.png")
                    .WithTitle("Shark Charm")
                    .WithText("Gain 1 " + KeywordTagCalm + " on kill")
                    .WithTier(2)
                    .SetConstraints(new TargetConstraintMaxCounterMoreThan())
                    .SubscribeToAfterAllBuildEvent(data =>
                    {
                        data.effects = new CardData.StatusEffectStacks[]
                        {
                            SStack(StatusOnKillApplyCalmToSelf, 1)
                        };
                    })
            );
        }
    }
}