using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Climb Event", menuName = "Events/Climb Event")]
public class Climb : Events
{
    [Header("Custom Settings")] public GameObject ObjectToClimb;
    public GameObject ObjectToTouch;

    private GameObject objToClimbInstance;
    private GameObject objToTouchInstance;

    private TouchDetector tt;

    public override void StartEvent()
    {
        base.StartEvent();
        GameController.Instance.MessageSystem.ShowButtonOnControllers(MessageSystem.Button.Touchpad, "Locomotion", 60);
        GameController.Instance.MessageSystem.ShowButtonOnControllers(MessageSystem.Button.Trigger, "Grab", 60);
        objToClimbInstance = Instantiate(ObjectToClimb);
        objToTouchInstance = Instantiate(ObjectToTouch);
        tt = objToTouchInstance.GetComponent<TouchDetector>();
    }

    public override void CallInUpdate()
    {
        if (tt.Trigger)
        {
            CompleteEvent();
        }
    }

    public override void AbortEvent()
    {
        base.AbortEvent();
        if (objToClimbInstance != null)
            objToClimbInstance.GetComponentInChildren<VRTK.VRTK_InteractableObject>().ForceStopInteracting();
        VRTK.VRTK_PlayerClimb climb = GameObject.Find("VRTKScripts").transform.Find("PlayArea")
            .GetComponent<VRTK.VRTK_PlayerClimb>();
        climb.enabled = false;
        climb.enabled = true;
        Destroy(objToClimbInstance);
        Destroy(objToTouchInstance);
    }

    public override void CompleteEvent()
    {
        base.CompleteEvent();
        var Target = GameObject.Find("Score");
        Target.GetComponent<ScoreBoard>().SetActive();

        objToClimbInstance.GetComponentInChildren<VRTK.VRTK_InteractableObject>().ForceStopInteracting();
        VRTK.VRTK_PlayerClimb climb = GameObject.Find("VRTKScripts").transform.Find("PlayArea")
            .GetComponent<VRTK.VRTK_PlayerClimb>();
        climb.enabled = false;
        climb.enabled = true;
        Destroy(objToClimbInstance);
        Destroy(objToTouchInstance);
    }
}