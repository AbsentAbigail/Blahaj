using Deadpan.Enums.Engine.Components.Modding;
using FMODUnity;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.Localization.Tables;
using UnityEngine.Localization;
using UnityEngine;

namespace Blahaj
{
    public static class BattleDataHelpers
    {
        public static BattleData WithTitle(this BattleData dataFile, string title)
        {
            dataFile.title = title;
            return dataFile;
        }

        public static BattleData WithPointFactor(this BattleData dataFile, float factor = 1f)
        {
            dataFile.pointFactor = factor;
            return dataFile;
        }

        public static BattleData WithWaveCounter(this BattleData dataFile, int waveCounter = 4)
        {
            dataFile.waveCounter = waveCounter;
            return dataFile;
        }

        public static BattleData WithPools(this BattleData dataFile, params BattleWavePoolData[] pools)
        {
            dataFile.pools = pools;
            return dataFile;
        }

        public static BattleData WithBonusUnitPool(this BattleData dataFile, params CardData[] pools)
        {
            dataFile.bonusUnitPool = pools;
            return dataFile;
        }

        public static BattleData WithBonusUnitRange(this BattleData dataFile, Vector2Int v)
        {
            dataFile.bonusUnitRange = v;
            return dataFile;
        }

        public static BattleData WithGoldGiverPool(this BattleData dataFile, params CardData[] pools)
        {
            dataFile.goldGiverPool = pools;
            return dataFile;
        }

        public static BattleData WithGoldGivers(this BattleData dataFile, int amount = 1)
        {
            dataFile.goldGivers = amount;
            return dataFile;
        }

        public static BattleData WithGenerationScript(this BattleData dataFile, BattleGenerationScript s)
        {
            dataFile.generationScript = s;
            return dataFile;
        }

        public static BattleData WithSetUpScript(this BattleData dataFile, Script s)
        {
            dataFile.setUpScript = s;
            return dataFile;
        }

        public static BattleData WithSprite(this BattleData dataFile, Sprite sprite)
        {
            dataFile.sprite = sprite;
            return dataFile;
        }

        public static BattleData WithSprite(this BattleData dataFile, string sprite)
        {
            dataFile.sprite = new InternalMod(null).GetImageSprite(sprite);
            return dataFile;
        }

        public static BattleData WithName(this BattleData dataFile, string name, SystemLanguage lang = SystemLanguage.English)
        {
            StringTable collection = LocalizationHelper.GetCollection("Cards", new LocaleIdentifier(lang));
            collection.SetString(dataFile.name + "_ref", name);
            dataFile.nameRef = collection.GetString(dataFile.name + "_ref");
            return dataFile;
        }
    }
    public static class BuildingPlotTypeHelpers
    {
        public static BuildingPlotType WithIllegalBuildings(this BuildingPlotType dataFile, params BuildingType[] illegalBuildings)
        {
            dataFile.illegalBuildings = illegalBuildings;
            return dataFile;
        }
    }
    public static class BuildingTypeHelpers
    {
        public static BuildingType WithTitle(this BuildingType dataFile, LocalizedString title)
        {
            dataFile.titleKey = title;
            return dataFile;
        }

        public static BuildingType WithTitle(this BuildingType dataFile, string title, SystemLanguage lang = SystemLanguage.English)
        {
            StringTable collection = LocalizationHelper.GetCollection("Cards", new LocaleIdentifier(lang));
            collection.SetString(dataFile.name + "_building_title", title);
            dataFile.titleKey = collection.GetString(dataFile.name + "_building_title");
            return dataFile;
        }

        public static BuildingType WithHelp(this BuildingType dataFile, LocalizedString title)
        {
            dataFile.helpKey = title;
            return dataFile;
        }

        public static BuildingType WithHelp(this BuildingType dataFile, string title, SystemLanguage lang = SystemLanguage.English)
        {
            StringTable collection = LocalizationHelper.GetCollection("Cards", new LocaleIdentifier(lang));
            collection.SetString(dataFile.name + "_building_help", title);
            dataFile.helpKey = collection.GetString(dataFile.name + "_building_help");
            return dataFile;
        }

        public static BuildingType WithHelpEmoteType(this BuildingType dataFile, Prompt.Emote.Type helpEmoteType = Prompt.Emote.Type.Explain)
        {
            dataFile.helpEmoteType = helpEmoteType;
            return dataFile;
        }

        public static BuildingType WithStarted(this BuildingType dataFile, UnlockData started)
        {
            dataFile.started = started;
            return dataFile;
        }

        public static BuildingType WithFinished(this BuildingType dataFile, UnlockData finished)
        {
            dataFile.finished = finished;
            return dataFile;
        }

        public static BuildingType WithUnlocks(this BuildingType dataFile, params UnlockData[] unlocks)
        {
            dataFile.unlocks = unlocks;
            return dataFile;
        }

        public static BuildingType WithUnlockedCheckedKey(this BuildingType dataFile, string unlockedCheckedKey)
        {
            dataFile.unlockedCheckedKey = unlockedCheckedKey;
            return dataFile;
        }
    }
    public static class CampaignNodeTypeHelpers
    {
        public static CampaignNodeType WithLetter(this CampaignNodeType dataFile, string letter)
        {
            dataFile.letter = letter;
            return dataFile;
        }

        public static CampaignNodeType WithZoneName(this CampaignNodeType dataFile, string zoneName)
        {
            dataFile.zoneName = zoneName;
            return dataFile;
        }

        public static CampaignNodeType WithMustClear(this CampaignNodeType dataFile, bool mustClear)
        {
            dataFile.mustClear = mustClear;
            return dataFile;
        }

        public static CampaignNodeType WithCanSkip(this CampaignNodeType dataFile, bool canSkip)
        {
            dataFile.canSkip = canSkip;
            return dataFile;
        }

