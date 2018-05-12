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
        Application.targetFrameRate = 90;
    }

    #endregion

    public DataHolder DataHolder;
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
    [Tooltip("One round time")] public int GameTime = 5;
    public ForceTeleportScript ForceTeleportScript;

    private bool GameInProgress = false;

    void Start()
    {
        StartCoroutine(LoadData());
        ScoreBoard.GameOver += ScoreBoard_GameOver;
    }

    public void RestartGame()
    {
        StopAllCoroutines();
        DataHolder.SaveData();
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadData()
    {
        yield return new WaitForEndOfFrame();
        DataHolder.LoadData();
    }

    IEnumerator LoadScene()
    {
        yield return new WaitForEndOfFrame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void OnDestroy()
    {
        ScoreBoard.GameOver -= ScoreBoard_GameOver;
    }

    private void ScoreBoard_GameOver(ScoreBoard.WhatOver over)
    {
        GameInProgress = false;
        MessageSystem.HideAllButtons();
        MessageSystem.HideAllWindows();
        EventsManager.EndAllEvents();
        ForceTeleportScript.ForceTeleportToGameSummary();
        if (over == ScoreBoard.WhatOver.TopPointsTime)
        {
            MessageSystem.ShowCustomText("Congratulations!\n\nYou are in Top 5!", MessageSystem.Window.W800H400);
            StartCoroutine(Hide(7, MessageSystem.Window.W800H400));
        }
        if (over == ScoreBoard.WhatOver.TopPointsEvents)
        {
            MessageSystem.ShowCustomText("Congratulations!\n\nYou have done all the events!\n\nYou are in Top 5!", MessageSystem.Window.W800H400);
            StartCoroutine(Hide(7, MessageSystem.Window.W800H400));
        }
        if (over == ScoreBoard.WhatOver.Events)
        {
            MessageSystem.ShowCustomText("Congratulations!\n\nYou have done all the events!", MessageSystem.Window.W800H400);
            StartCoroutine(Hide(7, MessageSystem.Window.W800H400));
        }
        if (over == ScoreBoard.WhatOver.Time)
        {
            MessageSystem.ShowCustomText("End of Time!\n\nTry again to beat your record!", MessageSystem.Window.W800H400);
            StartCoroutine(Hide(7, MessageSystem.Window.W800H400));
        }
        DataHolder.SaveData();
    }

    private IEnumerator Hide(float time, MessageSystem.Window window)
    {
        yield return new WaitForSeconds(time);
        MessageSystem.HideWindow(window);
    }

    public void ChangeTooltips()
    {
        if (Tooltips) Tooltips = false;
        else Tooltips = true;
    }

    public void ChangeLocomotion(bool UseArmSwinger)
    {
        StartCoroutine(RestartLocomotion(UseArmSwinger));
    }

    private IEnumerator RestartLocomotion(bool UseArmSwinger)
    {
        yield return new WaitForEndOfFrame();
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
        yield return new WaitForEndOfFrame();
        MoveInPlace.enabled = !MoveInPlace.enabled;
        MoveInPlace.enabled = !MoveInPlace.enabled;
        TeleportL.enabled = !TeleportL.enabled;
        TeleportL.enabled = !TeleportL.enabled;
        TeleportR.enabled = !TeleportR.enabled;
        TeleportR.enabled = !TeleportR.enabled;
    }

    public bool IsLocomotionArmSwinger()
    {
        return MoveInPlace.enabled;
    }

    public bool IsGameInProgress()
    {
        return GameInProgress;
    }

    public void StartGame()
    {
        /*GameController.Instance.EventsManager.Restart();*/
        ScoreBoard.RestartBoard();
        ForceTeleportScript.ForceTeleportToStart();
        GameInProgress = true;
    }
}