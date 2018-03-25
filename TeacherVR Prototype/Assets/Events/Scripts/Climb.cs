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
        Destroy(objToClimbInstance);
        Destroy(objToTouchInstance);
    }

    public override void CompleteEvent()
    {
        base.CompleteEvent();
        Destroy(objToClimbInstance);
        Destroy(objToTouchInstance);
    }
}