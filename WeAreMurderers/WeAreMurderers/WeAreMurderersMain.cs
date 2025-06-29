using System.Reflection;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using MurrenMods.WeAreMurderers.Entries;
using Nautilus.FMod;
using Nautilus.Handlers;
using Nautilus.Utility;
using UnityEngine;

namespace MurrenMods.WeAreMurderers
{
    [BepInPlugin(WAMguid, PluginName, VersionString)]
    [BepInDependency("com.snmodding.nautilus")] // marks Nautilus as a dependency for this mod
    public class WeAreMurderersMain : BaseUnityPlugin
    {
        private const string WAMguid = "com.MurrenMods.WeAreMurderers";
        private const string PluginName = "We Are Murderers";
        private const string VersionString = "0.1.0";

        public static AssetBundle WAMAssets { get; private set; }
        
        public static SaveData SaveData = new SaveData();

        private static readonly Harmony Harmony = new Harmony(WAMguid);

        public static ManualLogSource Log;

        private void Awake()
        {
            Log = Logger;
            Logger.LogInfo("Loading " + PluginName + " " + VersionString + "...");
            Harmony.PatchAll();
            
            WAMAssets = AssetBundleLoadingUtils.LoadFromAssetsFolder(Assembly.GetExecutingAssembly(), "wearemurderers_res");

            LoadSounds();
            
            EntryHandler.RegisterEntries(Data.Entries);
            LanguageHandler.RegisterLocalizationFolder();
            SaveData = SaveDataHandler.RegisterSaveDataCache<SaveData>();
            
            Logger.LogInfo(PluginName + " " + VersionString + " " + "loaded.");
        }

        private void LoadSounds()
        {
            CustomSoundSourceBase source = new AssetBundleSoundSource(WAMAssets);
            FModSoundBuilder builder = new FModSoundBuilder(source);
            
            builder.CreateNewEvent("languagelevel", "bus:/master/SFX_for_pause/PDA_pause/all/all voice/AI voice")
                .SetMode2D()
                .SetSound("languagelevelupgrade")
                .Register();
            
            builder.CreateNewEvent("invalidchip9", "bus:/master/SFX_for_pause/PDA_pause/all/all voice/AI voice")
                .SetMode2D()
                .SetSound("chip9")
                .Register();
        }
    }
}