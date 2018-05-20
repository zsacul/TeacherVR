using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawControl : MonoBehaviour {

    public MovementX X;
    public MovementY Y;
    public GEDDAN Claw;

    public float moveX;
    public float moveY;
    public float moveClaw;

    void Start () {
		
	}
	
	
	void Update () {
        X.posX = moveX;
        Y.posY = moveY;
        Claw.liftdrop = moveClaw;
    }
}
