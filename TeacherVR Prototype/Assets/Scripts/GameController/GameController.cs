using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    [Tooltip("The GameObject to inject into the VRTK SDK Manager as the Left Controller Script Alias.")]
    public GameObject leftScriptAlias;
    [Tooltip("The GameObject to inject into the VRTK SDK Manager as the Right Controller Script Alias.")]
    public GameObject rightScriptAlias;
    void Start()
    {
        //VRTK_SDKManager.instance.scriptAliasRightController = rightScriptAlias;
        //VRTK_SDKManager.instance.scriptAliasLeftController = leftScriptAlias;
        ScoreBoard.GameOver += ScoreBoard_GameOver;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            //VRTK_SDKManager.instance.UnloadSDKSetup();
            SceneManager.LoadScene( SceneManager.GetActiveScene().buildIndex);
        }
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