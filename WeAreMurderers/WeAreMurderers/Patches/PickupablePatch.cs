using System.Collections.Generic;
using HarmonyLib;
using MurrenMods.WeAreMurderers.Entries;

namespace MurrenMods.WeAreMurderers.Patches
{
    [HarmonyPatch(typeof(Pickupable))]
    public class PickupablePatch
    {
        [HarmonyPatch(nameof(Pickupable.OnHandClick))]
        [HarmonyPrefix]
        public static bool OnHandClickPrefix(Pickupable __instance)
        {
            string id = __instance.GetTechType().ToString();
            if (!id.Contains("QX-VR_log")) return true;

            // If the chip is already unlocked, do not allow pickup
            if (Data.unlockedEntries.Contains(id))
            {
                return false;
            }

            // If the chip is not unlocked, allow pickup and register the entry
            Data.unlockedEntries.Add(id);
            int oldll = Data.LanguageLevel;
            Data.FoundChips++;
            if(Data.LanguageLevel != oldll)
            {
                //TODO: PDA notification of increased translation levels
            }
            EntryHandler.UnlockEntries(Data.LanguageLevel,  Data.unlockedEntries);
            return false;
        }
    }
}