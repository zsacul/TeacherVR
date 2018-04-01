using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target_Control : MonoBehaviour
{
    private bool allKilled = false;
    private int targetsNum = 0;
    private int targetsKilled = 0;

    void Start()
    {
        foreach (Transform child in transform)
        {
            targetsNum++;
        }
    }

    void Update()
    {
        if (targetsKilled == targetsNum) allKilled = true;
    }

    public void Killed()
    {
        targetsKilled++;
    }

    public bool Destruction()
    {
        return allKilled;
    }
}