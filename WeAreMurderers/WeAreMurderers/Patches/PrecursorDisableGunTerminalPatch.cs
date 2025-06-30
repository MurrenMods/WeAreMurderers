using HarmonyLib;
using Story;

namespace MurrenMods.WeAreMurderers.Patches
{
    [HarmonyPatch(typeof(PrecursorDisableGunTerminal))]
    public class PrecursorDisableGunTerminalPatch
    {
        [HarmonyPatch(nameof(PrecursorDisableGunTerminal.NotifyGoalComplete))]
        [HarmonyPostfix]
        public static void NotifyGoalCompletePostfix(PrecursorDisableGunTerminal __instance)
        {
            //doesnt work in creative apparently
            if (StoryGoalManager.main.IsGoalComplete(__instance.gunDeactivate.key))
            {
                PDALog.Add("thankyou", true);
            }
        }
    }
}