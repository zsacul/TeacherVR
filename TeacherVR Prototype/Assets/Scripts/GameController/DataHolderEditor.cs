using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DataHolder))]
public class DataHolderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        DataHolder myScript = (DataHolder)target;

        if (GUILayout.Button("Save game settings"))
        {
            if (Application.isPlaying) myScript.SaveData();
            else Debug.LogWarning("Application is not in play mode");
        }

        if (GUILayout.Button("Load game settings"))
        {
            if (Application.isPlaying) myScript.LoadData();
            else Debug.LogWarning("Application is not in play mode");
        }

        if (GUILayout.Button("Reset all game settings"))
        {
            if (Application.isPlaying) myScript.ResetAllData();
            else Debug.LogWarning("Application is not in play mode");
        }
    }
}