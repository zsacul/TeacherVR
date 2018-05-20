using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementX : MonoBehaviour {

    public float posX;
    public float highBorder;
    public float lowBorder;
    private Transform transform;
    private Vector3 trans;

    void Start()
    {
        transform = GetComponent<Transform>();
    }


    void Update()
    {
        if (transform.localPosition.x < highBorder && transform.localPosition.x > lowBorder)
        {

            PosUpdate();
        }
        else
        {
            if (transform.localPosition.x >= lowBorder)
            {
                if (posX < 0)
                {
                    PosUpdate();
                }
            }
            else if (transform.localPosition.x <= highBorder)
            {
                if (posX > 0)
                {
                    PosUpdate();
                }
            }
        }


    }

    void PosUpdate()
    {
        trans = new Vector3(posX, 0f, 0f);
        transform.localPosition += trans;
    }
}


