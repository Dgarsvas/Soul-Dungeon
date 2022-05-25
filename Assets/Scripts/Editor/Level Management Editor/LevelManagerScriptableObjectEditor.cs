using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(LevelManagerScriptableObject), true)]
public class LevelManagerScriptableObjectEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        EditorGUILayout.Space();
        if (GUILayout.Button("Generate List Of Levels"))
        {
            LevelManagerScriptableObject obj = (LevelManagerScriptableObject)target;
            obj.levels.Clear();
            foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
            {
                if (scene.enabled)
                {
                    string name = scene.path.Substring(scene.path.LastIndexOf('/') + 1);
                    name = name.Substring(0, name.Length - 6);
                    obj.levels.Add(name);
                }
            }
        }
    } 
}
