using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowHeadsetY : MonoBehaviour
{
    public float Offset;
    public Transform HeadSetFollower;
	void Start ()
	{
	    StartCoroutine(Find());
	}

    IEnumerator Find()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        HeadSetFollower = VRTK.VRTK_DeviceFinder.HeadsetCamera();
    }
	
	void Update ()
    {
		transform.position = new Vector3(transform.position.x,HeadSetFollower.position.y + Offset,transform.position.z);
	}
}
