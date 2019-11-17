using System;
using System.Collections.Generic;
using UnityEngine;

public class SM_SpawnerManager : MonoBehaviour
{
    #region f/p
    public List<SM_SpawnPoint> SpawnPoints = new List<SM_SpawnPoint>();
    public SM_SpawnTrigger TriggerZonePrefabs = null;
    
    #endregion

    #region unity methods

    private void Start()
    {
        SpawnAll();
    }

    #endregion


    #region custom methods

    void SpawnAll()
    {
        if (!TriggerZonePrefabs) return;
        
        for (int i = 0; i < SpawnPoints.Count; i++)
        {
            SM_SpawnTrigger _trigger = Instantiate(TriggerZonePrefabs);
            if(_trigger) _trigger.SetData(SpawnPoints[i]);
           
        }
    }
    public void AddPoint() => SpawnPoints.Add(new SM_SpawnPoint());
    public void Remove(int _index) => SpawnPoints.RemoveAt(_index);
    public void Clear() => SpawnPoints.Clear();
    

    #endregion
}