using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particles : MonoBehaviour
{
    public GameObject OnePoint;

    public void CreateOnePoint(Vector3 transform, float rotation)
    {
        GameObject newParticle = Instantiate(OnePoint, transform, gameObject.transform.rotation) as GameObject;
    }
}
