using System.Collections.Generic;
using MurrenMods.WeAreMurderers.Entries;
using Nautilus.Assets;
using Nautilus.Assets.PrefabTemplates;
using Nautilus.Handlers;
using UnityEngine;

namespace MurrenMods.WeAreMurderers.Object
{
    public static class ObjectRegistry
    {
        public static void RegisterChips(EntryData[] entries)
        {
            foreach(EntryData e in entries)
            {
                var info = PrefabInfo.WithTechType(e.Path, e.Path, "A small chip that can store and process data.");
                var chipprefab = new CustomPrefab(info);
                
                //temporarily use the ion cube model for chips
                var chipobj = new CloneTemplate(info, TechType.PrecursorIonCrystal);
                chipprefab.SetGameObject(chipobj);
                
                chipprefab.Register();
            }
            
            //handle spawns

            var spawns = new List<SpawnInfo>()
            {
                new SpawnInfo(entries[0].Path, new Vector3(451,-93.5f, 1176.6f)),
                new SpawnInfo(entries[1].Path, new Vector3()),
                new SpawnInfo(entries[2].Path, new Vector3()),
                new SpawnInfo(entries[3].Path, new Vector3()),
                new SpawnInfo(entries[4].Path, new Vector3()),
                new SpawnInfo(entries[5].Path, new Vector3()),
                new SpawnInfo(entries[6].Path, new Vector3()),
                new SpawnInfo(entries[7].Path, new Vector3()),
                new SpawnInfo(entries[8].Path, new Vector3()),
            };
        }
    }
}