        public static CampaignNodeType WithCanEnter(this CampaignNodeType dataFile, bool canEnter)
        {
            dataFile.canEnter = canEnter;
            return dataFile;
        }

        public static CampaignNodeType WithIsBattle(this CampaignNodeType dataFile, bool isBattle)
        {
            dataFile.isBattle = isBattle;
            return dataFile;
        }

        public static CampaignNodeType WithIsBoss(this CampaignNodeType dataFile, bool isBoss)
        {
            dataFile.isBoss = isBoss;
            return dataFile;
        }

        public static CampaignNodeType WithModifierReward(this CampaignNodeType dataFile, bool modifierReward)
        {
            dataFile.modifierReward = modifierReward;
            return dataFile;
        }

        public static CampaignNodeType WithInteractable(this CampaignNodeType dataFile, bool interactable)
        {
            dataFile.interactable = interactable;
            return dataFile;
        }

        public static CampaignNodeType WithStartRevealed(this CampaignNodeType dataFile, bool startRevealed)
        {
            dataFile.startRevealed = startRevealed;
            return dataFile;
        }

        public static CampaignNodeType WithFinalNode(this CampaignNodeType dataFile, bool finalNode)
        {
            dataFile.finalNode = finalNode;
            return dataFile;
        }

        public static CampaignNodeType WithMapNodePrefab(this CampaignNodeType dataFile, MapNode mapNodePrefab)
        {
            dataFile.mapNodePrefab = mapNodePrefab;
            return dataFile;
        }

        public static CampaignNodeType WithMapNodeSprite(this CampaignNodeType dataFile, Sprite mapNodeSprite)
        {
            dataFile.mapNodeSprite = mapNodeSprite;
            return dataFile;
        }

        public static CampaignNodeType WithMapNodeSprite(this CampaignNodeType dataFile, string mapNodeSprite)
        {
            dataFile.mapNodeSprite = new InternalMod(null).GetImageSprite(mapNodeSprite);
            return dataFile;
        }

        public static CampaignNodeType WithSize(this CampaignNodeType dataFile, float size = 1f)
        {
            dataFile.size = size;
            return dataFile;
        }

        public static CampaignNodeType WithCanLink(this CampaignNodeType dataFile, bool canLink)
        {
            dataFile.canLink = canLink;
            return dataFile;
        }
    }
    public static class CardDataHelpers
    {

        public static CardData SetStats(this CardData dataFile, int? health = null, int? damage = null, int counter = 0)
        {
            return dataFile.SetHealth(health).SetDamage(damage).SetCounter(counter);
        }

        public static CardData SetCounter(this CardData dataFile, int counter)
        {
            dataFile.counter = counter;
            return dataFile;
        }

        public static CardData SetDamage(this CardData dataFile, int? damage)
        {
            if (damage.HasValue)
                dataFile.damage = damage.Value;
            dataFile.hasAttack = damage.HasValue;
            return dataFile;
        }

        public static CardData NeedsTarget(this CardData dataFile, bool value = true)
        {
            dataFile.needsTarget = value;
            return dataFile;
        }

        public static CardData SetHealth(this CardData dataFile, int? health)
        {
            if (health.HasValue)
                dataFile.hp = health.Value;
            dataFile.hasHealth = health.HasValue;
            return dataFile;
        }

        public static CardData SetSprites(this CardData dataFile, Sprite mainSprite, Sprite backgroundSprite)
        {
            dataFile.mainSprite = mainSprite;
            dataFile.backgroundSprite = backgroundSprite;
            return dataFile;
        }

        public static CardData SetSprites(this CardData dataFile, string mainSprite, string backgroundSprite)
        {
            return dataFile.SetSprites(new InternalMod(null).ImagePath(mainSprite).ToSprite(), new InternalMod(null).ImagePath(backgroundSprite).ToSprite());
        }

        public static CardData SetStartWithEffect(this CardData dataFile, params CardData.StatusEffectStacks[] stacks)
        {
            dataFile.startWithEffects = stacks;
            return dataFile;
        }

        public static CardData SetTraits(this CardData dataFile, params CardData.TraitStacks[] stacks)
        {
            dataFile.traits = stacks.ToList<CardData.TraitStacks>();
            return dataFile;
        }

        public static CardData WithDescription(this CardData dataFile, string desc)
        {
            dataFile.desc = desc;
            return dataFile;
        }

        public static CardData WithValue(this CardData dataFile, int price)
        {
            dataFile.value = price;
            return dataFile;
        }

        public static CardData WithTargetMode(this CardData dataFile, TargetMode mode)
        {
            dataFile.targetMode = mode;
            return dataFile;
        }

        public static CardData WithTargetMode(this CardData dataFile, string mode = "TargetModeBasic")
        {
            dataFile.targetMode = Extensions.GetTargetMode(mode);
            return dataFile;
        }

        public static CardData WithPlayType(this CardData dataFile, Card.PlayType type)
        {
            dataFile.playType = type;
            return dataFile;
        }

        public static CardData SetAttackEffect(this CardData dataFile, params CardData.StatusEffectStacks[] stacks)
        {
            dataFile.attackEffects = stacks;
            return dataFile;
        }

        public static CardData WithIdleAnimationProfile(this CardData dataFile, CardAnimationProfile bp)
        {
            dataFile.idleAnimationProfile = bp;
            return dataFile;
        }

        public static CardData WithIdleAnimationProfile(this CardData dataFile, string bp = "SwayAnimationProfile")
        {
            return dataFile.WithIdleAnimationProfile(Extensions.GetCardAnimationProfile(bp));
        }

        public static CardData WithBloodProfile(this CardData dataFile, BloodProfile bp)
        {
            dataFile.bloodProfile = bp;
            return dataFile;
        }

