using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
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
    public bool Tooltips = true;
    public Vector2[] TemplateShape;
    public Color ChalkColor;
    public ScoreBoard ScoreBoard;
    public MessageSystem MessageSystem;
    public SoundManager SoundManager;
    public Particles Particles;

    void Start()
    {
    }


    void Update()
    {
    }
}