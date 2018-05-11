using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class PlayerInSpace : MonoBehaviour
{
    private Transform Player;

	void Start ()
	{
	    StartCoroutine(Find());
		InvokeRepeating("IfPlayerInSpace", 2f,1f);
	}

    IEnumerator Find()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        Player = VRTK_DeviceFinder.HeadsetTransform();
    }

    void IfPlayerInSpace()
    {
        if(Player.position.y < -1)GameController.Instance.RestartGame();
    }
}
