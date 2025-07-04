﻿using System.Collections.Generic;
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
            
            PDAHandler.AddLogEntry("languagelevel", "languagelevel", Nautilus.Utility.AudioUtils.GetFmodAsset("languagelevel"));
            PDAHandler.AddLogEntry("invalidchip9", "invalidchip9", Nautilus.Utility.AudioUtils.GetFmodAsset("invalidchip9"));
            PDAHandler.AddLogEntry("thankyou", "thankyou", Nautilus.Utility.AudioUtils.GetFmodAsset("thankyou"));
        }
        
        public static void UnlockEntries(int languageLevel, List<string> entries)
        {
            foreach (string entry in entries)
            {
                for (int i = 1; i <= languageLevel; i++)
                {
                    PDAEncyclopedia.Add(entry + "_" + i, i == languageLevel);
                }
            }
        }
    }
}