using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitVolume : MonoBehaviour {
    static public int minLevelOfLoudness = 6;


    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name + " " + collision.relativeVelocity.magnitude);
        if (collision.relativeVelocity.magnitude > minLevelOfLoudness)
            Noise.doneSomethingLoudEnough = true;
    }
}
