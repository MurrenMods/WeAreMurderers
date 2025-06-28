using Nautilus.Assets;
using Nautilus.Assets.PrefabTemplates;

namespace MurrenMods.WeAreMurderers.Object
{
    public static class ObjectRegistry
    {
        public static void RegisterChips(string[] entries)
        {
            foreach(string e in entries)
            {
                var info = PrefabInfo.WithTechType(e, e, "A small chip that can store and process data.");
                var chipprefab = new CustomPrefab(info);
                
                //temporarily use the ion cube model for chips
                var chipobj = new CloneTemplate(info, TechType.PrecursorIonCrystal);
                chipprefab.SetGameObject(chipobj);
                
                chipprefab.Register();
            }
        }
    }
}