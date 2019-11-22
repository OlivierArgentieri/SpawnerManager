using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class SM_Mode
{ 
    public Vector3 Position = Vector3.zero; 
    
    #region custom methods

    public abstract void Spawn(GameObject _agent);
    public abstract void Spawn(List<GameObject> _agent);
    public abstract void SpawnWithDestroyDelay(GameObject _agent, float _destroyDelay = 0);
    public abstract void SpawnWithDestroyDelay(List<GameObject> _agents, float _destroyDelay = 0);
#if UNITY_EDITOR
    public abstract void DrawSceneMode();
    public abstract void DrawSettings();
    public abstract void DrawLinkTosSpawner(Vector3 Position);
#endif

    #endregion
}