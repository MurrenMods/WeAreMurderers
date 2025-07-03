using System.Collections;
using UnityEngine;
using UWE;

namespace MurrenMods.WeAreMurderers.Utility;

public static class MaterialUtility
{
    public static void ApplyMaterial(Renderer renderer, int slot, Material material)
    {
        if (renderer == null || material == null)
        {
            WeAreMurderersMain.Log.LogError("Cannot apply material: Renderer or Material is null.");
            return;
        }
        var targetMaterial = renderer.materials[slot];
        targetMaterial = material;
        targetMaterial.CopyPropertiesFromMaterial(material);
        foreach (var e in material.GetTexturePropertyNameIDs())
        {
            targetMaterial.SetTexture(e, material.GetTexture(e));
        }
        targetMaterial.shader = material.shader;
        renderer.materials[slot] = targetMaterial;
    }
    
    public static IEnumerator FindMaterialFromPath(string path, IOut<Material> matResult)
    {
        Material mat = null;
        do
        {
            var handle = AddressablesUtility.LoadAsync<Material>(path);
            yield return handle.Task;
            mat = handle.Result;
            WeAreMurderersMain.Log.LogInfo("Loaded material " + path + ": " + (mat != null ? "Success" : "Failed"));
        } while (mat == null);
        matResult.Set(mat);
    }

    public static IEnumerator FindMaterialFromPrefabPath(string matName, string path, IOut<Material> matResult)
    {
        matResult.Set(null);
        var task = PrefabDatabase.GetPrefabForFilenameAsync(path);
        yield return task;
        if (task.TryGetPrefab(out var prefab))
        {
            var renderers = prefab.GetAllComponentsInChildren<Renderer>();
            foreach (var renderer in renderers)
            {
                foreach (var mat in renderer.materials)
                {
                    string strippedName = RemoveInstanceFromName(mat.name);
                    if (strippedName.Equals(matName))
                    {
                        matResult.Set(mat);
                        WeAreMurderersMain.Log.LogInfo("Found material " + strippedName + " in prefab " + path);
                        yield break;
                    }
                }
            }
        }
    }

    public static string RemoveInstanceFromName(string originalMatName)
    {
        string returnValue = originalMatName;
        if(originalMatName.EndsWith(" (Instance)"))
        {
            returnValue = originalMatName.Substring(0, originalMatName.Length - 11);
            return RemoveInstanceFromName(returnValue);
        }

        return returnValue;
    }
}