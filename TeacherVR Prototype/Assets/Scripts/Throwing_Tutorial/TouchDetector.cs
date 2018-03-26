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
            StartCoroutine(TriggerOff());
        }
    }

    private IEnumerator TriggerOff()
    {
        yield return new WaitForEndOfFrame();
        Trigger = false;
    }
}
