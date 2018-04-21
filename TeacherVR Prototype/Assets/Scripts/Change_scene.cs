using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Change_scene : MonoBehaviour {

    public void GotoMainScene()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void ClickExit()

    {
        Application.Quit();
    }
    public void startgame()
    {
        GameObject varGameObject = GameObject.Find("GameController");
        varGameObject.GetComponent<EventsManager>().enabled = true;
    }
}
