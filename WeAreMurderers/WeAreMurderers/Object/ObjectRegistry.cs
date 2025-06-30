using System.Collections;
using System.Collections.Generic;
using MurrenMods.WeAreMurderers.Entries;
using Nautilus.Assets;
using Nautilus.Assets.PrefabTemplates;
using Nautilus.Handlers;
using Nautilus.Utility;
using UnityEngine;
using UWE;

namespace MurrenMods.WeAreMurderers.Object
{
    public static class ObjectRegistry
    {
        static List<UnityEngine.Texture> _textures = new List<UnityEngine.Texture>();
        public static void RegisterChips(EntryData[] entries)
        {
            PrepareTextureBank();
            var chip0info = PrefabInfo.WithTechType("chip0", "The end of the world", "<CORRUPTED>");
            var chip0prefab = new CustomPrefab(chip0info);
            var chip0 = WeAreMurderersMain.WAMAssets.LoadAsset<GameObject>("AlienDataCPU");
            PrefabUtils.AddBasicComponents(chip0, chip0info.ClassID, chip0info.TechType, LargeWorldEntity.CellLevel.Medium);
            MaterialUtils.ApplySNShaders(chip0);
            chip0.AddComponent<Pickupable>();
            WeAreMurderersMain.Log.LogInfo(chip0.transform.childCount);
            WeAreMurderersMain.Log.LogInfo(chip0.GetComponent<Renderer>().materials);
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
            MaterialUtils.ApplySNShaders(graveobj);
            graveprefab.SetGameObject(graveobj);
            graveprefab.Register();
            
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
        
        private static IEnumerable PrepareTextureBank()
        {
            CoroutineTask<GameObject> task = CraftData.GetPrefabForTechTypeAsync(TechType.PrecursorIonCrystal);
            yield return task;
            GameObject prefab = task.GetResult();
            var mat = prefab.GetComponent<Material>();
            _textures.Add(mat.mainTexture);
        }
    }
}