        public static CardData WithBloodProfile(this CardData dataFile, string bp = "Blood Profile Normal")
        {
            return dataFile.WithBloodProfile(new InternalMod(null).GetAsset<BloodProfile>(bp));
        }

        public static CardData CanPlayOnBoard(this CardData dataFile, bool value = true)
        {
            dataFile.canPlayOnBoard = value;
            return dataFile;
        }

        public static CardData CanPlayOnEnemy(this CardData dataFile, bool value = true)
        {
            dataFile.canPlayOnEnemy = value;
            return dataFile;
        }

        public static CardData CanPlayOnFriendly(this CardData dataFile, bool value = true)
        {
            dataFile.canPlayOnFriendly = value;
            return dataFile;
        }

        public static CardData CanPlayOnHand(this CardData dataFile, bool value = true)
        {
            dataFile.canPlayOnHand = value;
            return dataFile;
        }

        public static CardData CanBeHit(this CardData dataFile, bool value = true)
        {
            dataFile.canBeHit = value;
            return dataFile;
        }

        public static CardData CanShoveToOtherRow(this CardData dataFile, bool value = true)
        {
            dataFile.canShoveToOtherRow = value;
            return dataFile;
        }
        public static CardData WithCardType(this CardData dataFile, CardType type)
        {
            dataFile.cardType = type;
            return dataFile;
        }

        public static CardData WithCardType(this CardData dataFile, string type = "Friendly")
        {
            dataFile.cardType = new InternalMod(null).Get<CardType>(type);
            return dataFile;
        }

        public static CardData WithTitle(this CardData dataFile, LocalizedString title)
        {
            dataFile.titleKey = title;
            return dataFile;
        }

        public static CardData WithFlavour(this CardData dataFile, LocalizedString flavour)
        {
            dataFile.flavourKey = flavour;
            return dataFile;
        }

        public static CardData WithText(this CardData dataFile, LocalizedString text)
        {
            dataFile.textKey = text;
            return dataFile;
        }

        public static CardData WithTitle(this CardData dataFile, string title, SystemLanguage lang = SystemLanguage.English)
        {
            StringTable collection = LocalizationHelper.GetCollection("Cards", new LocaleIdentifier(lang));
            collection.SetString(dataFile.name + "_title", title);
            dataFile.titleKey = collection.GetString(dataFile.name + "_title");
            return dataFile;
        }

        public static CardData WithFlavour(this CardData dataFile, string flavour, SystemLanguage lang = SystemLanguage.English)
        {
            StringTable collection = LocalizationHelper.GetCollection("Cards", new LocaleIdentifier(lang));
            collection.SetString(dataFile.name + "_flavour", flavour);
            dataFile.flavourKey = collection.GetString(dataFile.name + "_flavour");
            return dataFile;
        }

        public static CardData WithText(this CardData dataFile, string text, SystemLanguage lang = SystemLanguage.English)
        {
            StringTable collection = LocalizationHelper.GetCollection("Cards", new LocaleIdentifier(lang));
            collection.SetString(dataFile.name + "_text", text);
            dataFile.textKey = collection.GetString(dataFile.name + "_text");
            return dataFile;
        }
    }
    public static class CardTypeHelpers
    {
        public static CardType WithSortPriority(this CardType dataFile, int sortPriority)
        {
            dataFile.sortPriority = sortPriority;
            return dataFile;
        }

        public static CardType WithIcon(this CardType dataFile, Sprite icon)
        {
            dataFile.icon = icon;
            return dataFile;
        }

        public static CardType WithIcon(this CardType dataFile, string icon)
        {
            dataFile.icon = new InternalMod(null).GetImageSprite(icon);
            return dataFile;
        }

        public static CardType WithPrefabRef(this CardType dataFile, AssetReference prefabRef)
        {
            dataFile.prefabRef = prefabRef;
            return dataFile;
        }

        public static CardType WithTextBoxSprite(this CardType dataFile, Sprite icon)
        {
            dataFile.textBoxSprite = icon;
            return dataFile;
        }

        public static CardType WithTextBoxSprite(this CardType dataFile, string icon)
        {
            dataFile.textBoxSprite = new InternalMod(null).GetImageSprite(icon);
            return dataFile;
        }

        public static CardType WithNameTagSprite(this CardType dataFile, Sprite icon)
        {
            dataFile.nameTagSprite = icon;
            return dataFile;
        }

        public static CardType WithNameTagSprite(this CardType dataFile, string icon)
        {
            dataFile.nameTagSprite = new InternalMod(null).GetImageSprite(icon);
            return dataFile;
        }

        public static CardType WithTitle(this CardType dataFile, string title, SystemLanguage lang = SystemLanguage.English)
        {
            StringTable collection = LocalizationHelper.GetCollection("Cards", new LocaleIdentifier(lang));
            collection.SetString(dataFile.name + "_type_title", title);
            dataFile.titleKey = collection.GetString(dataFile.name + "_type_title");
            return dataFile;
        }

        public static CardType WithTitle(this CardType dataFile, LocalizedString str)
        {
            dataFile.titleKey = str;
            return dataFile;
        }

        public static CardType WithCanDie(this CardType dataFile, bool canDie)
        {
            dataFile.canDie = canDie;
            return dataFile;
        }

        public static CardType WithCanTakeCrown(this CardType dataFile, bool canTakeCrown)
        {
            dataFile.canTakeCrown = canTakeCrown;
            return dataFile;
        }

        public static CardType WithCanRecall(this CardType dataFile, bool canRecall)
        {
            dataFile.canRecall = canRecall;
            return dataFile;
        }

        public static CardType WithCanReserve(this CardType dataFile, bool canReserve)
        {
            dataFile.canReserve = canReserve;
            return dataFile;
        }

        public static CardType WithItem(this CardType dataFile, bool item)
        {
            dataFile.item = item;
            return dataFile;
        }

