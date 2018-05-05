using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

[CreateAssetMenu(fileName = "New Ring Event", menuName = "Events/Ring Event")]
public class Ring : Events
{
    [Header("Custom Settings")] public GameObject RingObj;
    public GameObject ObjForLine;

    public Transform MinTransform;
    public Transform MaxTransform;

    private Transform RingEnd;
    private Vector3[] MidPoints;

    private VRTK_InteractableObject Capsule;
    private GameObject RingInst;
    private List<GameObject> CubeInstList = new List<GameObject>();

    private float LastTime;
    private float delay;

    public override void StartEvent()
    {
        base.StartEvent();
        GameController.Instance.MessageSystem.ShowButtonOnControllers(MessageSystem.Button.Trigger, "Grab", 60);
        Capsule = GameObject.Find("Sliding table/Capsule").GetComponent<VRTK_InteractableObject>();
        LastTime = 0;
        delay = 1f;
        Clear();
        MidPoints = new Vector3[Lvl];
        MidPoints[0] = new Vector3(
            Random.Range(MinTransform.position.x, MaxTransform.position.x),
            MinTransform.position.y,
            MinTransform.position.z);
        MidPoints[1] = new Vector3(
            MidPoints[0].x,
            MinTransform.position.y + (float) 1 / Lvl *
            (MaxTransform.position.y - MinTransform.position.y),
            MidPoints[0].z);
        for (int i = 2; i < Lvl; i++)
        {
            MidPoints[i] = new Vector3(
                Random.Range(MinTransform.position.x, MaxTransform.position.x),
                MinTransform.position.y + (float) i / Lvl *
                (MaxTransform.position.y - MinTransform.position.y),
                Random.Range(MinTransform.position.z, MaxTransform.position.z));

            int k = 1;
            while (Vector3.Angle(MidPoints[i - 2] - MidPoints[i - 1],
                       MidPoints[i] - MidPoints[i - 1]) < 80 - k ||
                   Vector3.Angle(MidPoints[i - 2] - MidPoints[i - 1],
                       MidPoints[i] - MidPoints[i - 1]) > 120 + k)
            {
                Debug.Log(Vector3.Angle(MidPoints[i - 2] - MidPoints[i - 1],
                    MidPoints[i] - MidPoints[i - 1]));
                k++;
                MidPoints[i] = new Vector3(
                    Random.Range(MinTransform.position.x, MaxTransform.position.x),
                    MinTransform.position.y + (float) i / Lvl *
                    (MaxTransform.position.y - MinTransform.position.y),
                    Random.Range(MinTransform.position.z, MaxTransform.position.z));
            }
        }
        RingEnd = Instantiate(MinTransform);
        RingEnd.position = MidPoints[0];
    }

    public override void CallInUpdate()
    {
        if (!Capsule.isGrabbable && RingInst == null)
        {
            Message(5, description, MessageSystem.ObjectToFollow.Headset, MessageSystem.Window.W800H400);
            
            RingInst = Instantiate(RingObj, MidPoints[0], RingObj.transform.rotation);
            RingInst.GetComponent<ResetOnRange>().End = RingEnd;

            GameController.Instance.Particles.CreateParticle(Particles.NaszeParticle.Poof, MidPoints[0]);
            GameController.Instance.Particles.CreateParticle(Particles.NaszeParticle.Poof,
                MidPoints[MidPoints.Length - 1]);
            GameController.Instance.SoundManager.Play3DAt(SamplesList.ShortPoof, MidPoints[0], 0.1f);
            GameController.Instance.SoundManager.Play3DAt(SamplesList.ShortPoof,
                MidPoints[MidPoints.Length - 1], 0.1f);

            CubeInstList.Add(Instantiate(ObjForLine, MidPoints[0], ObjForLine.transform.rotation));

            foreach (Vector3 point in MidPoints)
            {
                while (Vector3.Distance(CubeInstList[CubeInstList.Count - 1].transform.position,
                           point) >
                       ObjForLine.transform.localScale.y)
                {
                    Vector3 Offset =
                        (point - CubeInstList[CubeInstList.Count - 1].transform.position).normalized *
                        ObjForLine.transform.localScale.y;

                    CubeInstList.Add(Instantiate(ObjForLine,
                        CubeInstList[CubeInstList.Count - 1].transform.position + Offset,
                        ObjForLine.transform.rotation));
                }
            }
        }

        if (RingInst != null && Time.time > LastTime + delay)
        {
            LastTime = Time.time;
            ElectricSound();
        }

        if (RingInst != null &&
            Vector3.Distance(MidPoints[MidPoints.Length - 1], RingInst.transform.position) <
            ObjForLine.transform.localScale.y * 2)
            CompleteEvent();
    }

    public override void AbortEvent()
    {
        base.AbortEvent();
        Clear();
    }

    public override void CompleteEvent()
    {
        base.CompleteEvent();
        GameController.Instance.ScoreBoard.PointsAddAnim(50 * Lvl);
        GameController.Instance.Particles.CreateParticle(Particles.NaszeParticle.Small_Good_Correct_Ok,
            RingInst.transform.position);
        GameController.Instance.SoundManager.Play3DAt(SamplesList.Correct, RingInst.transform.position, 0.01f);
        Clear();
    }

    private void Clear()
    {
        foreach (var cube in CubeInstList)
        {
            Destroy(cube);
        }
        CubeInstList.Clear();
        Destroy(RingInst);
        if (RingEnd != null) Destroy(RingEnd.gameObject);
        Capsule.gameObject.GetComponent<AnimControlOnGrab>().Restart();
    }

    private void ElectricSound()
    {
        GameController.Instance.SoundManager.Play3DAt(SamplesList.Electric, MidPoints[MidPoints.Length / 2], 0.03f);
    }
}