using System;
using System.Collections.Generic;
using System.IO;
using BepInEx.Logging;
using MurrenMods.WeAreMurderers.Object;
using Nautilus.Handlers;

namespace MurrenMods.WeAreMurderers.Entries
{
    public static class EntryHandler
    {
        public static void RegisterEntries(EntryData[] entries)
        {
            foreach (EntryData e in entries)
            {
                for (int i = 1; i <= e.Count; i++)
                {
                    PDAHandler.AddEncyclopediaEntry(e.Path + "_" + i, "wamlogs/" + e.Path, "fuck", "this");
                }
            }
            PDAHandler.AddEncyclopediaEntry("AlienMeasurements", "wamlogs/", "forg", "forg");
            ObjectRegistry.RegisterChips(entries);
        }
        
        public static void UnlockEntries(int languageLevel, List<string> entries)
        {
            foreach (string entry in entries)
            {
                for (int i = 1; i <= languageLevel; i++)
                {
                    PDAEncyclopedia.Add(entry + "_" + i, true);
                }
            }
        }
    }
}