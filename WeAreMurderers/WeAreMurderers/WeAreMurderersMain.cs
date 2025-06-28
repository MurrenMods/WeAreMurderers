using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace MurrenMods.WeAreMurderers
{
    [BepInPlugin(MyGuid, PluginName, VersionString)]
    [BepInDependency("com.snmodding.nautilus")] // marks Nautilus as a dependency for this mod
    public class WeAreMurderersMain : BaseUnityPlugin
    {
        private const string MyGuid = "com.MurrenMods.WeAreMurderers";
        private const string PluginName = "We Are Murderers";
        private const string VersionString = "0.1.0";

        private static readonly Harmony Harmony = new Harmony(MyGuid);

        public static ManualLogSource Log;

        private void Awake()
        {
            Harmony.PatchAll();
            Logger.LogInfo(PluginName + " " + VersionString + " " + "loaded.");
            Log = Logger;
        }
    }
}