using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementY : MonoBehaviour {

    public float posY;
    public float rightBorder;
    public float leftBorder;
    private Transform transform;
    private Vector3 trans;

	void Start () {
        transform = GetComponent<Transform>();
	}
	

	void Update () {
        if (transform.localPosition.y > leftBorder && transform.localPosition.y < rightBorder) {

            PosUpdate();
        } 
        else
        {
            if (transform.localPosition.y >= rightBorder)
            {
                if (posY < 0)
                {
                    PosUpdate();
                }
            }
            else if (transform.localPosition.y <= leftBorder)
            {
                if (posY > 0)
                {
                    PosUpdate();
                }
            }
        }
        

    }

    void PosUpdate()
    {
        trans = new Vector3(0f, posY, 0f);
        transform.localPosition += trans;
    }
}
