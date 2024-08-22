using Deadpan.Enums.Engine.Components.Modding;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using WildfrostHopeMod;
using static StatusEffectApplyX;

namespace Blahaj
{
    public class BlahajMod : WildfrostMod
    {
        public static BlahajMod instance;

        public BlahajMod(string modDirectory) : base(modDirectory)
        {
            instance = this;
        }

        public override string GUID => "absentabigail.wildfrost.blahaj";

        public override string[] Depends => new string[]
        {
            "hope.wildfrost.configs"
        };

        public override string Title => "Blahaj and Friends";

        public override string Description => "Add your favourite Ikea plushies to the game!";

        #region Blahaj Properties

        private string UnitBlahaj => "Blahaj";
        private string StatusCalm => "Calm";
        private string StatusApplyCalmToAllyInFront => "On Turn Apply Calm To AllyInFrontOf";
        private string StatusOnKillApplyCalmToSelf => "On Kill Apply Calm To Self";
        private string KeywordCalm => "calm";
        private string KeywordTagCalm => "<keyword=" + GUID + "." + KeywordCalm + ">";

        #endregion Blahaj Properties

        #region Blackfisk Properties

        private string UnitBlavingadOctopus => "Blackfisk";
        private string StatusInstantIncreaseCounter => "Instant Increase Current Counter";

        #endregion Blackfisk Properties

        #region Val Properties

        private string UnitBlavingadWhale => "Val";
        private string StatusOnHitEat => "On Hit Eat";
        private string StatusInstantEat => "Instant Eat";

        #endregion Val Properties

        #region Kramig Propertiies

        private string UnitKramig => "Kramig";
        private string StatusStress => "Stress";

        #endregion Kramig Propertiies

        #region Aftonsparv Propertiies

        private string UnitAftonsparv => "Aftonsparv";
        private string ItemUFO => "UFO";
        private string StatusHealLowestHealthAlly => "Heal lowest health ally";
        private string StatusAbducted => "Abducted";
        private string StatusSummonUFO => "Summon UFO";
        private string StatusInstantSummonUFOInHand => "Instant Summon UFO In Hand";
        private string StatusOnTurnSummonUFOInHand => "On Turn Summon UFO In Hand";
        private string KeywordAbduct => "abduct";
        private string KeywordTagAbduct => "<keyword=" + GUID + "." + KeywordAbduct + ">";

        #endregion Aftonsparv Propertiies

        [ConfigInput]
        [ConfigSlider(0f, 10f)]
        [ConfigItem(0.25f, null, "Val min size")]
        public float valMinSize;

        [ConfigInput]
        [ConfigSlider(0f, 10f)]
        [ConfigItem(4f, null, "Val max size")]
        public float valMaxSize;

        [ConfigInput]
        [ConfigSlider(0, 100)]
        [ConfigItem(40, null, "Attack needed for max size")]
        public int valMaxSizeAt;

        private List<CardDataBuilder> cards;
        private List<StatusEffectDataBuilder> statusEffects;
        private List<KeywordDataBuilder> keywords;
        private List<CardUpgradeDataBuilder> cardUpgrades;
        private bool preLoaded = false;

        private void CreateModAssets()
        {
            statusEffects = new List<StatusEffectDataBuilder>()
            {
                #region Calm Status

                new StatusEffectDataBuilder(this)
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
                    }),

