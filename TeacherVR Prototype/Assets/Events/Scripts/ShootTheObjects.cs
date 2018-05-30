using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using VRTK.Examples;

[CreateAssetMenu(fileName = "New ShootTheObjects Event", menuName = "Events/ShootTheObjects Event")]
public class ShootTheObjects : Events
{
    [Header("Custom Settings")]
    public GameObject ObjectToShoot;

    [Range(0.1f, 10f)] public float MoveSpeed = 1f;
    [Range(0.1f, 10f)] public float AngularSpeed = 2f;

    public float ObjectScale = 1f;

    public Vector3 MinPos = new Vector3(2f, 3f, 1f);
    public Vector3 MaxPos = new Vector3(18f, 6f, 15f);

    private List<GameObject> Instances = new List<GameObject>();
    private List<Vector3> TargetLocations = new List<Vector3>();

    private Vector3 RandomPos()
    {
        return new Vector3(Random.Range(MinPos.x, MaxPos.x), Random.Range(MinPos.y, MaxPos.y),
            Random.Range(MinPos.z, MaxPos.z));
    }

    public override void StartEvent()
    {
        base.StartEvent();
        //GameController.Instance.MessageSystem.ShowButtonOnControllers(MessageSystem.Button.Grip, "Take the spray bottle", 60);
        Message(5, description, MessageSystem.ObjectToFollow.Headset, MessageSystem.Window.W800H400);
        Instances.Clear();
        TargetLocations.Clear();
        for (int i = 0; i < Lvl * 2; i++)
        {
            GameObject obj = Instantiate(ObjectToShoot, RandomPos(),
                ObjectToShoot.transform.rotation);
            obj.transform.localScale *= ObjectScale;
            TargetLocations.Add(RandomPos());
            Instances.Add(obj);
        }
    }

    public override void CallInUpdate()
    {
        int alive = 0;
        int i = 0;
        foreach (GameObject obj in Instances)
        {
            if (obj != null)
            {
                alive++;
                float stepPos = MoveSpeed * Time.deltaTime;
                float stepRot = AngularSpeed * Time.deltaTime;
                if (Vector3.Distance(obj.transform.position, TargetLocations[i]) < 0.2f)
                    TargetLocations[i] = RandomPos();
                obj.transform.position =
                    Vector3.MoveTowards(obj.transform.position, TargetLocations[i], stepPos);
                obj.transform.rotation = Quaternion.Slerp(obj.transform.rotation,
                    Quaternion.LookRotation(TargetLocations[i] - obj.transform.position, Vector3.up), stepRot);
            }
            i++;
        }
        //SetProgressBar((float) (Lvl * 2 - alive) / (Lvl * 2) * 100);
        if (Instances.Count != 0 && alive == 0) CompleteEvent();
    }

    private void DestroySpray()
    {
        Transform LH = VRTK_DeviceFinder.GetControllerLeftHand().transform.parent.Find("Hand");
        Transform RH = VRTK_DeviceFinder.GetControllerRightHand().transform.parent.Find("Hand");

        if (LH == null || RH == null)
        {
            LH = VRTK_DeviceFinder.GetControllerLeftHand().transform.parent.Find("Model");
            RH = VRTK_DeviceFinder.GetControllerRightHand().transform.parent.Find("Model");
        }

        WaterSpray LHW = LH.GetComponentInChildren<WaterSpray>();
        WaterSpray RHW = RH.GetComponentInChildren<WaterSpray>();

        if (LHW != null) LHW.ForceDestroy();
        if (RHW != null) RHW.ForceDestroy();
    }

    public override void AbortEvent()
    {
        base.AbortEvent();
        foreach (GameObject obj in Instances)
        {
            if (obj != null) Destroy(obj);
        }
        Instances.Clear();
        TargetLocations.Clear();

       DestroySpray();
    }

    public override void CompleteEvent()
    {
        base.CompleteEvent();
        Instances.Clear();
        TargetLocations.Clear();

        DestroySpray();
    }
}