using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimControlOnGrab : MonoBehaviour
{
    public float min;
    public float max;
    public string AnimName;
    public Animation anim;

    void Start()
    {
        anim.Play(AnimName);
        anim[AnimName].speed = 0;
    }

	void LateUpdate ()
	{
	    anim[AnimName].normalizedTime = (transform.localPosition.x - min) / max;
	}
}
