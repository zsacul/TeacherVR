using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

[CreateAssetMenu(fileName = "New TurnOnPC Event", menuName = "Events/TurnOnPC Event")]
public class TurnOnPC : Events
{
    public Material PCOnMaterial;

    public GameObject USBCable;

    public Transform Cable1Transform;
    public Transform Cable2Transform;

    private GameObject PC;

    private Renderer MonitorRenderer;
    private Material PCOffMaterial;

    private GameObject USB1Ins;
    private GameObject USB2Ins;

    Transform u1, u2, mu1, mu2;

    public override void StartEvent()
    {
        base.StartEvent();
        Message(5, description, MessageSystem.ObjectToFollow.Headset, MessageSystem.Window.W800H400);
        PC = GameObject.FindGameObjectWithTag("PCEvent");
        MonitorRenderer = PC.transform.Find("Monitor").gameObject.GetComponent<Renderer>();
        PCOffMaterial = MonitorRenderer.material;
        USB1Ins = Instantiate(USBCable, Cable1Transform.position, USBCable.transform.rotation);
        USB2Ins = Instantiate(USBCable, Cable2Transform.position, USBCable.transform.rotation);
    }

    //Poprawić optymalizacje! Może na eventach?
    public override void CallInUpdate()
    {
            u1 = PC.transform.Find("PC/USBPort1/SnapDropZone/USBCable");
            u2 = PC.transform.Find("PC/USBPort2/SnapDropZone/USBCable");
            mu1 = PC.transform.Find("Monitor/MicroUSBPort/SnapDropZone/MicroUSBCable");
            mu2 = PC.transform.Find("Keyboard/MicroUSBPort/SnapDropZone/MicroUSBCable");

            if (u1 != null && u2 != null && mu1 != null && mu2 != null)
            {
                MonitorRenderer.material = PCOnMaterial;
                u1.GetComponentInChildren<VRTK_InteractableObject>().isGrabbable = false;
                u2.GetComponentInChildren<VRTK_InteractableObject>().isGrabbable = false;
                mu1.GetComponentInChildren<VRTK_InteractableObject>().isGrabbable = false;
                mu2.GetComponentInChildren<VRTK_InteractableObject>().isGrabbable = false;
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
        if (mu1 != null) Destroy(mu1.gameObject);
        if (mu2 != null) Destroy(mu2.gameObject);
    }

    public override void CompleteEvent()
    {
        base.CompleteEvent();
        GameController.Instance.SoundManager.Play3DAt(SamplesList.ComputerBeep,PC.transform.position);
    }
}