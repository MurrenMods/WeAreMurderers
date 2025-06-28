using System.Collections.Generic;
using System.IO;
using Nautilus.Handlers;

namespace MurrenMods.WeAreMurderers.Entries
{
    public class EntryHandler
    {
        private EntryData[] _entries;

        public void RegisterEntries()
        {
            LanguageHandler.SetLanguageLine("EncyPath_wamlogs/", "Alien Logs");
            foreach (EntryData e in _entries)
            {
                LanguageHandler.SetLanguageLine("wamlogs/" + e.Path, e.Path);
                for (int i = 1; i <= e.Count; i++)
                {
                    PDAHandler.AddEncyclopediaEntry(e.Path + "_" + i, "wamlogs/" + e.Path, null, null);
                }
            }
        }
        
        public void UnlockEntries(int languageLevel, List<string> entries)
        {
            foreach (string entry in entries)
            {
                for (int i = 1; i <= languageLevel; i++)
                {
                    PDAEncyclopedia.Add("wamlogs/" + entry + "_" + i, true);
                }
            }
        }
    }
}