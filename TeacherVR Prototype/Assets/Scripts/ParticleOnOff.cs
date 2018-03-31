using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using VRTK;

public class ParticleOnOff : MonoBehaviour
{
    public float LifeTime = 0f;

    public GameObject Particle;
    public Transform ParticleTransform;
    private GameObject ParticleInstance;

    public enum Occasion
    {
        Touched,
        Untouch,
        Grabbed,
        Ungrabbed,
        Enable,
        Disable
    }

    public Occasion TurnOn;
    public Occasion TurnOff;

    private VRTK_InteractableObject io;

    void OnEnable()
    {
        if (TurnOn == Occasion.Enable)
        {
            Inst();
        }

        if (TurnOff == Occasion.Enable)
        {
            Del();
        }
    }

    void OnDisable()
    {
        if (TurnOn == Occasion.Disable)
        {
            Inst();
        }
        if (TurnOff == Occasion.Disable)
        {
            Del();
        }
    }

    void Start()
    {
        io = GetComponent<VRTK_InteractableObject>();
        switch (TurnOn)
        {
            case Occasion.Touched:
                io.InteractableObjectTouched += ObjectTouchedOn;
                break;
            case Occasion.Untouch:
                io.InteractableObjectTouched += ObjectUntouchedOn;
                break;
            case Occasion.Grabbed:
                io.InteractableObjectTouched += ObjectGrabbedOn;
                break;
            case Occasion.Ungrabbed:
                io.InteractableObjectTouched += ObjectUngrabbedOn;
                break;
        }

        switch (TurnOff)
        {
            case Occasion.Touched:
                io.InteractableObjectTouched += ObjectTouchedOff;
                break;
            case Occasion.Untouch:
                io.InteractableObjectTouched += ObjectUntouchedOff;
                break;
            case Occasion.Grabbed:
                io.InteractableObjectTouched += ObjectGrabbedOff;
                break;
            case Occasion.Ungrabbed:
                io.InteractableObjectTouched += ObjectUngrabbedOff;
                break;
        }
    }

    private void Inst()
    {
        ParticleInstance = Instantiate(Particle, ParticleTransform.position, ParticleTransform.rotation);
    }

    private void Del()
    {
        if (ParticleInstance != null) Destroy(ParticleInstance, LifeTime);
    }

    private void ObjectTouchedOn(object sender, InteractableObjectEventArgs e)
    {
        Inst();
    }

    private void ObjectUntouchedOn(object sender, InteractableObjectEventArgs e)
    {
        Inst();
    }

    private void ObjectGrabbedOn(object sender, InteractableObjectEventArgs e)
    {
        Inst();
    }

    private void ObjectUngrabbedOn(object sender, InteractableObjectEventArgs e)
    {
        Inst();
    }

    private void ObjectTouchedOff(object sender, InteractableObjectEventArgs e)
    {
        Del();
    }

    private void ObjectUntouchedOff(object sender, InteractableObjectEventArgs e)
    {
        Del();
    }

    private void ObjectGrabbedOff(object sender, InteractableObjectEventArgs e)
    {
        Del();
    }

    private void ObjectUngrabbedOff(object sender, InteractableObjectEventArgs e)
    {
        Del();
    }
}