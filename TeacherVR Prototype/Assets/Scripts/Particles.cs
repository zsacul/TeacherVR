using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particles : MonoBehaviour
{
    public GameObject Particle1;

    public GameObject Particle2;


    public void CreateParticle1(Vector3 transform, float rotation)
    {
        GameObject newParticle = Instantiate(Particle1, transform, gameObject.transform.rotation) as GameObject;
    }
}