        public static CardType WithUnit(this CardType dataFile, bool unit)
        {
            dataFile.unit = unit;
            return dataFile;
        }

        public static CardType WithTag(this CardType dataFile, string tag)
        {
            dataFile.tag = tag;
            return dataFile;
        }

        public static CardType WithMiniboss(this CardType dataFile, bool miniboss)
        {
            dataFile.miniboss = miniboss;
            return dataFile;
        }

        public static CardType WithDiscoverInJournal(this CardType dataFile, bool discoverInJournal)
        {
            dataFile.discoverInJournal = discoverInJournal;
            return dataFile;
        }

        public static CardType WithDescriptionColours(this CardType dataFile, Text.ColourProfileHex descriptionColours)
        {
            dataFile.descriptionColours = descriptionColours;
            return dataFile;
        }
    }
    public static class CardUpgradeDataHelpers
    {

        public static CardUpgradeData WithTier(this CardUpgradeData dataFile, int tier)
        {
            dataFile.tier = tier;
            return dataFile;
        }

        public static CardUpgradeData WithImage(this CardUpgradeData dataFile, Sprite img)
        {
            dataFile.image = img;
            return dataFile;
        }

        public static CardUpgradeData WithType(this CardUpgradeData dataFile, CardUpgradeData.Type type)
        {
            dataFile.type = type;
            return dataFile;
        }

        public static CardUpgradeData SetAttackEffects(this CardUpgradeData dataFile, params CardData.StatusEffectStacks[] efs)
        {
            dataFile.attackEffects = efs;
            return dataFile;
        }

        public static CardUpgradeData SetEffects(this CardUpgradeData dataFile, params CardData.StatusEffectStacks[] efs)
        {
            dataFile.effects = efs;
            return dataFile;
        }

        public static CardUpgradeData SetTraits(this CardUpgradeData dataFile, params CardData.TraitStacks[] efs)
        {
            dataFile.giveTraits = efs;
            return dataFile;
        }

        public static CardUpgradeData SetScripts(this CardUpgradeData dataFile, params CardScript[] efs)
        {
            dataFile.scripts = efs;
            return dataFile;
        }

        public static CardUpgradeData SetConstraints(this CardUpgradeData dataFile, params TargetConstraint[] efs)
        {
            dataFile.targetConstraints = efs;
            return dataFile;
        }

        public static CardUpgradeData SetBecomesTarget(this CardUpgradeData dataFile, bool val)
        {
            dataFile.becomesTargetedCard = val;
            return dataFile;
        }

        public static CardUpgradeData SetCanBeRemoved(this CardUpgradeData dataFile, bool val)
        {
            dataFile.canBeRemoved = val;
            return dataFile;
        }

        public static CardUpgradeData ChangeDamage(this CardUpgradeData dataFile, int val)
        {
            dataFile.damage = val;
            return dataFile;
        }

        public static CardUpgradeData ChangeHP(this CardUpgradeData dataFile, int val)
        {
            dataFile.hp = val;
            return dataFile;
        }

        public static CardUpgradeData ChangeCounter(this CardUpgradeData dataFile, int val)
        {
            dataFile.counter = val;
            return dataFile;
        }

        public static CardUpgradeData ChangeUses(this CardUpgradeData dataFile, int val)
        {
            dataFile.uses = val;
            return dataFile;
        }

        public static CardUpgradeData ChangeEffectBonus(this CardUpgradeData dataFile, int val)
        {
            dataFile.effectBonus = val;
            return dataFile;
        }

        public static CardUpgradeData WithSetDamage(this CardUpgradeData dataFile, bool val)
        {
            dataFile.setDamage = val;
            return dataFile;
        }

        public static CardUpgradeData WithSetHP(this CardUpgradeData dataFile, bool val)
        {
            dataFile.setHp = val;
            return dataFile;
        }

        public static CardUpgradeData WithSetCounter(this CardUpgradeData dataFile, bool val)
        {
            dataFile.setCounter = val;
            return dataFile;
        }

        public static CardUpgradeData WithSetUses(this CardUpgradeData dataFile, bool val)
        {
            dataFile.setUses = val;
            return dataFile;
        }

        public static CardUpgradeData WithImage(this CardUpgradeData dataFile, string img)
        {
            dataFile.image = new InternalMod(null).ImagePath(img).ToSprite();
            return dataFile;
        }

        public static CardUpgradeData WithTitle(this CardUpgradeData dataFile, LocalizedString title)
        {
            dataFile.titleKey = title;
            return dataFile;
        }

        public static CardUpgradeData WithText(this CardUpgradeData dataFile, LocalizedString text)
        {
            dataFile.textKey = text;
            return dataFile;
        }

        public static CardUpgradeData WithTitle(this CardUpgradeData dataFile, string title, SystemLanguage lang = SystemLanguage.English)
        {
            StringTable collection = LocalizationHelper.GetCollection("Upgrades", new LocaleIdentifier(lang));
            collection.SetString(dataFile.name + "_title", title);
            dataFile.titleKey = collection.GetString(dataFile.name + "_title");
            return dataFile;
        }

        public static CardUpgradeData WithText(this CardUpgradeData dataFile, string text, SystemLanguage lang = SystemLanguage.English)
        {
            StringTable collection = LocalizationHelper.GetCollection("Upgrades", new LocaleIdentifier(lang));
            collection.SetString(dataFile.name + "_text", text);
            dataFile.textKey = collection.GetString(dataFile.name + "_text");
            return dataFile;
        }
    }
    public static class ChallengeDataHelpers
    {
        public static ChallengeData WithText(this ChallengeData dataFile, LocalizedString str)
        {
            dataFile.textKey = str;
            return dataFile;
        }

