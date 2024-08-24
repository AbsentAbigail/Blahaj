using Deadpan.Enums.Engine.Components.Modding;
using UnityEngine;
using UnityEngine.UI;
using static StatusEffectApplyX;

namespace Blahaj
{
    public partial class BlahajMod
    {
        private string UnitBlavingadWhale => "Val";
        private string StatusOnHitEat => "On Hit Eat";
        private string StatusInstantEat => "Instant Eat";

        private void AddValStatusEffects()
        {
            assets.Add(
                new StatusEffectDataBuilder(this)
                    .Create<StatusInstantEatCard>(StatusInstantEat)
                    .SubscribeToAfterAllBuildEvent(data =>
                    {
                        var realData = data as StatusInstantEatCard;

                        realData.effectToApply = TryGet<StatusEffectData>("Kill");

                        realData.illegalEffects = new StatusEffectData[]
                        {
                            TryGet<StatusEffectData>("On Turn Escape To Self"),
                            TryGet<StatusEffectData>("Scrap")
                        };
                    })
            );

            assets.Add(
                new StatusEffectDataBuilder(this)
                    .Create<StatusOnHitEat>(StatusOnHitEat)
                    .WithCanBeBoosted(false)
                    .WithText("Eat and <keyword=absorb> targets with less <keyword=health> than my <keyword=attack>")
                    .WithType("")
                    .SubscribeToAfterAllBuildEvent(data =>
                    {
                        var realData = data as StatusEffectApplyX;

                        realData.applyToFlags = ApplyToFlags.FrontEnemy;
                        realData.effectToApply = TryGet<StatusEffectData>(StatusInstantEat);
                    })
            );
        }

        private void AddValCard()
        {
            assets.Add(
                new CardDataBuilder(this)
                    .CreateUnit(UnitBlavingadWhale, "Val")
                    .SetSprites("Val.png", "ValBG.png")
                    .SetStats(4, 4, 5)
                    .WithCardType("Friendly")
                    .WithBloodProfile("Blood Profile Blue (x2)")
                    .AddPool("GeneralUnitPool")
                    .WithFlavour("Has a zipper for a mouth!")
                    .WithValue(50)
                    .SubscribeToAfterAllBuildEvent(data =>
                    {
                        data.startWithEffects = new CardData.StatusEffectStacks[] {
                            SStack(StatusOnHitEat, 1),
                        };
                        data.greetMessages = new string[]
                        {
                            "Has a zipper for a mouth!",
                            "In the mouth of this big blue whale there is room for pajamas or a treasure. It’s because this soft animal is a true friend who can keep a secret, play and give hugs when needed."
                        };

                        data.scriptableImagePrefab = CreateScriptableCardImage<ValCardImage>("val");
                    })
            );
        }

        private T CreateScriptableCardImage<T>(string name) where T : ScriptableCardImage
        {
            // Create a new GameObject that will host the ScriptableImage
            var ghostObject = new GameObject(name, typeof(RectTransform), typeof(Image), typeof(T))
            {
                // HideAndDontSave so it doesn't get touched during gameplay, OR
                hideFlags = HideFlags.HideAndDontSave
            };

            // ensure the GameObject is kept in memory this session
            GameObject.DontDestroyOnLoad(ghostObject);

            // Set the GameObject's size to the card size
            ghostObject.GetComponent<RectTransform>().sizeDelta = new Vector2(3.8f, 5.7f);

            // The image will try to autofill to fit the RectTransform size
            ghostObject.GetComponent<Image>().preserveAspect = true;
            // This fixes the card being hoverable
            ghostObject.GetComponent<Image>().raycastTarget = false;

            return ghostObject.GetComponent<T>();
        }
    }
}