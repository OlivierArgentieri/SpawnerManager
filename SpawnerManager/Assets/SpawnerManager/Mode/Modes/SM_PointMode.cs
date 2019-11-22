using System;
using System.Collections.Generic;

#if UNITY_EDITOR
using EditoolsUnity;
using UnityEditor;
#endif

using UnityEngine;

[Serializable]
public class SM_PointMode : SM_Mode
{
    #region f/p

    #endregion

    #region custom methods

    public override void Spawn(GameObject _agent)
    {
        GameObject.Instantiate(_agent, Position, Quaternion.identity);
    }
    
    public override void SpawnWithDestroyDelay(GameObject _agent)
    {
        GameObject _go = GameObject.Instantiate(_agent, Position, Quaternion.identity);
        GameObject.Destroy(_go, AutoDestroyDelay);
    }

    public override void Spawn(List<GameObject> _agents)
    {
        int _randomIndex = UnityEngine.Random.Range(0, _agents.Count);
        if (!_agents[_randomIndex]) return;

        GameObject.Instantiate(_agents[_randomIndex], Position, Quaternion.identity);
    }
    
    
    public override void SpawnWithDestroyDelay(List<GameObject> _agents)
    {
        int _randomIndex = UnityEngine.Random.Range(0, _agents.Count);
        if (!_agents[_randomIndex]) return;

        GameObject _go = GameObject.Instantiate(_agents[_randomIndex], Position, Quaternion.identity);
        GameObject.Destroy(_go, AutoDestroyDelay);
    }
    

#if UNITY_EDITOR
    public override void DrawLinkTosSpawner(Vector3 _position)
    {
        Handles.DrawDottedLine(Position, _position, 0.5f);
    }

    public override void DrawSceneMode()
    {
        EditoolsHandle.PositionHandle(ref Position, Quaternion.identity);
        
        Handles.CubeHandleCap(1, Position, Quaternion.identity, .1f, EventType.Repaint);
    }

    public override void DrawSettings()
    {
        
        EditoolsField.Toggle("Auto Destroy Agents ?", ref AutoDestroyAgent);
        if(AutoDestroyAgent)
            AutoDestroyDelay = EditorGUILayout.Slider("Auto Destroy Delay", AutoDestroyDelay, 0, 15);
    }
#endif

    #endregion
}