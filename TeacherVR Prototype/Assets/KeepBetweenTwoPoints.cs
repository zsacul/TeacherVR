using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepBetweenTwoPoints : MonoBehaviour
{
    public Vector3 Start;
    public Vector3 End;

    void LateUpdate()
    {
        transform.localPosition = new Vector3(
            Mathf.Clamp(transform.localPosition.x, Start.x, End.x),
            Mathf.Clamp(transform.localPosition.y, Start.y, End.y),
            Mathf.Clamp(transform.localPosition.z, Start.z, End.z));
    }
}