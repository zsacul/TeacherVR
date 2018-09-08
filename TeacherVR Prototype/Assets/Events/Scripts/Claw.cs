using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using VRTK.Examples;

//Tu należy zmienić Events/Basic Event na Events/MojaNazwaEventu Event podczas dziedziczenia
//Dziedziczymy tworząc nowy skrypt i wpisując public class MojaNazwaEventu : Events {}
[CreateAssetMenu(fileName = "New Claw Event", menuName = "Events/Claw Event")]
public class Claw : Events
{
    [Header("Custom Settings")]

    public GameObject destination;
    public GameObject clawController;
    public GameObject claw;

    private GameObject destinationInstance;
    private GameObject clawControllerInstance;
    private GameObject clawInstance;
    private ClawController clawControllerScript;

    //Podświetlone miejsca do których trzeba podejść
    //public GameObject GoTo;

    //protected GameObject GoToInst;

    //public Transform GoToTransform;

    //private void ShowGoTo()
    //{
     //   if (GoTo != null && GoToTransform != null)
     //       GoToInst = Instantiate(GoTo, GoToTransform.position, GoToTransform.rotation);
    //}

    //private void DestroyGotTo()
    //{
    //    if (GoToInst != null) Destroy(GoToInst);
    //}

    //Funkcja po której wywołaniu startuje event
    //Powinna zapamiętać na starcie parametry zmienianych obiektów
    public override void StartEvent()
    {
        base.StartEvent();
        destinationInstance = Instantiate(destination);
        clawInstance = Instantiate(claw);
        clawControllerInstance = Instantiate(clawController);
        clawControllerScript = clawControllerInstance.GetComponent<ClawController>();
    }

    public override void CallInUpdate()
    { 
        if (clawControllerScript.Finished())
        {
            CompleteEvent();
        }
    }

    public override void AbortEvent()
    {
        base.AbortEvent();
        clawControllerScript.Reset();
        Destroy(destinationInstance);
        Destroy(clawInstance);
        Destroy(clawControllerInstance);
    }

    public override void CompleteEvent()
    {
        base.CompleteEvent();
        clawControllerScript.Reset();
        Destroy(destinationInstance);
        Destroy(clawInstance);
        Destroy(clawControllerInstance);

    }

    /*
    protected void AddPoints(int pkt)
    {
        if (AddPointsEvent != null)
        {
            Debug.Log(pkt + " points for " + name);
            AddPointsEvent(pkt);
        }
    }

    protected void Message(float time, string txt, MessageSystem.ObjectToFollow objectToFollow,
        MessageSystem.Window window)
    {
        if (MessageEvent != null)
        {
            Debug.Log(txt + " for " + time + " from " + name);
            MessageEvent(time, txt, objectToFollow, window);
        }
    }*/
    
    
    /*protected void CompleteProgressBar()
    {
        SetProgressBar(100);
    }

    protected void ResetProgressBar()
    {
        SetProgressBar(0);
    }

    protected void SetProgressBar(float progress)
    {
        GameController.Instance.MessageSystem.SetProgressBar(progress);
    }

    protected void AddProgress(float progress)
    {
        GameController.Instance.MessageSystem.SetProgressBar(
            GameController.Instance.MessageSystem.GetProgress() + progress);
    }*/
}