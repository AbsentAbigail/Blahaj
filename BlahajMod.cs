using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WildfrostHopeMod;

namespace Blahaj
{
    public partial class BlahajMod : WildfrostMod
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

        public static List<object> assets = new List<object>();
        private bool preLoaded = false;

        private void CreateModAssets()
        {
            AddBlackfiskStatusEffects();
            AddBlahajStatusEffects();
            AddValStatusEffects();
            AddKramigStatusEffects();
            AddAftonsparvStatusEffects();

            LogHelper.Log("Status Effects added");

            AddBlahajKeyword();
            AddAftonsparvKeyword();

            LogHelper.Log("Keywords added");

            AddBlahajCard();
            AddBlackfiskCard();
            AddValCard();
            AddKramigCard();
            AddAftonsparvCards();

            LogHelper.Log("Plushies added");

            AddBlahajCharm();

            LogHelper.Log("Shark charm added");

            preLoaded = true;

            assets = new List<object>();
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
            if (assets.OfType<T>().Any())
                LogHelper.Log($"Adding {typeof(Y).Name}s: {assets.OfType<T>().Select(a => a._data.name).Join()}");
            return assets.OfType<T>().ToList();
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