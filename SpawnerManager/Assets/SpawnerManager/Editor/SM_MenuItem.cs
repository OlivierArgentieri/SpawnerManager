using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SM_MenuItem 
{
    #region f/p
    #endregion
    
    #region custom methods

    [MenuItem("SpawnTool/SpawnerManager")]
    public static void Init()
    {
        SM_SpawnerManager[] _spawnerManagers = Object.FindObjectsOfType<SM_SpawnerManager>();

        if (_spawnerManagers.Length > 0) return;
        GameObject _spawnerManager = new GameObject("SpawnerManager", typeof(SM_SpawnerManager));
    }
    #endregion
}
