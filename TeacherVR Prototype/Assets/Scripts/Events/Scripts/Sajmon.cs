using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Event", menuName = "Events/Sajmon Event")]
public class Sajmon : Events
{
    public GameObject PC;

    private Color color;

    public override void StartEvent()
    {
        base.StartEvent();
        PC.GetComponent<Renderer>().material.color = Color.red;
    }

    public override void AbortEvent()
    {
        base.AbortEvent();
    }
}
