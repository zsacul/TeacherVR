using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

[RequireComponent(typeof(VRTK_InteractableObject))]
public class ResetOnRange : MonoBehaviour
{
    public float MaxRange = 10;
    public Action OverMaxRangeAction = Action.ResetToEndPoint;
    public bool UseWrongParticle = false;
    public Transform End;

    private VRTK_InteractableObject io;
    private VRTK_InteractableObject io2;

    private Vector3 startPos1;
    private Vector3 startPos2;

    public enum Action
    {
        ResetToEndPoint,
        ResetBothToMiddle,
        ResetBothToStart
    }

    void Start()
    {
        io = GetComponent<VRTK_InteractableObject>();
        if (OverMaxRangeAction == Action.ResetBothToMiddle)
            io2 = End.GetComponent<VRTK_InteractableObject>();
        if (OverMaxRangeAction == Action.ResetBothToStart)
        {
            io2 = End.GetComponent<VRTK_InteractableObject>();
            startPos1 = transform.position;
            startPos2 = End.transform.position;
        }
    }

    void Update()
    {
        if (OverMaxRangeAction == Action.ResetBothToStart)
        {
            if (Vector3.Distance(transform.position, startPos1) > MaxRange ||
                Vector3.Distance(End.position, startPos2) > MaxRange)
            {
                SpawnWrongPartcile();
                Unsnap(transform.parent);
                Unsnap(End.transform.parent);
                io.ForceStopInteracting();
                io2.ForceStopInteracting();
                transform.position = startPos1;
                End.transform.position = startPos2;
            }
        }
        else if (Vector3.Distance(End.position, transform.position) > MaxRange)
        {
            io.ForceStopInteracting();
            if (OverMaxRangeAction == Action.ResetBothToMiddle)
            {
                SpawnWrongPartcile();
                Unsnap(transform.parent);
                Unsnap(End.transform.parent);
                io2.ForceStopInteracting();

                Vector3 a;
                Vector3 b;
                a = Vector3.Lerp(transform.position, End.position, 1f / 3);
                b = Vector3.Lerp(transform.position, End.position, 2f / 3);
                transform.position = a;
                End.position = b;
            }
            else
            {
                SpawnWrongPartcile();
                Unsnap(transform.parent);
                transform.position = End.position;
            }
        }
    }

    void Unsnap(Transform parent)
    {
        while (parent != null)
        {
            if (parent.GetComponent<VRTK_SnapDropZone>() != null)
            {
                parent.GetComponent<VRTK_SnapDropZone>().ForceUnsnap();
                break;
            }
            parent = parent.parent;
        }
    }

    void SpawnWrongPartcile()
    {
        if (UseWrongParticle)
        {
            GameController.Instance.Particles.CreateParticle(Particles.NaszeParticle.Small_Wrong, transform.position);
            GameController.Instance.Particles.CreateParticle(Particles.NaszeParticle.Small_Wrong,
                End.transform.position);
            GameController.Instance.SoundManager.Play2D(SamplesList.Error,0.01f);
        }
    }
}