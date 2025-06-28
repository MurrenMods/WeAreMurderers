using System;
using System.Collections.Generic;
using MurrenMods.WeAreMurderers.Entries;

namespace MurrenMods.WeAreMurderers
{
    public static class Data
    {
        public static int FoundChips = 0;
        public static int LanguageLevel => Math.Max(1, (FoundChips + 1) / 2);

        public static List<string> unlockedEntries = new List<string>();
        
        public static EntryData[] Entries
        {
            get
            {
                return new EntryData[]
                {
                    new EntryData(5, "QX-VR_log1"),
                    new EntryData(5, "QX-VR_log2"),
                    new EntryData(5, "QX-VR_log3"),
                    new EntryData(5, "QX-VR_log4"),
                    new EntryData(5, "QX-VR_log5"),
                    new EntryData(5, "QX-VR_log6"),
                    new EntryData(5, "QX-VR_log7"),
                    new EntryData(5, "QX-VR_log8"),
                    new EntryData(5, "QX-VR_log9"),
                };
            }
        }
    }
}