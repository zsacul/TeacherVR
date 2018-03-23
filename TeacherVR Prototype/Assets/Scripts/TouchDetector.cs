using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchDetector: MonoBehaviour
{
    public bool Trigger;
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("LController") || other.tag.Equals("RController"))
        {
            Trigger = true;
            Invoke("TriggerOff",1);
        }
    }

    private void TriggerOff()
    {
        Trigger = false;
    }
}
