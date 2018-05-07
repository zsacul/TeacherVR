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
        FireWork, Poof, Combo
    };


    public GameObject OnePoint, TenPoints, FiftyPoints, HundredPoints, TwoHundredPoints, ThreeHundredPoints,
                      Minus15, Minus30, Plus15, Plus30,
                      Small_Good_Correct_Ok, Small_Hit_Boom, Small_Wrong,
                      FireWork, Poof,Combo;


    public void CreateOnePoint(Vector3 transform, float rotation)
    {
        Instantiate(OnePoint, transform, gameObject.transform.rotation);
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
            Instantiate(OnePoint, transform, gameObject.transform.rotation);
        }
        else if (PS == NaszeParticle.TenPoints)
        {
            Instantiate(TenPoints, transform, gameObject.transform.rotation);
        }
        else if (PS == NaszeParticle.FiftyPoints)
        {
            Instantiate(FiftyPoints, transform, gameObject.transform.rotation);
        }
        else if (PS == NaszeParticle.HundredPoints)
        {
            Instantiate(HundredPoints, transform, gameObject.transform.rotation);
        }
        else if (PS == NaszeParticle.TwoHundredPoints)
        {
            Instantiate(TwoHundredPoints, transform, gameObject.transform.rotation);
        }
        else if (PS == NaszeParticle.ThreeHundredPoints)
        {
            Instantiate(ThreeHundredPoints, transform, gameObject.transform.rotation);
        }
        // CZAS
        else if (PS == NaszeParticle.Minus15)
        {
            Instantiate(Minus15, transform, gameObject.transform.rotation);
        }
        else if (PS == NaszeParticle.Minus30)
        {
            Instantiate(Minus30, transform, gameObject.transform.rotation);
        }
        else if (PS == NaszeParticle.Plus15)
        {
            Instantiate(Plus15, transform, gameObject.transform.rotation);
        }
        else if (PS == NaszeParticle.Plus30)
        {
            Instantiate(Plus30, transform, gameObject.transform.rotation);
        }
        // NAPISY
        else if (PS == NaszeParticle.Small_Good_Correct_Ok)
        {
            Instantiate(Small_Good_Correct_Ok, transform, gameObject.transform.rotation);
        }
        else if (PS == NaszeParticle.Small_Hit_Boom)
        {
            Instantiate(Small_Hit_Boom, transform, gameObject.transform.rotation);
        }
        else if (PS == NaszeParticle.Small_Wrong)
        {
            Instantiate(Small_Wrong, transform, gameObject.transform.rotation);
        }
        // RESZTA
        else if (PS == NaszeParticle.FireWork)
        {
            Instantiate(FireWork, transform, gameObject.transform.rotation);
        }
        else if (PS == NaszeParticle.Poof)
        {
            Instantiate(Poof, transform, gameObject.transform.rotation);
        }
        else if (PS == NaszeParticle.Combo)
        {
            Instantiate(Combo, transform, gameObject.transform.rotation);
        }
    }
}
