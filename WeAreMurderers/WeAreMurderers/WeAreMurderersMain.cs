using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using MurrenMods.WeAreMurderers.Entries;
using MurrenMods.WeAreMurderers.Object;
using Nautilus.Handlers;

namespace MurrenMods.WeAreMurderers
{
    [BepInPlugin(WAMguid, PluginName, VersionString)]
    [BepInDependency("com.snmodding.nautilus")] // marks Nautilus as a dependency for this mod
    public class WeAreMurderersMain : BaseUnityPlugin
    {
        private const string WAMguid = "com.MurrenMods.WeAreMurderers";
        private const string PluginName = "We Are Murderers";
        private const string VersionString = "1.0.0";

        public static ResourceManager ResourceManager { get; private set; }

        
        public static SaveData SaveData = new SaveData();

        private static readonly Harmony Harmony = new Harmony(WAMguid);

        public static ManualLogSource Log;

        private void Awake()
        {
            Log = Logger;
            Logger.LogInfo("Loading " + PluginName + " " + VersionString + "...");
            Harmony.PatchAll();
            
            ResourceManager = new ResourceManager();
            ResourceManager.LoadMainBundle();
            UWE.CoroutineHost.StartCoroutine(ResourceManager.PrepareClonedMaterials());
            UWE.CoroutineHost.StartCoroutine(ObjectRegistry.RegisterObjects(SharedData.Entries));
            ResourceManager.LoadSounds();
            
            EntryHandler.RegisterEntries(SharedData.Entries);
            LanguageHandler.RegisterLocalizationFolder();
            SaveData = SaveDataHandler.RegisterSaveDataCache<SaveData>();
            
            Logger.LogInfo(PluginName + " " + VersionString + " " + "loaded.");
        }

        
    }
}