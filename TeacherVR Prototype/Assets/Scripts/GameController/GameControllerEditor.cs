using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEngine.SceneManagement;

[CustomEditor(typeof(GameController))]
public class GameControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Restart game"))
        {
            if(Application.isPlaying) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            else Debug.LogWarning("Application is not in play mode");
        }
    }
}