        public static ChallengeData WithRewardText(this ChallengeData dataFile, LocalizedString str)
        {
            dataFile.rewardKey = str;
            return dataFile;
        }

        public static ChallengeData WithTitle(this ChallengeData dataFile, string title, SystemLanguage lang = SystemLanguage.English)
        {
            StringTable collection = LocalizationHelper.GetCollection("Challenges", new LocaleIdentifier(lang));
            collection.SetString(dataFile.name + "_title", title);
            dataFile.titleKey = collection.GetString(dataFile.name + "_title");
            return dataFile;
        }

        public static ChallengeData WithText(this ChallengeData dataFile, string title, SystemLanguage lang = SystemLanguage.English)
        {
            StringTable collection = LocalizationHelper.GetCollection("Challenges", new LocaleIdentifier(lang));
            collection.SetString(dataFile.name + "_text", title);
            dataFile.textKey = collection.GetString(dataFile.name + "_text");
            return dataFile;
        }

        public static ChallengeData WithRewardText(this ChallengeData dataFile, string title, SystemLanguage lang = SystemLanguage.English)
        {
            StringTable collection = LocalizationHelper.GetCollection("Challenges", new LocaleIdentifier(lang));
            collection.SetString(dataFile.name + "_reward", title);
            dataFile.rewardKey = collection.GetString(dataFile.name + "_reward");
            return dataFile;
        }

        public static ChallengeData WithGoal(this ChallengeData dataFile, int amountGoal)
        {
            dataFile.goal = amountGoal;
            return dataFile;
        }

        public static ChallengeData WithListener(this ChallengeData dataFile, ChallengeListener listener)
        {
            dataFile.listener = listener;
            return dataFile;
        }

        public static ChallengeData WithListener(this ChallengeData dataFile, string listener)
        {
            dataFile.listener = new InternalMod(null).Get<ChallengeListener>(listener);
            return dataFile;
        }

        public static ChallengeData WithIcon(this ChallengeData dataFile, Sprite icon)
        {
            dataFile.icon = icon;
            return dataFile;
        }

        public static ChallengeData WithRequires(this ChallengeData dataFile, params ChallengeData[] requires)
        {
            dataFile.requires = requires;
            return dataFile;
        }

        public static ChallengeData WithRequires(this ChallengeData dataFile, params string[] requires)
        {
            dataFile.requires = ((IEnumerable<string>)requires).Select<string, ChallengeData>(new Func<string, ChallengeData>(new InternalMod(null).Get<ChallengeData>)).ToArray<ChallengeData>();
            return dataFile;
        }

        public static ChallengeData WithReward(this ChallengeData dataFile, UnlockData reward)
        {
            dataFile.reward = reward;
            return dataFile;
        }

        public static ChallengeData WithReward(this ChallengeData dataFile, string reward)
        {
            dataFile.reward = new InternalMod(null).Get<UnlockData>(reward);
            return dataFile;
        }
    }
    public static class ChallengeListenerHelpers
    {
        public static ChallengeListener WithKey(this ChallengeListener dataFile, string key)
        {
            dataFile.key = key;
            dataFile.hasKey = true;
            return dataFile;
        }

        public static ChallengeListener WithCheckType(this ChallengeListener dataFile, ChallengeListener.CheckType type)
        {
            dataFile.checkType = type;
            return dataFile;
        }

        public static ChallengeListener WithStat(this ChallengeListener dataFile, string stat)
        {
            dataFile.stat = stat;
            return dataFile;
        }

        public static ChallengeListener WithStat(this ChallengeListener dataFile, int toReach)
        {
            dataFile.target = toReach;
            return dataFile;
        }
    }
    public static class ClassDataHelpers
    {
        public static ClassData WithRequiresUnlock(this ClassData dataFile, UnlockData requiresUnlock)
        {
            dataFile.requiresUnlock = requiresUnlock;
            return dataFile;
        }

        public static ClassData WithStartingInventory(this ClassData dataFile, Inventory startingInventory)
        {
            dataFile.startingInventory = startingInventory;
            return dataFile;
        }

        public static ClassData WithLeaders(this ClassData dataFile, params CardData[] leaders)
        {
            dataFile.leaders = leaders;
            return dataFile;
        }

        public static ClassData WithCharacterPrefab(this ClassData dataFile, Character characterPrefab)
        {
            dataFile.characterPrefab = characterPrefab;
            return dataFile;
        }

        public static ClassData WithRewardPools(this ClassData dataFile, params RewardPool[] rewardPools)
        {
            dataFile.rewardPools = rewardPools;
            return dataFile;
        }

        public static ClassData WithSelectSfxEvent(this ClassData dataFile, EventReference selectSfxEvent)
        {
            dataFile.selectSfxEvent = selectSfxEvent;
            return dataFile;
        }
        /// <summary>see vanilla references</summary>
        /// <param name="selectSfxEvent">format: "event:/..."</param>
        public static ClassData WithSelectSfxEvent(this ClassData dataFile, string selectSfxEvent)
        {
            dataFile.selectSfxEvent = RuntimeManager.PathToEventReference(selectSfxEvent);
            return dataFile;
        }

        public static ClassData WithFlag(this ClassData dataFile, Sprite flag)
        {
            dataFile.flag = flag;
            return dataFile;
        }

        public static ClassData WithFlag(this ClassData dataFile, string flag)
        {
            dataFile.flag = new InternalMod(null).GetImageSprite(flag);
            return dataFile;
        }
    }
    public static class EyeDataHelpers
    {
        public static EyeData WithCardData(this EyeData dataFile, string cardData)
        {
            dataFile.cardData = cardData;
            return dataFile;
        }

        public static EyeData WithCardData(this EyeData dataFile, CardData cardData)
        {
            dataFile.cardData = cardData.name;
            return dataFile;
        }

