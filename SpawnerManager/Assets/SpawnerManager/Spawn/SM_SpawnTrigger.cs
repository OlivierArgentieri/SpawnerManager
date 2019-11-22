using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

[Serializable]
public class SM_SpawnTrigger : MonoBehaviour
{
    #region f/p
    [SerializeField] private BoxCollider triggerZone = null;
    [SerializeField] private SM_SpawnPoint data = null;
    public bool Triggered { get; set; } = false;
    #endregion


    #region unity methods
    private void Start()
    {
        if (data == null || !data.UseTrigger || Triggered) return;
            StartCoroutine(DelayedCallback(data.SpawnDelay, TriggerSpawn));
    }

    private void OnTriggerEnter()
    {
        if (data == null || !Triggered) return;
        StartCoroutine(DelayedCallback(data.SpawnDelay, TriggerSpawn));
    }
    #endregion



    #region custom methods
    IEnumerator DelayedCallback(float _time, Action _callback)
    {
        yield return new WaitForSeconds(_time);
        _callback?.Invoke();
    }

    
    void TriggerSpawn()
    {
        if (data == null || Triggered) return;
        
        for (int i = 0; i < data.SpawnModes.Count; i++)
        {
            SM_SpawnMode _mode = data.SpawnModes[i];
            
            if(data.IsMonoAgent && data.CanDespawn)
               _mode.Mode.SpawnWithDestroyDelay(data.MonoAgent, data.DespawnDelay);
            else if(data.CanDespawn)
                _mode.Mode.SpawnWithDestroyDelay(data.Agents, data.DespawnDelay);
            
            else if(data.IsMonoAgent)
                _mode.Mode.Spawn(data.MonoAgent);
            else
                _mode.Mode.Spawn(data.Agents);
        }
        Triggered = true;
    }
    
    public void SetData(SM_SpawnPoint _data)
    {
        data = _data;
        transform.position = _data.Position;
        if (triggerZone) triggerZone.size = data.Size;
    }

    #endregion
    
#if UNITY_EDITOR
    #region debug

    private void OnDrawGizmos()
    {
        Handles.color = Color.green;
        Handles.DrawWireCube(transform.position, transform.localScale);
        Handles.color = Color.white;
    }

    #endregion
#endif
}