using System.Collections;
using System.Collections.Generic;
using MurrenMods.WeAreMurderers.Entries;
using Nautilus.Assets;
using Nautilus.Assets.PrefabTemplates;
using Nautilus.Handlers;
using Nautilus.Utility;
using UnityEngine;
using UnityEngine.Experimental.AssetBundlePatching;
using UWE;

namespace MurrenMods.WeAreMurderers.Object
{
    public static class ObjectRegistry
    {
        static List<UnityEngine.Material> _mats = new List<UnityEngine.Material>();

        private static void RegisterChips(EntryData[] entries)
        {
            var chip0info = PrefabInfo.WithTechType("chip0", "The end of the world", "<CORRUPTED>");
            var chip0prefab = new CustomPrefab(chip0info);
            var chip0 = WeAreMurderersMain.WAMAssets.LoadAsset<GameObject>("AlienDataCPU");
            var chip0renderer = chip0.GetComponentInChildren<Renderer>();
            PrefabUtils.AddBasicComponents(chip0, chip0info.ClassID, chip0info.TechType, LargeWorldEntity.CellLevel.Medium);
            chip0renderer.materials[1] = _mats[0];
            chip0renderer.materials[1].CopyPropertiesFromMaterial(_mats[0]);
            foreach (var e in _mats[0].GetTexturePropertyNameIDs())
            {
                chip0renderer.materials[1].SetTexture(e, _mats[0].GetTexture(e));
            }
            chip0renderer.materials[1].shader = _mats[0].shader;
            chip0renderer.materials[0] = _mats[1];
            chip0renderer.materials[0].CopyPropertiesFromMaterial(_mats[1]);
            foreach (var e in _mats[1].GetTexturePropertyNameIDs())
            {
                chip0renderer.materials[0].SetTexture(e, _mats[1].GetTexture(e));
            }
            chip0renderer.materials[0].shader = _mats[1].shader;
            chip0.AddComponent<Pickupable>();
            chip0.SetActive(true);
            chip0prefab.SetGameObject(chip0);
            chip0prefab.Register();
            foreach(EntryData e in entries)
            {
                var info = PrefabInfo.WithTechType(e.Path, e.Path, "A small chip that can store and process data.");
                var chipprefab = new CustomPrefab(info);
                var chipobj = new CloneTemplate(info, "chip0");
                chipprefab.SetGameObject(chipobj);
                chipprefab.Register();
            }
            
            var graveinfo = PrefabInfo.WithTechType("QX-VR_Grave", "QX-VR_Grave", "A grave with a small device lodged on where the head would be");
            var graveprefab = new CustomPrefab(graveinfo);
            var graveobj = WeAreMurderersMain.WAMAssets.LoadAsset<GameObject>("AlienGrave");
            graveobj.SetActive(true);
            PrefabUtils.AddBasicComponents(graveobj, graveinfo.ClassID, graveinfo.TechType, LargeWorldEntity.CellLevel.Medium);
            graveprefab.SetGameObject(graveobj);
            graveprefab.Register();
            var graverenderer = graveobj.GetComponentInChildren<Renderer>();
            graverenderer.materials[1] = _mats[0];
            graverenderer.materials[1].CopyPropertiesFromMaterial(_mats[0]);
            foreach (var e in _mats[0].GetTexturePropertyNameIDs())
            {
                graverenderer.materials[1].SetTexture(e, _mats[0].GetTexture(e));
            }
            graverenderer.materials[1].shader = _mats[0].shader;
            
            CoordinatedSpawnsHandler.RegisterCoordinatedSpawn(new SpawnInfo("QX-VR_Grave", new Vector3(348f, 155.4f, 906.4f), new Vector3(-118, -22, -90), new Vector3(45, 45, 45)));

            var align = new Vector3(90, 0, 0);
            var scale = new Vector3(30, 30, 30);
            var spawns = new List<SpawnInfo>()
            {
                new SpawnInfo(entries[0].Path, new Vector3(449,-93.78f, 1176.6f), align, scale),//DONE
                new SpawnInfo(entries[1].Path, new Vector3(-881.3f, -309.95f, -811.3f), align, scale),//DONE
                new SpawnInfo(entries[2].Path, new Vector3(-1130.3f, -687.4f, -688.4f), align, scale),//DONE
                new SpawnInfo(entries[3].Path, new Vector3(-255.7f, -813.5f, 294.7f), align, scale),//DONE
                new SpawnInfo(entries[4].Path, new Vector3(-641.4f, -562.5f, 1480.2f), align, scale),//DONE
                new SpawnInfo(entries[5].Path, new Vector3(-1220.3f, -399.9f, 1059.3f), align, scale),//DONE
                new SpawnInfo(entries[6].Path, new Vector3(-28.3f, -1220.15f, 113), align, scale),//DONE
                new SpawnInfo(entries[7].Path, new Vector3(324.8f, -1448.7f, -454.4f), align, scale),//DONE
                new SpawnInfo(entries[8].Path, new Vector3(347.8f, 155.75f, 906.8f), new Vector3(-118, -22, -90), scale),//DONE
            };
            
            CoordinatedSpawnsHandler.RegisterCoordinatedSpawns(spawns);
            
            WeAreMurderersMain.Log.LogInfo("Successfully registered and spawned objects..");
        }
        
        public static IEnumerator PrepareTextureBankAndRegisterChips(EntryData[] entries)
        {
            CoroutineTask<GameObject> ioncubetask = CraftData.GetPrefabForTechTypeAsync(TechType.PrecursorIonCrystal);
            WeAreMurderersMain.Log.LogInfo("Attempting to load texture bank.");
            yield return ioncubetask;
            GameObject ioncubeprefab = ioncubetask.GetResult();
            var ioncuberend = ioncubeprefab.GetComponentInChildren<Renderer>();
            _mats.Add(new Material(ioncuberend.sharedMaterials[0]));

            _mats.Add(new Material(ioncuberend.sharedMaterials[0]));
            
            RegisterChips(entries);
        }
    }
}