using System;
using UnityEngine;

namespace MurrenMods.WeAreMurderers.Entries
{
    public class EntryData
    {
        public int Count;
        public string Path;
        
        public EntryData(int count, string path)
        {
            Count = count;
            Path = path;
        }
    }
}