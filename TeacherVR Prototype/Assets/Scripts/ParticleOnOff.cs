using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Networking;
using VRTK;

public class ParticleOnOff : MonoBehaviour
{
    public float ParticleScale = 1f;

    public float LifeTime = 0f;

    public GameObject Particle;
    public Transform ParticleTransform;

    public Events[] EventScriptableObjects;
    private Events CurrentEventScriptableObject;

    private GameObject ParticleInstance;

    public enum Occasion
    {
        Snaped,
        Unsnaped,
        Touched,
        Untouched,
        Grabbed,
        Ungrabbed,
        Enable,
        Disable,
        Event,
        Never
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
        else
        {
            ParticleInstance = Instantiate(Particle, ParticleTransform.position, ParticleTransform.rotation);
            ParticleInstance.transform.parent = ParticleTransform;
        }

        ParticleInstance.transform.localScale *= ParticleScale;
    }

    private void Del()
    {
        if (ParticleInstance != null) Destroy(ParticleInstance, LifeTime);
    }

    private void OnDestroy()
    {
        switch (TurnOn)
        {
            case Occasion.Snaped:
                if (sdz != null) sdz.ObjectSnappedToDropZone -= ObjectSnappedOn;
                break;
            case Occasion.Unsnaped:
                if (sdz != null) sdz.ObjectSnappedToDropZone -= ObjectUnsnappedOn;
                break;
            case Occasion.Touched:
                if (io != null) io.InteractableObjectTouched -= ObjectTouchedOn;
                break;
            case Occasion.Untouched:
                if (io != null) io.InteractableObjectTouched -= ObjectUntouchedOn;
                break;
            case Occasion.Grabbed:
                if (io != null) io.InteractableObjectTouched -= ObjectGrabbedOn;
                break;
            case Occasion.Ungrabbed:
                if (io != null) io.InteractableObjectTouched -= ObjectUngrabbedOn;
                break;
            case Occasion.Event:
                CheckEventAbort();
                GameController.Instance.EventsManager.EventsManagerStartNext -= EventsManager_EventsManagerStartNext;
                break;
        }

        switch (TurnOff)
        {
            case Occasion.Snaped:
                if (sdz != null) sdz.ObjectSnappedToDropZone -= ObjectSnappedOff;
                break;
            case Occasion.Unsnaped:
                if (sdz != null) sdz.ObjectSnappedToDropZone -= ObjectUnsnappedOff;
                break;
            case Occasion.Touched:
                if (io != null) io.InteractableObjectTouched -= ObjectTouchedOff;
                break;
            case Occasion.Untouched:
                if (io != null) io.InteractableObjectTouched -= ObjectUntouchedOff;
                break;
            case Occasion.Grabbed:
                if (io != null) io.InteractableObjectTouched -= ObjectGrabbedOff;
                break;
            case Occasion.Ungrabbed:
                if (io != null) io.InteractableObjectTouched -= ObjectUngrabbedOff;
                break;
            case Occasion.Event:
                CheckEventAbort();
                GameController.Instance.EventsManager.EventsManagerStartNext -= EventsManager_EventsManagerStartNext;
                break;
        }
        Del();
    }

    void CheckEventAbort()
    {
        if (CurrentEventScriptableObject != GameController.Instance.EventsManager.GetCurrentEvent())
        {
            Del();
            CancelInvoke();
        }
    }

    void CheckEvent()
    {
        if (CurrentEventScriptableObject != null &&
            CurrentEventScriptableObject == GameController.Instance.EventsManager.GetCurrentEvent())
        {
            if (TurnOn == Occasion.Event) Inst();
            if (TurnOff == Occasion.Event) Del();
            CancelInvoke();
            InvokeRepeating("CheckEventAbort", 0, 1);
        }
    }

    void Start()
    {
        io = GetComponent<VRTK_InteractableObject>();
        sdz = GetComponent<VRTK_SnapDropZone>();
        EventsManager_EventsManagerStartNext();
        GameController.Instance.EventsManager.EventsManagerStartNext += EventsManager_EventsManagerStartNext;

        switch (TurnOn)
        {
            case Occasion.Snaped:
                sdz.ObjectSnappedToDropZone += ObjectSnappedOn;
                break;
            case Occasion.Unsnaped:
                sdz.ObjectUnsnappedFromDropZone += ObjectUnsnappedOn;
                break;
            case Occasion.Touched:
                io.InteractableObjectTouched += ObjectTouchedOn;
                break;
            case Occasion.Untouched:
                io.InteractableObjectUntouched += ObjectUntouchedOn;
                break;
            case Occasion.Grabbed:
                io.InteractableObjectGrabbed += ObjectGrabbedOn;
                break;
            case Occasion.Ungrabbed:
                io.InteractableObjectUngrabbed += ObjectUngrabbedOn;
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
                sdz.ObjectUnsnappedFromDropZone += ObjectUnsnappedOff;
                break;
            case Occasion.Touched:
                io.InteractableObjectTouched += ObjectTouchedOff;
                break;
            case Occasion.Untouched:
                io.InteractableObjectUntouched += ObjectUntouchedOff;
                break;
            case Occasion.Grabbed:
                io.InteractableObjectGrabbed += ObjectGrabbedOff;
                break;
            case Occasion.Ungrabbed:
                io.InteractableObjectUngrabbed += ObjectUngrabbedOff;
                break;
            case Occasion.Event:
                InvokeRepeating("CheckEvent", 0, 1);
                break;
        }
    }

    private void EventsManager_EventsManagerStartNext()
    {
        Events curr = GameController.Instance.EventsManager.GetCurrentEvent();
        if (curr != null)
            foreach (var eventSO in EventScriptableObjects)
            {
                if (eventSO.name == curr.name)
                {
                    CurrentEventScriptableObject = curr;
                    InvokeRepeating("CheckEvent", 0, 1);
                }
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