                new StatusEffectDataBuilder(this)
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
                    }),

                new StatusEffectDataBuilder(this)
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
                    }),

                #endregion Calm Status

                #region Bläckfisk

                new StatusEffectDataBuilder(this)
                    .Create<StatusInstantIncreaseCounter>(StatusInstantIncreaseCounter)
                    .WithStackable(true)
                    .WithCanBeBoosted(true)
                    .WithText("Count <keyword=counter> up by <{a}>"),

                #endregion Bläckfisk

                #region Val Status

                new StatusEffectDataBuilder(this)
                    .Create<StatusInstantEatCard>(StatusInstantEat)
                    .SubscribeToAfterAllBuildEvent(data =>
                    {
                        var realData = data as StatusInstantEatCard;

                        realData.effectToApply = TryGet<StatusEffectData>("Kill");

                        realData.illegalEffects = new StatusEffectData[2]
                        {
                            TryGet<StatusEffectData>("On Turn Escape To Self"),
                            TryGet<StatusEffectData>("Scrap")
                        };
                    }),

                new StatusEffectDataBuilder(this)
                    .Create<StatusOnHitEat>(StatusOnHitEat)
                    .WithCanBeBoosted (false)
                    .WithText("Eat and <keyword=absorb> targets with less <keyword=health> than my <keyword=attack>")
                    .WithType("")
                    .SubscribeToAfterAllBuildEvent(data =>
                    {
                        var realData = data as StatusEffectApplyX;

                        realData.applyToFlags = ApplyToFlags.FrontEnemy;
                        realData.effectToApply = TryGet<StatusEffectData>(StatusInstantEat);
                    }),

                #endregion Val Status

                #region Kramig

                new StatusEffectDataBuilder(this)
                    .Create<StatusStress>(StatusStress)
                    .WithCanBeBoosted(true)
                    .WithText("Deal <{a}> additional damage for each damaged ally")
                    .WithType("")
                    .WithStackable(true)
                    .FreeModify(data =>
                    {
                        (data as StatusStress).on = StatusEffectBonusDamageEqualToX.On.Board;
                    }),

                #endregion Kramig

                #region Aftonsparv

                    new StatusEffectDataBuilder(this)
                        .Create<StatusAbducted>(StatusAbducted)
                        .WithText("Apply " + KeywordTagAbduct)
                        .SubscribeToAfterAllBuildEvent(data =>
                        {
                            var realData = data as StatusAbducted;

                            realData.effectToApply = TryGet<StatusEffectData>("Snow");
                            realData.applyToFlags = ApplyToFlags.Self;
                        }),

                    StatusCopy("Summon Junk", StatusSummonUFO)
                        .SubscribeToAfterAllBuildEvent(data =>
                        {
                            (data as StatusEffectSummon).summonCard = TryGet<CardData>(ItemUFO);
                        }),

                    StatusCopy("Instant Summon Junk In Hand", StatusInstantSummonUFOInHand)
                        .SubscribeToAfterAllBuildEvent(data =>
                        {
                            (data as StatusEffectInstantSummon).targetSummon = TryGet<StatusEffectData>(StatusSummonUFO) as StatusEffectSummon;
                        }),

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
                        }),

                #endregion Aftonsparv
            };
            LogHelper.Log("Status Effects added");

            keywords = new List<KeywordDataBuilder>
            {
                new KeywordDataBuilder(this)
                    .Create(KeywordCalm)
                    .WithTitle("Calm")
                    .WithTitleColour(Color(91, 206, 250)) // Light Blue
                    .WithShowName(true)
                    .WithDescription($"Reduce max <keyword=counter> for every three {KeywordTagCalm}|Halves when damaged")
                    .WithBodyColour(Color(245, 169, 184)) // Pink
                    .WithNoteColour(Color(255, 255, 255)), // White

                
                new KeywordDataBuilder(this)
                    .Create(KeywordAbduct)
                    .WithTitle("Abducted")
                    .WithTitleColour(Color(74, 57, 148)) // Dark Blue
                    .WithShowName(true)
                    .WithDescription($"Untargetable and <keyword=snow>'d for one turn")
                    .WithBodyColour(Color(100, 67, 158)) // Light Purple
                    .WithNoteColour(Color(255, 255, 255)) // White
            };
            LogHelper.Log("Keywords added");

            cards = new List<CardDataBuilder>
            {
                #region Blahaj

                new CardDataBuilder(this)
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
                        data.startWithEffects = new CardData.StatusEffectStacks[1]
                        {
                            SStack(StatusApplyCalmToAllyInFront, 2)
                        };
                        data.greetMessages = new string[] {
                            "Big and safe to have by your side if you want to discover the world below the surface of the ocean. The blue shark can swim very far, dive really deep and hear noises from almost 250 metres away.",
                            "Accepts and loves you <3"
                        };
                    }),

                #endregion Blahaj
                
                #region Bläckfisk

                new CardDataBuilder(this)
                        .CreateUnit(UnitBlavingadOctopus, "Bläckfisk")
                        .SetSprites("Blackfisk.png", "ValBG.png")
                        .SetStats(8, 0, 8)
                        .WithCardType("Friendly")
                        .WithBloodProfile("Blood Profile Blue (x2)")
                        .AddPool("GeneralUnitPool")
                        .WithFlavour("8 arms to give 8 times better hugs!")
                        .WithValue(50)
                        .SetTraits(new CardData.TraitStacks[]
                        {
                            TStack("Pull", 1),
                            TStack("Aimless", 1),
                        })
                        .SetStartWithEffect(new CardData.StatusEffectStacks[]
                        {
                            SStack("MultiHit", 7)
                        })
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
                        }),

                #endregion Bläckfisk

                #region Val

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
                        }),

                #endregion Val

                #region Kramig

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
                    }),

                #endregion Kramig

                #region Aftonsparv
                
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
                    }),

                new CardDataBuilder(this)
                    .CreateItem(ItemUFO, "Rescue UFO")
                    .SetSprites("AftonsparvUFO.png", "AftonsparvBG.png")
                    .SetStats()
                    .SetTraits(TStack("Consume", 1), TStack("Zoomlin", 1))
                    .AddPool("GeneralItemPool")
                    .WithValue(40)
                    .SubscribeToAfterAllBuildEvent(data =>
                    {
                        data.attackEffects = new CardData.StatusEffectStacks[]
                        {
                            SStack(StatusAbducted, 1)
                        };
                    }),

                #endregion Aftonsparv
            };
            LogHelper.Log("Plushies added");

            cardUpgrades = new List<CardUpgradeDataBuilder>()
            {
                new CardUpgradeDataBuilder(this)
                    .CreateCharm("CardUpgradeShark")
                    .WithImage("SharkCharm.png")
                    .WithTitle("Shark Charm")
                    .WithText("Gain 1 " + KeywordTagCalm + " on kill")
                    .WithTier(2)
                    .SetConstraints(new TargetConstraintMaxCounterMoreThan())
                    .SubscribeToAfterAllBuildEvent(data =>
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
            LogHelper.Log("Adding assets for " + typeName);
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

        private T TryGet<T>(string name) where T : DataFile
        {
            T data;
            if (typeof(StatusEffectData).IsAssignableFrom(typeof(T)))
                data = base.Get<StatusEffectData>(name) as T;
            else
                data = base.Get<T>(name);

            if (data == null)
                throw new Exception($"TryGet Error: Could not find a [{typeof(T).Name}] with the name [{name}] or [{Extensions.PrefixGUID(name, this)}]");

            return data;
        }

        private CardData.StatusEffectStacks SStack(string name, int amount) => new CardData.StatusEffectStacks(TryGet<StatusEffectData>(name), amount);

        private CardData.TraitStacks TStack(string name, int amount) => new CardData.TraitStacks(TryGet<TraitData>(name), amount);

        private StatusEffectDataBuilder StatusCopy(string oldName, string newName)
        {
            StatusEffectData data = TryGet<StatusEffectData>(oldName).InstantiateKeepName();
            data.name = newName;
            data.ModAdded = this;
            StatusEffectDataBuilder builder = data.Edit<StatusEffectData, StatusEffectDataBuilder>()
                .FreeModify((_data) => { _data.ModAdded = null; });
            return builder;
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