using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

[CreateAssetMenu(fileName = "New Doughnut Event", menuName = "Events/Doughnut Event")]
public class Doughnut : Events
{
    public GameObject DoughnutObj;
    public GameObject ObjForLine;

    public Transform[] MidPoints;

    private VRTK_InteractableObject Capsule;
    private GameObject DoughnutInst;
    private List<GameObject> CubeInstList = new List<GameObject>();

    public override void StartEvent()
    {
        base.StartEvent();
        GameController.Instance.MessageSystem.ShowButtonOnControllers(MessageSystem.Button.Trigger, "Grab", 60);
        Capsule = GameObject.Find("Sliding table/Capsule").GetComponent<VRTK_InteractableObject>();
        Clear();
    }


    public override void CallInUpdate()
    {
        if (!Capsule.isGrabbable && DoughnutInst == null)
        {
            Message(5, description, MessageSystem.ObjectToFollow.Headset, MessageSystem.Window.W800H400);
            DoughnutInst = Instantiate(DoughnutObj, MidPoints[0].position, DoughnutObj.transform.rotation);
            DoughnutInst.GetComponent<ResetOnRange>().End = MidPoints[0];

            GameController.Instance.Particles.CreateParticle(Particles.NaszeParticle.Poof, MidPoints[0].position);
            GameController.Instance.Particles.CreateParticle(Particles.NaszeParticle.Poof,
                MidPoints[MidPoints.Length - 1].position);
            GameController.Instance.SoundManager.Play3DAt(SamplesList.ShortPoof, MidPoints[0].position, 0.1f);
            GameController.Instance.SoundManager.Play3DAt(SamplesList.ShortPoof,
                MidPoints[MidPoints.Length - 1].position, 0.1f);

            CubeInstList.Add(Instantiate(ObjForLine, MidPoints[0].position, ObjForLine.transform.rotation));

            foreach (Transform point in MidPoints)
            {
                while (Vector3.Distance(CubeInstList[CubeInstList.Count - 1].transform.position,
                           point.position) >
                       ObjForLine.transform.localScale.y)
                {
                    Vector3 Offset =
                        (point.position - CubeInstList[CubeInstList.Count - 1].transform.position).normalized *
                        ObjForLine.transform.localScale.y;

                    CubeInstList.Add(Instantiate(ObjForLine,
                        CubeInstList[CubeInstList.Count - 1].transform.position + Offset,
                        ObjForLine.transform.rotation));
                }
            }
        }

        if (DoughnutInst != null &&
            Vector3.Distance(MidPoints[MidPoints.Length - 1].position, DoughnutInst.transform.position) <
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
        GameController.Instance.ScoreBoard.PointsAddAnim(300);
        GameController.Instance.Particles.CreateParticle(Particles.NaszeParticle.ThreeHundredPoints,
            DoughnutInst.transform.position);
        GameController.Instance.SoundManager.Play3DAt(SamplesList.Correct, DoughnutInst.transform.position, 0.01f);
        Clear();
    }

    private void Clear()
    {
        foreach (var cube in CubeInstList)
        {
            Destroy(cube);
        }
        CubeInstList.Clear();
        Destroy(DoughnutInst);
        Capsule.gameObject.GetComponent<AnimControlOnGrab>().Restart();
    }
}