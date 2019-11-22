using System;
using System.Collections.Generic;
using System.Net;
#if UNITY_EDITOR
using EditoolsUnity;
using UnityEditor;
#endif
using UnityEngine;

[Serializable]
public class SM_LineMode : SM_Mode
{
    #region f/p
    public Vector3 EndPosition = Vector3.one;
    public int AgentNumber = 10;

    #endregion

    #region custom methods

    public override void Spawn(GameObject _agent)
    {
        if (!_agent) return;
        for (int i = 0; i < AgentNumber; i++)
        {
            GameObject.Instantiate(_agent, GetPositionOnLine(i, AgentNumber, Position, EndPosition), Quaternion.identity);
        }
    }
    
    public override void SpawnWithDestroyDelay(GameObject _agent, float _destroyDelay)
    {
        if (!_agent) return;
        for (int i = 0; i < AgentNumber; i++)
        {
            GameObject _go = GameObject.Instantiate(_agent, GetPositionOnLine(i, AgentNumber, Position, EndPosition), Quaternion.identity);
            GameObject.Destroy(_go, _destroyDelay);
        }
    }

    public override void Spawn(List<GameObject> _agents)
    {
        for (int i = 0; i < AgentNumber; i++)
        {
            int _randomIndex = UnityEngine.Random.Range(0, _agents.Count);
            if (!_agents[_randomIndex]) continue;
            GameObject.Instantiate(_agents[_randomIndex], GetPositionOnLine(i, AgentNumber, Position, EndPosition), Quaternion.identity);
        }
    }
    
    public override void SpawnWithDestroyDelay(List<GameObject> _agents, float _destroyDelay)
    {
        for (int i = 0; i < AgentNumber; i++)
        {
            int _randomIndex = UnityEngine.Random.Range(0, _agents.Count);
            if (!_agents[_randomIndex]) continue;
            GameObject _go = GameObject.Instantiate(_agents[_randomIndex], GetPositionOnLine(i, AgentNumber, Position, EndPosition), Quaternion.identity);
            GameObject.Destroy(_go, _destroyDelay);
        }
    }


   

    Vector3 GetPositionOnLine(int _pos, int _maxPos, Vector3 _position, Vector3 _endPosition)
    {
        return Vector3.Lerp(_position, _endPosition, (float)_pos/_maxPos);
    }


    #if UNITY_EDITOR
    public override void DrawLinkTosSpawner(Vector3 _position)
    {
        Handles.DrawDottedLine(Position, _position, 0.5f);
    }
    public override void DrawSceneMode()
    {
        EditoolsHandle.PositionHandle(ref Position, Quaternion.identity);
        EditoolsHandle.PositionHandle(ref EndPosition, Quaternion.identity);
        
        Handles.DrawLine(Position, EndPosition);

        for (int i = 0; i < AgentNumber; i++)
        {
            Handles.CubeHandleCap(i, GetPositionOnLine(i, AgentNumber,  Position, EndPosition), Quaternion.identity, .1f, EventType.Repaint);
        }
    }

    public override void DrawSettings()
    {
        EditoolsField.IntSlider("Agent Number", ref AgentNumber, 1, 100);
        EndPosition = EditoolsField.Vector3Field("End Position", EndPosition);
        Position = EditoolsField.Vector3Field("Start Position", Position);
    }
    #endif
#endregion


}