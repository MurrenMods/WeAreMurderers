using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using MurrenMods.WeAreMurderers.Entries;
using Nautilus.Handlers;

namespace MurrenMods.WeAreMurderers.Patches
{
    [HarmonyPatch(typeof(Pickupable))]
    public class PickupablePatch
    {
        private static bool playedNotification = false;
        [HarmonyPatch(nameof(Pickupable.OnHandClick))]
        [HarmonyPrefix]
        public static bool OnHandClickPrefix(Pickupable __instance)
        {
            string id = __instance.GetTechType().ToString();
            if (!id.Contains("QX-VR_log"))
            {
                WeAreMurderersMain.Log.LogInfo("Picked up item with id " + id + ", not a QX-VR log, skipping pickup logic.");
                return true;
            }

            // If the chip is already unlocked, do not allow pickup
            if (WeAreMurderersMain.SaveData.UnlockedEntries.Contains(id))
            {
                WeAreMurderersMain.Log.LogInfo("Chip " + id + " is already unlocked, skipping pickup logic.");
                return false;
            }

            if (id == "QX-VR_log9" && !WeAreMurderersMain.SaveData.UnlockedEntries.Contains("QX-VR_log8"))
            {
                // If the chip is the last one, ensure the previous one is unlocked
                if (!playedNotification)
                {
                    playedNotification = true;
                    PDALog.Add("invalidchip9", true);
                }
                WeAreMurderersMain.Log.LogInfo("Chip 9 " + id + " cannot be picked up because QX-VR_log8 is not unlocked.");
                return false;
            }

            if (id == "QX-VR_log2")
            {
                PDAEncyclopedia.Add("AlienMeasurements", true);
            }

            // If the chip is not unlocked, allow pickup and register the entry
            WeAreMurderersMain.SaveData.UnlockedEntries.Add(id);
            int oldll = Data.LanguageLevel;
            WeAreMurderersMain.SaveData.FoundChips++;
            if(Data.LanguageLevel != oldll)
            {
                PDALog.Add("languagelevel", true);
            }
            EntryHandler.UnlockEntries(Data.LanguageLevel, WeAreMurderersMain.SaveData.UnlockedEntries);
            WeAreMurderersMain.Log.LogInfo("Picked up chip " + id + ", unlocking entry and updating language level.");
            return false;
        }
    }
}