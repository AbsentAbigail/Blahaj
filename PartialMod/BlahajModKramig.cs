using Deadpan.Enums.Engine.Components.Modding;

namespace Blahaj
{
    public partial class BlahajMod
    {
        private string UnitKramig => "Kramig";
        private string StatusStress => "Stress";

        private void AddKramigStatusEffects()
        {
            assets.Add(
                new StatusEffectDataBuilder(this)
                    .Create<StatusStress>(StatusStress)
                    .WithCanBeBoosted(true)
                    .WithText("Deal <{a}> additional damage for each damaged ally")
                    .WithType("")
                    .WithStackable(true)
                    .FreeModify(data =>
                    {
                        (data as StatusStress).on = StatusEffectBonusDamageEqualToX.On.Board;
                    })
            );
        }

        private void AddKramigCard()
        {
            assets.Add(
                new CardDataBuilder(this)
                    .CreateUnit(UnitKramig, "Kramig")
                    .SetSprites("Kramig.png", "KramigBG.png")
                    .SetStats(8, 3, 4)
                    .WithCardType("Friendly")
                    .WithBloodProfile("Blood Profile Normal")
                    .AddPool("GeneralUnitPool")
                    .WithFlavour("Protects its friends")
                    .WithValue(50)
                    .SubscribeToAfterAllBuildEvent(data =>
                    {
                        data.startWithEffects = new CardData.StatusEffectStacks[]
                        {
                            SStack(StatusStress, 2)
                        };
                        data.greetMessages = new string[] {
                            "In the wild, an adult panda eats about 83 pounds of bamboo – every day! But this black and white softie doesn’t need any food, just a lot of love.",
                            "Protects its friends"
                        };
                    })
            );
        }
    }
}