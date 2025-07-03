using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Nautilus.FMod;
using Nautilus.Utility;
using UnityEngine;

namespace MurrenMods.WeAreMurderers;

public class ResourceManager
{
    private bool _preparedSounds = false;
    public AssetBundle MainBundle { get; private set; }

    public List<Material> MaterialBank { get; private set; } = new();

    public bool LoadedMaterials { get; private set; } = false;
    
    public void LoadMainBundle()
    {
        if (MainBundle != null)
        {
            WeAreMurderersMain.Log.LogWarning("Attempted to load main bundle when it was already loaded. Skipping.");
            return;
        }
        MainBundle = AssetBundleLoadingUtils.LoadFromAssetsFolder(Assembly.GetExecutingAssembly(), "wearemurderers_res");
        if (MainBundle == null)
        {
            WeAreMurderersMain.Log.LogError("Failed to load main asset bundle. Please ensure the asset bundle is present in the mod folder.");
        }
        else
        {
            WeAreMurderersMain.Log.LogInfo("Main asset bundle loaded successfully.");
        }
    }

    public IEnumerator PrepareClonedMaterials()
    {
        if (LoadedMaterials)
        {
            WeAreMurderersMain.Log.LogWarning("Attempted to prepare cloned materials when they were already prepared. Reloading material bank.");
            LoadedMaterials = false;
            MaterialBank.Clear();
        }

        CoroutineTask<GameObject> ioncubetask = CraftData.GetPrefabForTechTypeAsync(TechType.PrecursorIonCrystal);
        WeAreMurderersMain.Log.LogInfo("Attempting to load material bank.");
        yield return ioncubetask;
        GameObject ioncubeprefab = ioncubetask.GetResult();
        var ioncuberend = ioncubeprefab.GetComponentInChildren<Renderer>();
        MaterialBank.Add(new Material(ioncuberend.sharedMaterials[0]));

        var matTask = new TaskResult<Material>();
        yield return Utility.MaterialUtility.FindMaterialFromPath("Assets/Models/chassis/precursor/Materials/precursor_interior_tiles_00.mat", matTask);
        var alienTiles = matTask.Get();
        MaterialBank.Add(alienTiles);
            
        var matTask2 = new TaskResult<Material>();
        yield return Utility.MaterialUtility.FindMaterialFromPrefabPath("precursor_interior_tiles_12_moon_pool", "WorldEntities/Doodads/Precursor/TempGun_Interiors/Precursor_Gun_MoonPool.prefab", matTask2);
        var alienTiles2 = matTask2.Get();
        MaterialBank.Add(alienTiles2);
        WeAreMurderersMain.Log.LogInfo("Loaded material bank successfully.");
        LoadedMaterials = true;
    }

    public void LoadSounds()
    {
        if (_preparedSounds)
        {
            WeAreMurderersMain.Log.LogWarning("Attempted to load sounds when they were already prepared. Skipping.");
            return;
        }
        WeAreMurderersMain.Log.LogInfo("Loading sound bank...");
        
        CustomSoundSourceBase source = new AssetBundleSoundSource(MainBundle);
        FModSoundBuilder builder = new FModSoundBuilder(source);
        
        builder.CreateNewEvent("languagelevel", "bus:/master/SFX_for_pause/PDA_pause/all/all voice/AI voice")
            .SetMode2D()
            .SetSound("languagelevelupgrade")
            .Register();
        
        builder.CreateNewEvent("invalidchip9", "bus:/master/SFX_for_pause/PDA_pause/all/all voice/AI voice")
            .SetMode2D()
            .SetSound("chip9")
            .Register();
        
        builder.CreateNewEvent("thankyou", "bus:/master/SFX_for_pause/PDA_pause/all/all voice/AI voice")
            .SetMode2D()
            .SetSound("thankyou")
            .Register();
        
        WeAreMurderersMain.Log.LogInfo("Sounds loaded successfully.");
        _preparedSounds = true;
    }


}