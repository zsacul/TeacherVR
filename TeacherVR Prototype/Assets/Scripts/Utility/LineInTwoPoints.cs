using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LineInTwoPoints : MonoBehaviour
{
    public Transform End;

    private LineRenderer Line;

    void Start()
    {
        Line = GetComponent<LineRenderer>();
    }

    void Update()
    {
        StartCoroutine(PosUpdate());
    }

    private IEnumerator PosUpdate()
    {
        yield return new WaitForEndOfFrame();
        Line.SetPosition(0, transform.position);
        Line.SetPosition(1, End.position);
    }
}