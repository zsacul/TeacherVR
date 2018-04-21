using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particles : MonoBehaviour
{
    public enum NaszeParticle
    {
        OnePoint, TenPoints, FiftyPoints, HundredPoints, TwoHundredPoints, ThreeHundredPoints,
        Minus15, Minus30, Plus15, Plus30,
        Small_Good_Correct_Ok, Small_Hit_Boom, Small_Wrong,
        FireWork, Poof
    };


    public GameObject OnePoint, TenPoints, FiftyPoints, HundredPoints, TwoHundredPoints, ThreeHundredPoints,
                      Minus15, Minus30, Plus15, Plus30,
                      Small_Good_Correct_Ok, Small_Hit_Boom, Small_Wrong,
                      FireWork, Poof;


    public void CreateOnePoint(Vector3 transform, float rotation)
    {
        GameObject newParticle = Instantiate(OnePoint, transform, gameObject.transform.rotation) as GameObject;
    }

    /*
    ODWOŁANIE POPRZEZ np.:
    ParticleSystem.GetComponent<Particles>().CreateParticle(Particles.NaszeParticle.HundredPoints, Where.transform.position);

    */

    public void CreateParticle(NaszeParticle PS, Vector3 transform)
    {
        // PUNKTY
        if (PS == NaszeParticle.OnePoint)
        {
            GameObject newParticle = Instantiate(OnePoint, transform, gameObject.transform.rotation) as GameObject;
        }
        else if (PS == NaszeParticle.TenPoints)
        {
            GameObject newParticle = Instantiate(TenPoints, transform, gameObject.transform.rotation) as GameObject;
        }
        else if (PS == NaszeParticle.FiftyPoints)
        {
            GameObject newParticle = Instantiate(FiftyPoints, transform, gameObject.transform.rotation) as GameObject;
        }
        else if (PS == NaszeParticle.HundredPoints)
        {
            GameObject newParticle = Instantiate(HundredPoints, transform, gameObject.transform.rotation) as GameObject;
        }
        else if (PS == NaszeParticle.TwoHundredPoints)
        {
            GameObject newParticle = Instantiate(TwoHundredPoints, transform, gameObject.transform.rotation) as GameObject;
        }
        else if (PS == NaszeParticle.ThreeHundredPoints)
        {
            GameObject newParticle = Instantiate(ThreeHundredPoints, transform, gameObject.transform.rotation) as GameObject;
        }
        // CZAS
        else if (PS == NaszeParticle.Minus15)
        {
            GameObject newParticle = Instantiate(Minus15, transform, gameObject.transform.rotation) as GameObject;
        }
        else if (PS == NaszeParticle.Minus30)
        {
            GameObject newParticle = Instantiate(Minus30, transform, gameObject.transform.rotation) as GameObject;
        }
        else if (PS == NaszeParticle.Plus15)
        {
            GameObject newParticle = Instantiate(Plus15, transform, gameObject.transform.rotation) as GameObject;
        }
        else if (PS == NaszeParticle.Plus30)
        {
            GameObject newParticle = Instantiate(Plus30, transform, gameObject.transform.rotation) as GameObject;
        }
        // NAPISY
        else if (PS == NaszeParticle.Small_Good_Correct_Ok)
        {
            GameObject newParticle = Instantiate(Small_Good_Correct_Ok, transform, gameObject.transform.rotation) as GameObject;
        }
        else if (PS == NaszeParticle.Small_Hit_Boom)
        {
            GameObject newParticle = Instantiate(Small_Hit_Boom, transform, gameObject.transform.rotation) as GameObject;
        }
        else if (PS == NaszeParticle.Small_Wrong)
        {
            GameObject newParticle = Instantiate(Small_Wrong, transform, gameObject.transform.rotation) as GameObject;
        }
        // RESZTA
        else if (PS == NaszeParticle.FireWork)
        {
            GameObject newParticle = Instantiate(FireWork, transform, gameObject.transform.rotation) as GameObject;
        }
        else if (PS == NaszeParticle.Poof)
        {
            GameObject newParticle = Instantiate(Poof, transform, gameObject.transform.rotation) as GameObject;
        }
    }
}
