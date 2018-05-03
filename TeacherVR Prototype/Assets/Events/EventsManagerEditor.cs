using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(EventsManager))]
public class EventsManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        EventsManager myScript = (EventsManager) target;

        if (GUILayout.Button("Start next event"))
        {
            if (Application.isPlaying) myScript.StartNextEvent();
            else Debug.LogWarning("Application is not in play mode");
        }
        
        if (GUILayout.Button("Restart events"))
        {
            if (Application.isPlaying) myScript.Restart();
            else Debug.LogWarning("Application is not in play mode");
        }
    }
}