        public static EyeData WithEyes(this EyeData dataFile, params EyeData.Eye[] eyes)
        {
            dataFile.eyes = eyes;
            return dataFile;
        }
    }
    public static class GameModeHelpers
    {
        public static GameMode WithSaveFileName(this GameMode dataFile, string saveFileName)
        {
            dataFile.saveFileName = saveFileName;
            return dataFile;
        }

        public static GameMode WithSeed(this GameMode dataFile, string seed)
        {
            dataFile.seed = seed;
            return dataFile;
        }

        public static GameMode WithClasses(this GameMode dataFile, params ClassData[] classes)
        {
            dataFile.classes = classes;
            return dataFile;
        }

        public static GameMode WithGenerator(this GameMode dataFile, CampaignGenerator generator)
        {
            dataFile.generator = generator;
            return dataFile;
        }

        public static GameMode WithPopulator(this GameMode dataFile, CampaignPopulator populator)
        {
            dataFile.populator = populator;
            return dataFile;
        }

        public static GameMode WithStartInNode(this GameMode dataFile, bool startInNode)
        {
            dataFile.startInNode = startInNode;
            return dataFile;
        }

        public static GameMode WithTakeStartingPet(this GameMode dataFile, bool takeStartingPet = true)
        {
            dataFile.takeStartingPet = takeStartingPet;
            return dataFile;
        }

        public static GameMode WithCountsAsWin(this GameMode dataFile, bool countsAsWin = true)
        {
            dataFile.countsAsWin = countsAsWin;
            return dataFile;
        }

        public static GameMode WithShowStats(this GameMode dataFile, bool showStats = true)
        {
            dataFile.showStats = showStats;
            return dataFile;
        }

        public static GameMode WithGainProgress(this GameMode dataFile, bool gainProgress = true)
        {
            dataFile.gainProgress = gainProgress;
            return dataFile;
        }

        public static GameMode WithDoSave(this GameMode dataFile, bool doSave = true)
        {
            dataFile.doSave = doSave;
            return dataFile;
        }

        public static GameMode WithCanRestart(this GameMode dataFile, bool canRestart = true)
        {
            dataFile.canRestart = canRestart;
            return dataFile;
        }

        public static GameMode WithCanGoBack(this GameMode dataFile, bool canGoBack = true)
        {
            dataFile.canGoBack = canGoBack;
            return dataFile;
        }

        public static GameMode WithSubmitScore(this GameMode dataFile, bool submitScore = false)
        {
            dataFile.submitScore = submitScore;
            return dataFile;
        }

        public static GameMode WithMainGameMode(this GameMode dataFile, bool mainGameMode = true)
        {
            dataFile.mainGameMode = mainGameMode;
            return dataFile;
        }

        public static GameMode WithDailyRun(this GameMode dataFile, bool dailyRun = false)
        {
            dataFile.dailyRun = dailyRun;
            return dataFile;
        }

        public static GameMode WithTutorialRun(this GameMode dataFile, bool tutorialRun = false)
        {
            dataFile.tutorialRun = tutorialRun;
            return dataFile;
        }

        public static GameMode WithLeaderboardType(this GameMode dataFile, Scores.Type leaderboardType)
        {
            dataFile.leaderboardType = leaderboardType;
            return dataFile;
        }

        public static GameMode WithStartScene(this GameMode dataFile, string startScene)
        {
            dataFile.startScene = startScene;
            return dataFile;
        }

        public static GameMode WithSceneAfterSelection(this GameMode dataFile, string sceneAfterSelection = "Campaign")
        {
            dataFile.sceneAfterSelection = sceneAfterSelection;
            return dataFile;
        }

        public static GameMode WithCampaignSystemNames(this GameMode dataFile, params string[] campaignSystemNames)
        {
            dataFile.campaignSystemNames = campaignSystemNames;
            return dataFile;
        }

        public static GameMode WithSystemsToDisable(this GameMode dataFile, params string[] systemsToDisable)
        {
            dataFile.systemsToDisable = systemsToDisable;
            return dataFile;
        }
    }
    public static class GameModifierDataHelpers
    {
        public static GameModifierData WithValue(this GameModifierData dataFile, int value = 100)
        {
            dataFile.value = value;
            return dataFile;
        }

        public static GameModifierData WithVisible(this GameModifierData dataFile, bool visible = true)
        {
            dataFile.visible = visible;
            return dataFile;
        }

        public static GameModifierData WithBellSprite(this GameModifierData dataFile, Sprite bellSprite)
        {
            dataFile.bellSprite = bellSprite;
            return dataFile;
        }

        public static GameModifierData WithBellSprite(this GameModifierData dataFile, string bellSprite)
        {
            dataFile.bellSprite = new InternalMod(null).GetImageSprite(bellSprite);
            return dataFile;
        }

        public static GameModifierData WithDingerSprite(this GameModifierData dataFile, Sprite dingerSprite)
        {
            dataFile.dingerSprite = dingerSprite;
            return dataFile;
        }

        public static GameModifierData WithDingerSprite(this GameModifierData dataFile, string dingerSprite)
        {
            dataFile.dingerSprite = new InternalMod(null).GetImageSprite(dingerSprite);
            return dataFile;
        }

        public static GameModifierData WithTitle(this GameModifierData dataFile, LocalizedString title)
        {
            dataFile.titleKey = title;
            return dataFile;
        }

        public static GameModifierData WithTitle(this GameModifierData dataFile, string title, SystemLanguage lang = SystemLanguage.English)
        {
            StringTable collection = LocalizationHelper.GetCollection("Cards", new LocaleIdentifier(lang));
            collection.SetString(dataFile.name + "_modifier_title", title);
            dataFile.titleKey = collection.GetString(dataFile.name + "_modifier_title");
            return dataFile;
        }

