using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EditoolsUnity;
using UnityEditor;

[CustomEditor(typeof(SM_SpawnerManager))]
public class SM_SpawnerToolsEditor : EditorCustom<SM_SpawnerManager>
{
    protected override void OnEnable()
    {
        base.OnEnable(); 
        Tools.current = Tool.None;
    }

    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        EditoolsBox.HelpBoxInfo("SPAWN TOOL V1");
        eTarget.TriggerZonePrefabs = (SM_SpawnTrigger) EditoolsField.ObjectField(eTarget.TriggerZonePrefabs, typeof(SM_SpawnTrigger), false);
        if (!eTarget.TriggerZonePrefabs) return;
        
        EditoolsLayout.Space(1);
        DrawnSpawnPointsUI();

        SceneView.RepaintAll();
    }

    private void OnSceneGUI()
    {
        if (!eTarget.TriggerZonePrefabs) return;

        DrawSpawnPointScene();
    }

    void DrawnSpawnPointsUI()
    {
        EditoolsLayout.Horizontal(true);
        EditoolsBox.HelpBoxInfo("Add Spawn Point");
        
        EditoolsLayout.Vertical(true);
        EditoolsButton.Button("+",Color.green , eTarget.AddPoint);
        EditoolsButton.ButtonWithConfirm("#",Color.red , eTarget.Clear, "Remove All ?", "Remove All Point ?" , _showCondition:eTarget.SpawnPoints.Count > 0);
        EditoolsLayout.Vertical(false); 
        
        EditoolsLayout.Horizontal(false);

        
        EditoolsLayout.Space(2);

        for (int i = 0; i < eTarget.SpawnPoints.Count; i++)
        {

            EditoolsLayout.Horizontal(true);
            EditoolsBox.HelpBox($"SpawnPoint {i+1}");
            EditoolsButton.ButtonWithConfirm("X", Color.red, eTarget.Remove, i, "Remove Point ?", "Remove This Point ?");

            EditoolsLayout.Horizontal(false);

            if (i > eTarget.SpawnPoints.Count - 1) return;
            
            SM_SpawnPoint _point = eTarget.SpawnPoints[i];
            EditoolsLayout.Foldout(ref _point.IsVisible, "Show/Hide");
            
            if(!_point.IsVisible) continue;

            EditoolsField.Vector3Field("Position", ref _point.Position); 
            EditoolsField.Vector3Field("Size", ref _point.Size);

            EditoolsLayout.Space(1);
            
            DrawSpawnModeUI(_point);
           
            DrawnAgentUI(_point);
        }
    }


    void DrawnAgentUI(SM_SpawnPoint _point)
    {
        EditoolsField.Toggle("Unique Agent? ", ref _point.IsMonoAgent);
        EditoolsLayout.Space(5);

        if (_point.IsMonoAgent)
        {
            EditoolsLayout.Horizontal(true);
            _point.MonoAgent = (GameObject) EditoolsField.ObjectField(_point.MonoAgent, typeof(GameObject), false);
            EditoolsButton.ButtonWithConfirm("X", Color.red, _point.RemoveAgent, "Remove Agent", "Remove Agent?");
            EditoolsLayout.Horizontal(false);

        }
        else
        {
            EditoolsLayout.Horizontal(true);
            EditoolsBox.HelpBox($"Add agent to spawn");
            EditoolsLayout.Vertical(true);
            EditoolsButton.Button("Add Agent", Color.cyan, _point.AddAgent);
            EditoolsButton.ButtonWithConfirm("#", Color.red, _point.ClearAgents, "Clear Agents", "Clear All Agents ?",
                _showCondition: _point.Agents.Count > 0);
            EditoolsLayout.Vertical(false);
            EditoolsLayout.Horizontal(false);

            for (int j = 0; j < _point.Agents.Count; j++)
            {
                EditoolsLayout.Horizontal(true);
                _point.Agents[j] = (GameObject) EditoolsField.ObjectField(_point.Agents[j], typeof(GameObject), false);
                EditoolsButton.ButtonWithConfirm("X", Color.red, _point.RemoveAgent, j, "Remove Agent ?",
                    "Remove This Agent ?");
                EditoolsLayout.Horizontal(false);
            }
        }
    }
    
    void DrawModeSettingsUI(SM_SpawnMode _mode)
    {
        _mode.Mode.DrawSettings();
    }
    
    void DrawSpawnPointScene()
    {
        for (int i = 0; i < eTarget.SpawnPoints.Count; i++)
        {
            SM_SpawnPoint _point = eTarget.SpawnPoints[i];

            EditoolsHandle.SetColor(Color.green);
            EditoolsHandle.DrawWireCube(_point.Position, _point.Size);
            EditoolsHandle.SetColor(Color.white);
            
            EditoolsHandle.PositionHandle(ref _point.Position, Quaternion.identity);
            EditoolsHandle.ScaleHandle(ref _point.Size, _point.Position, Quaternion.identity, 2);
            EditoolsLayout.Space();
            
            GetModeScene(_point);
        }
        
    }


    void DrawSpawnModeUI(SM_SpawnPoint _point)
    {
        EditoolsLayout.Horizontal(true);
        EditoolsBox.HelpBoxInfo("Add Spawn Mode");
        
        EditoolsLayout.Vertical(true);
        EditoolsButton.Button("+", Color.green, _point.AddMode);
        EditoolsButton.ButtonWithConfirm("#",Color.red , _point.ClearModes, "Remove All ?", "Remove All Mode ?", _showCondition: _point.SpawnModes.Count > 0);
        EditoolsLayout.Vertical(false); 
        
        EditoolsLayout.Horizontal(false);
        for (int i = 0; i < _point.SpawnModes.Count; i++)
        {
            SM_SpawnMode _mode = _point.SpawnModes[i];
            
            EditoolsLayout.Horizontal(true);
            _mode.Type = (SM_SpawnType) EditoolsField.EnumPopup("Mode Type", _mode.Type);
            EditoolsButton.ButtonWithConfirm("X", Color.red, _point.RemoveMode, i, "Remove Mode ?", "Remove This Mode ?");
            EditoolsLayout.Horizontal(false);
            DrawModeSettingsUI(_mode);
        }
    }

    void GetModeScene(SM_SpawnPoint _point)
    {
        for (int i = 0; i < _point.SpawnModes.Count; i++)
        {
            SM_SpawnMode _mode = _point.SpawnModes[i];
            DrawModeScene(_mode, _point);
        }
    }

    void DrawModeScene(SM_SpawnMode _mode, SM_SpawnPoint _point)
    {
        _mode.Mode.DrawLinkTosSpawner(_point.Position);
        _mode.Mode.DrawSceneMode();
    }
}
