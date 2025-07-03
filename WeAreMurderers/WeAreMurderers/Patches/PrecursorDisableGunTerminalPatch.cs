using System.Collections;
using HarmonyLib;
using Nautilus.Utility;
using UnityEngine;

namespace MurrenMods.WeAreMurderers.Patches
{
    [HarmonyPatch(typeof(PrecursorDisableGunTerminal))]
    public class PrecursorDisableGunTerminalPatch
    {
        [HarmonyPatch(nameof(PrecursorDisableGunTerminal.OnHandClick))]
        [HarmonyPostfix]
        public static void OnHandClickPostfix(PrecursorDisableGunTerminal __instance)
        {
            if ((bool)__instance.GetInstanceField("playerCured"))
            {
                WeAreMurderersMain.SaveData.CuredGun = true;
                UWE.CoroutineHost.StartCoroutine(AddPDA());
            }
        }

        private static IEnumerator AddPDA()
        {
            var t = Time.time;
            yield return new WaitUntil(() => Time.time > t + 12);
            PDALog.Add("thankyou", true);
            yield return null;
        }
        
        public static void DisableGraveRing(GameObject graveobj = null)
        {
            // Find the grave ring object in the scene
            graveobj = graveobj == null ? GameObject.Find("QX-VR_Grave(Clone)") : graveobj;
            if (graveobj == null)
            {
                WeAreMurderersMain.Log.LogError("Grave ring object not found in the scene.");
                return;
            }
            WeAreMurderersMain.Log.LogInfo("Found grave object, disabling ring....");
            // Get the renderer component of the grave ring
            var graverenderer = graveobj.GetComponent<Renderer>();
            // Disable the grave ring by setting its active state to false
            graverenderer.materials[1] = graverenderer.materials[0];
            graverenderer.materials[1].CopyPropertiesFromMaterial(graverenderer.materials[0]);
            foreach (var e in graverenderer.materials[0].GetTexturePropertyNameIDs())
            {
                graverenderer.materials[1].SetTexture(e, graverenderer.materials[0].GetTexture(e));
            }
            graverenderer.materials[1].shader = graverenderer.materials[0].shader;
            WeAreMurderersMain.Log.LogInfo("Disabled grave ring successfully.");
        }
    }
}