        public static GameModifierData WithDescription(this GameModifierData dataFile, LocalizedString title)
        {
            dataFile.descriptionKey = title;
            return dataFile;
        }

        public static GameModifierData WithDescription(this GameModifierData dataFile, string title, SystemLanguage lang = SystemLanguage.English)
        {
            StringTable collection = LocalizationHelper.GetCollection("Cards", new LocaleIdentifier(lang));
            collection.SetString(dataFile.name + "_modifier_desc", title);
            dataFile.descriptionKey = collection.GetString(dataFile.name + "_modifier_desc");
            return dataFile;
        }

        public static GameModifierData WithSystemsToAdd(this GameModifierData dataFile, params string[] systemsToAdd)
        {
            dataFile.systemsToAdd = systemsToAdd;
            return dataFile;
        }

        public static GameModifierData WithSetupScripts(this GameModifierData dataFile, params Script[] setupScripts)
        {
            dataFile.setupScripts = setupScripts;
            return dataFile;
        }

        public static GameModifierData WithStartScripts(this GameModifierData dataFile, params Script[] startScripts)
        {
            dataFile.startScripts = startScripts;
            return dataFile;
        }

        public static GameModifierData WithScriptPriority(this GameModifierData dataFile, int scriptPriority)
        {
            dataFile.scriptPriority = scriptPriority;
            return dataFile;
        }

        public static GameModifierData WithBlockedBy(this GameModifierData dataFile, params GameModifierData[] blockedBy)
        {
            dataFile.blockedBy = blockedBy;
            return dataFile;
        }

        public static GameModifierData WithLinkedStormBell(this GameModifierData dataFile, HardModeModifierData linkedStormBell)
        {
            dataFile.linkedStormBell = linkedStormBell;
            return dataFile;
        }

        public static GameModifierData WithRingSfxEvent(this GameModifierData dataFile, EventReference ringSfxEvent)
        {
            dataFile.ringSfxEvent = ringSfxEvent;
            return dataFile;
        }
        /// <summary>see vanilla references</summary>
        /// <param name="selectSfxEvent">format: "event:/..."</param>
        public static GameModifierData WithRingSfxEvent(this GameModifierData dataFile, string ringSfxEvent)
        {
            dataFile.ringSfxEvent = RuntimeManager.PathToEventReference(ringSfxEvent);
            return dataFile;
        }

        public static GameModifierData WithRingSfxPitch(this GameModifierData dataFile)
        {
            dataFile.ringSfxPitch = new Vector2(1f, 1f);
            return dataFile;
        }

        public static GameModifierData WithRingSfxPitch(this GameModifierData dataFile, Vector2 ringSfxPitch)
        {
            dataFile.ringSfxPitch = ringSfxPitch;
            return dataFile;
        }
    }
    public static class KeywordDataHelpers
    {
        public static KeywordData WithTitle(this KeywordData dataFile, string title, SystemLanguage lang = SystemLanguage.English)
        {
            StringTable collection = LocalizationHelper.GetCollection("Tooltips", new LocaleIdentifier(lang));
            collection.SetString(dataFile.name + "_title", title);
            dataFile.titleKey = collection.GetString(dataFile.name + "_title");
            return dataFile;
        }

        public static KeywordData WithDescription(this KeywordData dataFile, string title, SystemLanguage lang = SystemLanguage.English)
        {
            StringTable collection = LocalizationHelper.GetCollection("Tooltips", new LocaleIdentifier(lang));
            collection.SetString(dataFile.name + "_desc", title);
            dataFile.descKey = collection.GetString(dataFile.name + "_desc");
            return dataFile;
        }

        public static KeywordData WithTitleColour(this KeywordData dataFile, Color? theColour = null)
        {
            if (!theColour.HasValue)
                theColour = new Color?(new Color(1f, 0.7921569f, 0.3411765f, 1f));
            dataFile.titleColour = theColour.Value;
            return dataFile;
        }

        public static KeywordData WithBodyColour(this KeywordData dataFile, Color? theColour = null)
        {
            if (!theColour.HasValue)
                theColour = new Color?(Color.white);
            dataFile.bodyColour = theColour.Value;
            return dataFile;
        }

        public static KeywordData WithNoteColour(this KeywordData dataFile, Color? theColour = null)
        {
            if (!theColour.HasValue)
                theColour = new Color?(Color.gray);
            dataFile.noteColour = theColour.Value;
            return dataFile;
        }

        public static KeywordData WithPanelColour(this KeywordData dataFile, Color theColour)
        {
            dataFile.panelColor = theColour;
            return dataFile;
        }

        public static KeywordData WithPanelSprite(this KeywordData dataFile, string image)
        {
            dataFile.panelSprite = new InternalMod(null).GetImageSprite(image);
            return dataFile;
        }

        public static KeywordData WithIconName(this KeywordData dataFile, string iconName)
        {
            dataFile.iconName = iconName;
            return dataFile;
        }

        public static KeywordData WithIconTint(this KeywordData dataFile, Color hexColor)
        {
            dataFile.iconTintHex = hexColor.ToHexRGB();
            return dataFile;
        }

        public static KeywordData WithShow(this KeywordData dataFile, bool show = true)
        {
            dataFile.show = show;
            return dataFile;
        }

        public static KeywordData WithShowName(this KeywordData dataFile, bool show)
        {
            dataFile.showName = show;
            return dataFile;
        }

        public static KeywordData WithShowIcon(this KeywordData dataFile, bool show = true)
        {
            dataFile.showIcon = show;
            return dataFile;
        }

        public static KeywordData WithCanStack(this KeywordData dataFile, bool show)
        {
            dataFile.canStack = show;
            return dataFile;
        }
    }
    public static class StatusEffectDataHelpers
    {
        public static StatusEffectData WithIsStatus(this StatusEffectData dataFile, bool value)
        {
            dataFile.isStatus = value;
            return dataFile;
        }

