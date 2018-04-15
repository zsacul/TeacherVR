using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Climb Event", menuName = "Events/Climb Event")]
public class Climb : Events
{
    public GameObject ObjectToClimb;
    public GameObject ObjectToTouch;

    private GameObject objToClimbInstance;
    private GameObject objToTouchInstance;

    private TouchDetector tt;

    public override void StartEvent()
    {
        base.StartEvent();
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
        objToClimbInstance.GetComponentInChildren<VRTK.VRTK_InteractableObject>().ForceStopInteracting();
        VRTK.VRTK_PlayerClimb climb = GameObject.Find("VRTKScripts").transform.Find("PlayArea").GetComponent<VRTK.VRTK_PlayerClimb>();
        climb.enabled = false;
        climb.enabled = true;
        Destroy(objToClimbInstance);
        Destroy(objToTouchInstance);
    }

    public override void CompleteEvent()
    { 
        GameObject Target;
        Target = GameObject.Find("Score");                      
        Target.GetComponent<ScoreBoard>().SetActive();

        base.CompleteEvent();
        objToClimbInstance.GetComponentInChildren<VRTK.VRTK_InteractableObject>().ForceStopInteracting();
        VRTK.VRTK_PlayerClimb climb = GameObject.Find("VRTKScripts").transform.Find("PlayArea").GetComponent<VRTK.VRTK_PlayerClimb>();
        climb.enabled = false;
        climb.enabled = true;
        Destroy(objToClimbInstance);
        Destroy(objToTouchInstance);
    }
}