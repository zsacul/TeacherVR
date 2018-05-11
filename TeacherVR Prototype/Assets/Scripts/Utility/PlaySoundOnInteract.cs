using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class PlaySoundOnInteract : MonoBehaviour
{
    public SamplesList Sound;

    public ActionList Action;

    public float Volume = 1;
    public Vector2 RandomLoopDelay;
    public bool OnlyWhenGameInProgress = false;

    private VRTK_InteractableObject io;
    private VRTK_SnapDropZone sdz;
    private Rigidbody rb;

    private const float ThrowVelocity = 6;

    private float LastTime;
    private float Delay;

    private GameObject SoundInst;

    public enum ActionList
    {
        Snaped,
        Unsnaped,
        Touched,
        Untouched,
        Grabbed,
        Ungrabbed,
        Enable,
        Disable,
        Throw,
        RandomLoop,
        GrabLoop
    }

    /*void Awake()
    {
        if (Action == ActionList.Throw) rb = GetComponent<Rigidbody>();
    }*/

    void OnEnable()
    {
        if (Action == ActionList.Enable) Play();
    }

    void OnDisable()
    {
        if (Action == ActionList.Disable) Play();
    }

    private void OnDestroy()
    {
        switch (Action)
        {
            case ActionList.Snaped:
                if (sdz != null) sdz.ObjectSnappedToDropZone -= PlaySDZ;
                break;
            case ActionList.Unsnaped:
                if (sdz != null) sdz.ObjectUnsnappedFromDropZone -= PlaySDZ;
                break;
            case ActionList.Touched:
                if (io != null) io.InteractableObjectTouched -= PlayIO;
                break;
            case ActionList.Untouched:
                if (io != null) io.InteractableObjectUntouched -= PlayIO;
                break;
            case ActionList.Grabbed:
                if (io != null) io.InteractableObjectGrabbed -= PlayIO;
                break;
            case ActionList.Ungrabbed:
                if (io != null) io.InteractableObjectUngrabbed -= PlayIO;
                break;
            case ActionList.Throw:
                if (io != null) io.InteractableObjectGrabbed -= DelayOnGrab;
                break;
            case ActionList.GrabLoop:
                if (io != null)
                {
                    io.InteractableObjectGrabbed -= GrabStateOn;
                    io.InteractableObjectUngrabbed -= GrabStateOff;
                }
                break;
        }
    }

    void Start()
    {
        io = GetComponent<VRTK_InteractableObject>();
        sdz = GetComponent<VRTK_SnapDropZone>();
        LastTime = 0;
        Delay = 6;

        if (Action == ActionList.RandomLoop) Delay = Random.Range(RandomLoopDelay.x, RandomLoopDelay.y);

        switch (Action)
        {
            case ActionList.Snaped:
                if (sdz != null) sdz.ObjectSnappedToDropZone += PlaySDZ;
                break;
            case ActionList.Unsnaped:
                if (sdz != null) sdz.ObjectUnsnappedFromDropZone += PlaySDZ;
                break;
            case ActionList.Touched:
                if (io != null) io.InteractableObjectTouched += PlayIO;
                break;
            case ActionList.Untouched:
                if (io != null) io.InteractableObjectUntouched += PlayIO;
                break;
            case ActionList.Grabbed:
                if (io != null) io.InteractableObjectGrabbed += PlayIO;
                break;
            case ActionList.Ungrabbed:
                if (io != null) io.InteractableObjectUngrabbed += PlayIO;
                break;
            case ActionList.Throw:
                if (io != null) io.InteractableObjectGrabbed += DelayOnGrab;
                rb = GetComponent<Rigidbody>();
                break;
            case ActionList.GrabLoop:
                if (io != null)
                {
                    io.InteractableObjectGrabbed += GrabStateOn;
                    io.InteractableObjectUngrabbed += GrabStateOff;
                }
                break;
        }
    }

    void Update()
    {
        if (Action == ActionList.Throw && rb != null && rb.velocity.magnitude > ThrowVelocity)
        {
            if (Time.time > LastTime + Delay)
            {
                LastTime = Time.time;
                Delay = 6;
                Play();
            }
        }
        if (Action == ActionList.RandomLoop)
        {
            if (Time.time > LastTime + Delay)
            {
                LastTime = Time.time;
                Play();
            }
        }
    }

    private void Play()
    {
        if (OnlyWhenGameInProgress && GameController.Instance.IsGameInProgress() || !OnlyWhenGameInProgress)
            SoundInst = GameController.Instance.SoundManager.Play3DAt(Sound, transform.position, Volume);
    }

    private void PlaySDZ(object sender, SnapDropZoneEventArgs e)
    {
        Play();
    }

    private void PlayIO(object sender, InteractableObjectEventArgs e)
    {
        Play();
    }

    private void DelayOnGrab(object sender, InteractableObjectEventArgs e)
    {
        LastTime = Time.time;
        Delay = 0.2f;
    }

    private void GrabStateOn(object sender, InteractableObjectEventArgs e)
    {
        Play();
        SoundInst.GetComponent<AudioSource>().loop = true;
    }

    private void GrabStateOff(object sender, InteractableObjectEventArgs e)
    {
        if (SoundInst != null) SoundInst.GetComponent<AudioSource>().Stop();
        SoundInst.GetComponent<AudioSource>().loop = false;
    }
}