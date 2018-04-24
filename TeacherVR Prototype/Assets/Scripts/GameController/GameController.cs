using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using VRTK.Examples;

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
    public DrawingManager DrawingManager;
    public bool Tooltips = true;
    public bool Messages = true;
    public VRTK_Pointer TeleportL;
    public VRTK_Pointer TeleportR;
    public VRTK_MoveInPlace MoveInPlace;
    public ScoreBoard ScoreBoard;
    public MessageSystem MessageSystem;
    public SoundManager SoundManager;
    public Particles Particles;
    public MicInput MicInput;
    public StudentsRefs Students;
    public MenuScript[] MenuScripts;
    public int GameTime = 3;

    void Start()
    {
        ScoreBoard.GameOver += ScoreBoard_GameOver;
    }

    void OnDestroy()
    {
        ScoreBoard.GameOver -= ScoreBoard_GameOver;
    }

    private void ScoreBoard_GameOver()
    {
        MenuScripts[0].TeleportToMenu();
        foreach (MenuScript menu in MenuScripts)
        {
            menu.DoAction();
        }
    }

    public void ChangeTooltips()
    {
        if (Tooltips) Tooltips = false;
        else Tooltips = true;
    }

    public void ChangeLocomotion(bool UseArmSwinger)
    {
        if (UseArmSwinger)
        {
            MoveInPlace.enabled = true;
            TeleportL.enabled = false;
            TeleportR.enabled = false;
        }
        else
        {
            MoveInPlace.enabled = false;
            TeleportL.enabled = true;
            TeleportR.enabled = true;
        }
    }
}