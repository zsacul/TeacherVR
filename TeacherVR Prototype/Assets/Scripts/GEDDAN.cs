using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GEDDAN : MonoBehaviour {

    public float liftdrop;
    public float highBorder;
    public float lowBorder;

    private Transform[] pistons;
    private Vector3 trans;
    private float r;

    void Start () {
        pistons = new Transform[transform.childCount];

        for (int i = 0; i < transform.childCount; i++) {
            pistons[i] = transform.GetChild(i).gameObject.transform;
        }
	}
	
	void Update () {

        r = liftdrop;

        if (pistons[4].localPosition.z < highBorder && pistons[4].localPosition.z > lowBorder )
        {
            PosUpdate(r);
            
        } else
        {
            if (pistons[4].localPosition.z >= highBorder)
            {
                if (r < 0)
                {
                    PosUpdate(r);
                }
            }

            if (pistons[4].localPosition.z <= highBorder)
            {
                if (r > 0)
                {
                    PosUpdate(r);
                }
            }
        }
        
	}

    void PosUpdate(float move)
    {
        foreach (Transform i in pistons)
        {
            trans = new Vector3(0f, 0f, move);
            move *= 1.3f;
            i.localPosition += trans;

        }
    }

}

