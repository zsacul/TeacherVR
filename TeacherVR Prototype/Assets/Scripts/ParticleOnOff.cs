using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using VRTK;

public class ParticleOnOff : MonoBehaviour
{
    public float ParticleScale = 1f;

    public float LifeTime = 0f;

    public GameObject Particle;
    public Transform ParticleTransform;

    public Events EventScriptableObject;

    private GameObject ParticleInstance;

    public enum Occasion
    {
        Snaped,
        Unsnaped,
        Touched,
        Untouch,
        Grabbed,
        Ungrabbed,
        Enable,
        Disable,
        Event
    }

    public Occasion TurnOn;
    public Occasion TurnOff;

    private VRTK_InteractableObject io;
    private VRTK_SnapDropZone sdz;

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

    private void Inst()
    {
        if (ParticleTransform == null) ParticleInstance = Instantiate(Particle);
        else ParticleInstance = Instantiate(Particle, ParticleTransform.position, ParticleTransform.rotation);
        ParticleInstance.transform.localScale *= ParticleScale;
    }

    private void Del()
    {
        if (ParticleInstance != null) Destroy(ParticleInstance, LifeTime);
    }

    void CheckEvent()
    {
        if (EventScriptableObject == GameController.Instance.EventsManager.GetCurrentEvent())
        {
            if (TurnOn == Occasion.Event) Inst();
            if (TurnOff == Occasion.Event) Del();
            CancelInvoke();
        }
    }

    void Start()
    {
        io = GetComponent<VRTK_InteractableObject>();
        sdz = GetComponent<VRTK_SnapDropZone>();
        switch (TurnOn)
        {
            case Occasion.Snaped:
                sdz.ObjectSnappedToDropZone += ObjectSnappedOn;
                break;
            case Occasion.Unsnaped:
                sdz.ObjectSnappedToDropZone += ObjectUnsnappedOn;
                break;
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
            case Occasion.Event:
                InvokeRepeating("CheckEvent", 0, 1);
                break;
        }

        switch (TurnOff)
        {
            case Occasion.Snaped:
                sdz.ObjectSnappedToDropZone += ObjectSnappedOff;
                break;
            case Occasion.Unsnaped:
                sdz.ObjectSnappedToDropZone += ObjectUnsnappedOff;
                break;
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
            case Occasion.Event:
                InvokeRepeating("CheckEvent", 0, 1);
                break;
        }
    }

    private void ObjectSnappedOn(object sender, SnapDropZoneEventArgs e)
    {
        Inst();
    }

    private void ObjectUnsnappedOn(object sender, SnapDropZoneEventArgs e)
    {
        Inst();
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

    private void ObjectSnappedOff(object sender, SnapDropZoneEventArgs e)
    {
        Del();
    }

    private void ObjectUnsnappedOff(object sender, SnapDropZoneEventArgs e)
    {
        Del();
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