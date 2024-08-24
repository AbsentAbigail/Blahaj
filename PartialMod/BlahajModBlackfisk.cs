using Deadpan.Enums.Engine.Components.Modding;

namespace Blahaj
{
    public partial class BlahajMod
    {
        private string UnitBlavingadOctopus => "Blackfisk";
        private string StatusInstantIncreaseCounter => "Instant Increase Current Counter";

        private void AddBlackfiskStatusEffects()
        {
            assets.Add(new StatusEffectDataBuilder(this)
                .Create<StatusInstantIncreaseCounter>(StatusInstantIncreaseCounter)
                .WithStackable(true)
                .WithCanBeBoosted(true)
                .WithText("Count <keyword=counter> up by <{a}>"));
        }

        private void AddBlackfiskCard()
        {
            assets.Add(
                new CardDataBuilder(this)
                    .CreateUnit(UnitBlavingadOctopus, "Bläckfisk")
                    .SetSprites("Blackfisk.png", "ValBG.png")
                    .SetStats(8, 0, 8)
                    .WithCardType("Friendly")
                    .WithBloodProfile("Blood Profile Blue (x2)")
                    .AddPool("GeneralUnitPool")
                    .WithFlavour("8 arms to give 8 times better hugs!")
                    .WithValue(50)
                    .SetTraits(
                        TStack("Pull", 1),
                        TStack("Aimless", 1)
                    )
                    .SetStartWithEffect(
                        SStack("MultiHit", 7)
                    )
                    .SubscribeToAfterAllBuildEvent(data =>
                    {
                        data.attackEffects = new CardData.StatusEffectStacks[]
                        {
                            SStack(StatusInstantIncreaseCounter, 1)
                        };

                        data.greetMessages = new string[]
                        {
                            "The octopus is a truly unique marine animal with its 8 arms and the ability to camouflage itself. Imagine all the exciting adventures your child can experience with such a companion by their side.",
                            "8 arms to give 8 times better hugs!"
                        };
                    })
            );
        }
    }
}