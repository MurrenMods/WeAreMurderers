using MurrenMods.WeAreMurderers.Patches;
using UnityEngine;

namespace MurrenMods.WeAreMurderers.Object.Component;

public class LoadEventHandler : MonoBehaviour
{
    public void OnBecameVisible()
    {
        WeAreMurderersMain.Log.LogInfo("Grave became visible.");
        if (WeAreMurderersMain.SaveData.CuredGun)
        {
            PrecursorDisableGunTerminalPatch.DisableGraveRing(this.gameObject);
            WeAreMurderersMain.Log.LogInfo("Disabled grave ring on load because gun was cured.");
        }
    }
}