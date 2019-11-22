﻿using System;
using System.Collections;
using System.Collections.Generic;
 using System.Security.Cryptography;
 using UnityEngine;
 using Random = System.Random;
#if UNITY_EDITOR
using EditoolsUnity;
using UnityEditor;
#endif

[Serializable]
public class SM_CircleMode : SM_Mode
{
    #region f/p
    
    public int Radius = 5;
    public int AgentNumber = 10;
    #endregion

    
    
    #region custom methods
    IEnumerator DelayedCallback(float _time, Action _callback)
    {
        yield return new WaitForSeconds(_time);
        _callback?.Invoke();
    }
    
    public override void Spawn(GameObject _agent)
    {
        for (int i = 0; i < AgentNumber; i++)
        {
            GameObject.Instantiate(_agent, GetRadiusPosition(i, AgentNumber, Radius, Position), Quaternion.identity);
        } 
    }
    public override void SpawnWithDestroyDelay(GameObject _agent)
    {
        for (int i = 0; i < AgentNumber; i++)
        {
            GameObject _go = GameObject.Instantiate(_agent, GetRadiusPosition(i, AgentNumber, Radius, Position), Quaternion.identity);
            GameObject.Destroy(_go, AutoDestroyDelay);
        } 
    }

    public override void SpawnWithDestroyDelay(List<GameObject> _agents)
    {
        for (int i = 0; i < AgentNumber; i++)
        {
            int _randomIndex = UnityEngine.Random.Range(0, _agents.Count);
            if (!_agents[_randomIndex]) continue;
            GameObject _go = GameObject.Instantiate(_agents[_randomIndex], GetRadiusPosition(i, AgentNumber, Radius, Position), Quaternion.identity);
            GameObject.Destroy(_go, AutoDestroyDelay);
        }
    }
    
    public override void Spawn(List<GameObject> _agents)
    {
        for (int i = 0; i < AgentNumber; i++)
        {
            int _randomIndex = UnityEngine.Random.Range(0, _agents.Count);
            if (!_agents[_randomIndex]) continue;
            GameObject.Instantiate(_agents[_randomIndex], GetRadiusPosition(i, AgentNumber, Radius, Position), Quaternion.identity);
        }
    }
    
    public Vector3 GetRadiusPosition(int _pos, int _maxPos, int _radius, Vector3 _center)
    {
        float _angle = (float)_pos / _maxPos * Mathf.PI * 2;

        float _x = _center.x + Mathf.Cos(_angle) * _radius;
        float _y = _center.y;
        float _z = _center.z + Mathf.Sin(_angle) * _radius;
        
        return new Vector3(_x, _y, _z);
    }
    #endregion

    #if UNITY_EDITOR
    public override void DrawSettings()
    {
        //Radius = EditorGUILayout.IntSlider("Radius", Radius, 1, 100);
        //AgentNumber = EditorGUILayout.IntSlider("Radius", AgentNumber, 1, 100);
        EditoolsField.IntSlider("Radius", ref Radius, 1, 100);
        EditoolsField.IntSlider("Agent Number", ref AgentNumber, 1, 50);

        EditoolsField.Toggle("Auto Destroy Agents ?", ref AutoDestroyAgent);
        if(AutoDestroyAgent)
            AutoDestroyDelay = EditorGUILayout.Slider("Auto Destroy Delay", AutoDestroyDelay, 0, 15);

    }
    public override void DrawLinkTosSpawner(Vector3 _position) => Handles.DrawDottedLine(Position, _position, 0.5f);
    
    public override void DrawSceneMode()
    {
        EditoolsHandle.PositionHandle(ref Position, Quaternion.identity);
       
        Handles.DrawWireDisc(Position, Vector3.up, Radius);
        for (int i = 0; i < AgentNumber; i++)
        {
            Handles.CubeHandleCap(i, GetRadiusPosition(i, AgentNumber, Radius, Position), Quaternion.identity, .1f, EventType.Repaint);
        }
    }
    #endif
}
