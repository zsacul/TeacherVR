using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

[CreateAssetMenu(fileName = "New TurnOnPC Event", menuName = "Events/TurnOnPC Event")]
public class TurnOnPC : Events
{
    [Header("Custom Settings")] public Material PCOnMaterial;

    public GameObject USBCable;

    public Transform Cable1Transform;
    public Transform Cable2Transform;

    private GameObject PC;

    private Renderer MonitorRenderer;
    private Material PCOffMaterial;

    private GameObject USB1Ins;
    private GameObject USB2Ins;

    private float lastTime = 0;
    private const float delayTime = 2;

    Transform u1, u2, u3, u4;

    public override void StartEvent()
    {
        base.StartEvent();
        lastTime = 0;
        if (USB1Ins != null) Destroy(USB1Ins);
        if (USB2Ins != null) Destroy(USB2Ins);
        if (u1 != null) Destroy(u1.gameObject);
        if (u2 != null) Destroy(u2.gameObject);
        if (u3 != null) Destroy(u3.gameObject);
        if (u4 != null) Destroy(u4.gameObject);
        GameController.Instance.MessageSystem.ShowButtonOnControllers(MessageSystem.Button.Trigger, "Grab", 60);
        Message(8, description, MessageSystem.ObjectToFollow.Headset, MessageSystem.Window.W800H400);
        PC = GameObject.FindGameObjectWithTag("PCEvent");
        MonitorRenderer = PC.transform.Find("Monitor").gameObject.GetComponent<Renderer>();
        PCOffMaterial = MonitorRenderer.material;
        USB1Ins = Instantiate(USBCable, Cable1Transform.position, USBCable.transform.rotation);
        USB2Ins = Instantiate(USBCable, Cable2Transform.position, USBCable.transform.rotation);
    }

    private bool CheckIfSameCable()
    {
        if (u1 != null && u2 != null && (
                u1.GetChild(0).gameObject.activeSelf &&
                u1.GetChild(0).GetComponent<Cable_Procedural_Simple>().GetEnd() == u2.GetChild(1) ||
                u2.GetChild(0).gameObject.activeSelf &&
                u2.GetChild(0).GetComponent<Cable_Procedural_Simple>().GetEnd() == u1.GetChild(1)))
            return true;
        if (u3 != null && u4 != null && (
                u3.GetChild(0).gameObject.activeSelf &&
                u3.GetChild(0).GetComponent<Cable_Procedural_Simple>().GetEnd() == u4.GetChild(1) ||
                u4.GetChild(0).gameObject.activeSelf &&
                u4.GetChild(0).GetComponent<Cable_Procedural_Simple>().GetEnd() == u3.GetChild(1)))
            return true;
        return false;
    }

    public override void CallInUpdate()
    {
        u1 = PC.transform.Find("PC/USBPort1/SnapDropZone/USBCable");
        u2 = PC.transform.Find("PC/USBPort2/SnapDropZone/USBCable");
        u3 = PC.transform.Find("Monitor/USBPort3/SnapDropZone/USBCable");
        u4 = PC.transform.Find("Keyboard/USBPort4/SnapDropZone/USBCable");

        if (CheckIfSameCable())
        {
            if (Time.time > lastTime + delayTime)
            {
                lastTime = Time.time;
                GameController.Instance.Particles.CreateParticle(Particles.NaszeParticle.Small_Wrong,
                    PC.transform.position + Vector3.up / 4);
                GameController.Instance.SoundManager.Play3DAt(SamplesList.Error, PC.transform.position, 0.01f);
            }
            return;
        }

        if (u1 != null && u2 != null && u3 != null && u4 != null)
        {
            CompleteEvent();
        }
    }

    public override void AbortEvent()
    {
        base.AbortEvent();
        MonitorRenderer.material = PCOffMaterial;
        if (USB1Ins != null) Destroy(USB1Ins);
        if (USB2Ins != null) Destroy(USB2Ins);
        if (u1 != null) Destroy(u1.gameObject);
        if (u2 != null) Destroy(u2.gameObject);
        if (u3 != null) Destroy(u3.gameObject);
        if (u4 != null) Destroy(u4.gameObject);
    }

    public override void CompleteEvent()
    {
        base.CompleteEvent();
        MonitorRenderer.material = PCOnMaterial;
        u1.GetComponentInChildren<VRTK_InteractableObject>().isGrabbable = false;
        u2.GetComponentInChildren<VRTK_InteractableObject>().isGrabbable = false;
        u3.GetComponentInChildren<VRTK_InteractableObject>().isGrabbable = false;
        u4.GetComponentInChildren<VRTK_InteractableObject>().isGrabbable = false;
        GameController.Instance.ScoreBoard.PointsAddAnim(200);
        GameController.Instance.Particles.CreateParticle(Particles.NaszeParticle.TwoHundredPoints,
            PC.transform.position + Vector3.up / 4);
        GameController.Instance.SoundManager.Play3DAt(SamplesList.ComputerBeep, PC.transform.position, 0.01f);
    }
}