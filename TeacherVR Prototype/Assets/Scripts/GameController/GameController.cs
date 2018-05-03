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
    [Tooltip("One round time")]
    public int GameTime = 5;
    public ForceTeleportScript ForceTeleportScript;

    private bool GameInProgress = false;

    void Start()
    {
        //VRTK_SDKManager.instance.scriptAliasRightController = rightScriptAlias;
        //VRTK_SDKManager.instance.scriptAliasLeftController = leftScriptAlias;
        ScoreBoard.GameOver += ScoreBoard_GameOver;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void OnDestroy()
    {
        ScoreBoard.GameOver -= ScoreBoard_GameOver;
    }

    private void ScoreBoard_GameOver()
    {
        GameInProgress = false;
        MessageSystem.HideAllButtons();
        MessageSystem.HideAllWindows();
        EventsManager.EndAllEvents();
        ForceTeleportScript.ForceTeleportToGameSummary();
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

    public bool IsGameInProgress()
    {
        return GameInProgress;
    }

    public void StartGame()
    {
        GameInProgress = true;
    }
}