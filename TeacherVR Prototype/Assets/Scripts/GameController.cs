using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    #region Singleton

    public static GameController Instance;

    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("More than one intence of GameController found!");
            return;
        }
        Instance = this;
    }

    #endregion

    public EventsManager EventsManager;
    public GameObject RysObject;

    void Start () {
		
	}
	

	void Update () {
		
	}
    
}