        public static StatusEffectData WithIsReaction(this StatusEffectData dataFile, bool value)
        {
            dataFile.isReaction = value;
            return dataFile;
        }

        public static StatusEffectData WithIsKeyword(this StatusEffectData dataFile, bool value)
        {
            dataFile.isKeyword = value;
            return dataFile;
        }

        public static StatusEffectData WithType(this StatusEffectData dataFile, string type)
        {
            dataFile.type = type;
            return dataFile;
        }

        public static StatusEffectData WithKeyword(this StatusEffectData dataFile, string type)
        {
            dataFile.keyword = type;
            return dataFile;
        }

        public static StatusEffectData WithIconGroupName(this StatusEffectData dataFile, string type)
        {
            dataFile.iconGroupName = type;
            return dataFile;
        }

        public static StatusEffectData WithVisible(this StatusEffectData dataFile, bool value)
        {
            dataFile.visible = value;
            return dataFile;
        }

        public static StatusEffectData WithStackable(this StatusEffectData dataFile, bool value)
        {
            dataFile.stackable = value;
            return dataFile;
        }

        public static StatusEffectData WithOffensive(this StatusEffectData dataFile, bool value)
        {
            dataFile.offensive = value;
            return dataFile;
        }

        public static StatusEffectData WithMakesOffensive(this StatusEffectData dataFile, bool value)
        {
            dataFile.makesOffensive = value;
            return dataFile;
        }

        public static StatusEffectData WithDoesDamage(this StatusEffectData dataFile, bool value)
        {
            dataFile.doesDamage = value;
            return dataFile;
        }

        public static StatusEffectData WithCanBeBoosted(this StatusEffectData dataFile, bool value)
        {
            dataFile.canBeBoosted = value;
            return dataFile;
        }

        public static StatusEffectData WithText(this StatusEffectData dataFile, string title, SystemLanguage lang = SystemLanguage.English)
        {
            StringTable collection = LocalizationHelper.GetCollection("Card Text", new LocaleIdentifier(lang));
            collection.SetString(dataFile.name + "_text", title);
            dataFile.textKey = collection.GetString(dataFile.name + "_text");
            return dataFile;
        }

        public static StatusEffectData WithTextInsert(this StatusEffectData dataFile, string value)
        {
            dataFile.textInsert = value;
            return dataFile;
        }

        public static StatusEffectData WithOrder(this StatusEffectData dataFile, int order)
        {
            dataFile.textOrder = order;
            return dataFile;
        }
    }
    public static class TraitDataHelpers
    {
        public static TraitData WithKeyword(this TraitData dataFile, KeywordData data)
        {
            dataFile.keyword = data;
            return dataFile;
        }

        public static TraitData WithEffects(this TraitData dataFile, params StatusEffectData[] effects)
        {
            dataFile.effects = effects;
            return dataFile;
        }

        public static TraitData WithOverrides(this TraitData dataFile, params TraitData[] traits)
        {
            dataFile.overrides = traits;
            return dataFile;
        }

        public static TraitData WithIsReaction(this TraitData dataFile, bool isReaction)
        {
            dataFile.isReaction = isReaction;
            return dataFile;
        }
    }
    public static class UnlockDataHelpers
    {
        public static UnlockData WithUnlockDescription(this UnlockData dataFile, LocalizedString str)
        {
            dataFile.unlockDesc = str;
            return dataFile;
        }

        public static UnlockData WithUnlockTitle(this UnlockData dataFile, LocalizedString str)
        {
            dataFile.unlockTitle = str;
            return dataFile;
        }

        public static UnlockData WithUnlockDescription(this UnlockData dataFile, string title, SystemLanguage lang = SystemLanguage.English)
        {
            StringTable collection = LocalizationHelper.GetCollection("UI Text", new LocaleIdentifier(lang));
            collection.SetString(dataFile.name + "_unlockDesc", title);
            dataFile.unlockDesc = collection.GetString(dataFile.name + "_unlockDesc");
            return dataFile;
        }

        public static UnlockData WithUnlockTitle(this UnlockData dataFile, string title, SystemLanguage lang = SystemLanguage.English)
        {
            StringTable collection = LocalizationHelper.GetCollection("UI Text", new LocaleIdentifier(lang));
            collection.SetString(dataFile.name + "_unlockTitle", title);
            dataFile.unlockTitle = collection.GetString(dataFile.name + "_unlockTitle");
            return dataFile;
        }

        public static UnlockData WithRequires(this UnlockData dataFile, params UnlockData[] requires)
        {
            dataFile.requires = requires;
            return dataFile;
        }

        public static UnlockData WithRequires(this UnlockData dataFile, params string[] requires)
        {
            dataFile.requires = ((IEnumerable<string>)requires).Select<string, UnlockData>(new Func<string, UnlockData>(new InternalMod(null).Get<UnlockData>)).ToArray<UnlockData>();
            return dataFile;
        }

        public static UnlockData WithLowPriority(this UnlockData dataFile, float priority)
        {
            dataFile.lowPriority = priority;
            return dataFile;
        }

        public static UnlockData WithBuilding(this UnlockData dataFile, BuildingType relatedBuilding)
        {
            dataFile.relatedBuilding = relatedBuilding;
            relatedBuilding.unlocks = relatedBuilding.unlocks.AddToArray<UnlockData>(dataFile);
            return dataFile;
        }

        public static UnlockData WithBuilding(this UnlockData dataFile, string relatedBuilding)
        {
            return dataFile.WithBuilding(new InternalMod(null).Get<BuildingType>(relatedBuilding));
        }

        public static UnlockData WithType(this UnlockData dataFile, UnlockData.Type type)
        {
            dataFile.type = type;
            return dataFile;
        }
    }
}
