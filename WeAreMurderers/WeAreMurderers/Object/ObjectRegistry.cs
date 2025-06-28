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
                
                //temporarily use the ion cube model for chips TODO: replace with custom model
                var chipobj = new CloneTemplate(info, TechType.Lead);
                chipprefab.SetGameObject(chipobj);
                
                chipprefab.Register();
            }
            
            //handle spawns
            //TODO: Add grave

            var spawns = new List<SpawnInfo>()
            {
                new SpawnInfo(entries[0].Path, new Vector3(451,-94f, 1176.6f)),
                new SpawnInfo(entries[1].Path, new Vector3(-882, -309.8f, -811.3f)),
                new SpawnInfo(entries[2].Path, new Vector3(-1130.3f, -687, -688.4f)),
                new SpawnInfo(entries[3].Path, new Vector3(-255.7f, -813.5f, 294.7f)),
                new SpawnInfo(entries[4].Path, new Vector3(-641.4f, -562, 1480.2f)),
                new SpawnInfo(entries[5].Path, new Vector3(-1220.3f, -399.5f, 1059.3f)),
                new SpawnInfo(entries[6].Path, new Vector3(-28.3f, -1220.2f, 113)),
                new SpawnInfo(entries[7].Path, new Vector3(324.8f, -1448.8f, -454.4f)),
                new SpawnInfo(entries[8].Path, new Vector3(347.2f, 156, 908.5f)),
            };
            
            CoordinatedSpawnsHandler.RegisterCoordinatedSpawns(spawns);
        }
    }
}