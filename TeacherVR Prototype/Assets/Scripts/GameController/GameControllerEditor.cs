#if (UNITY_EDITOR)
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameController))]
public class GameControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GameController myScript = (GameController)target;

        if (GUILayout.Button("Restart game"))
        {
            if(Application.isPlaying) myScript.RestartGame();
            else Debug.LogWarning("Application is not in play mode");
        }
    }
}
#endif