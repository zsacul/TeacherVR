using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitVolume : MonoBehaviour {



    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name + " " + collision.relativeVelocity.magnitude);

        if (collision.relativeVelocity.magnitude > GameController.Instance.MicInput.minSilencingForce)
        {
           //particle
            GameController.Instance.SoundManager.Play3DAt(SamplesList.BookBang, gameObject.transform, collision.relativeVelocity.magnitude*10);
            Noise.doneSomethingLoudEnough = true;
        }
        else
            GameController.Instance.SoundManager.Play3DAt(SamplesList.BookBang, gameObject.transform, 1);
    }
}
