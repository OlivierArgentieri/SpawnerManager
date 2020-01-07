using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SM_SpawnPoint
{
    #region f/p
    public bool IsVisible = true;
    public List<SM_SpawnMode> SpawnModes = new List<SM_SpawnMode>();

    public bool IsMonoAgent = false;
    public GameObject MonoAgent = null;
    public List<GameObject> Agents = new List<GameObject>();
    
    public Vector3 Position = Vector3.zero;
    public Vector3 Size = Vector3.one;
    public bool UseTrigger = true;
    public bool UseDelay = false;
    public float SpawnDelay = 0;

    public void AddAgent() => Agents.Add(null);
    public void RemoveAgent(int _index) => Agents.RemoveAt(_index);
    public void RemoveAgent() => MonoAgent = null;
    public void ClearAgents() => Agents.Clear();
    #endregion

    #region custom methods
    public void AddMode() => SpawnModes.Add(new SM_SpawnMode());
    public void RemoveMode(int _index) => SpawnModes.RemoveAt(_index);
    public void ClearModes() => SpawnModes.Clear();

    #endregion
}