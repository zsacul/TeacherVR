using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private TouchDetector td;

    public override void StartEvent()
    {
        base.StartEvent();
        PC = GameObject.FindGameObjectWithTag("PCEvent");
        MonitorRenderer = PC.transform.Find("Monitor").gameObject.GetComponent<Renderer>();
        PCOffMaterial = MonitorRenderer.material;
        USB1Ins = Instantiate(USBCable, Cable1Transform.position, USBCable.transform.rotation);
        USB2Ins = Instantiate(USBCable, Cable2Transform.position, USBCable.transform.rotation);

        td = PC.GetComponentInChildren<TouchDetector>();
    }

    //Poprawić optymalizacje!
    public override void CallInUpdate()
    {
        if (td.Trigger && 
            PC.transform.Find("PC").Find("USBPort1").Find("SnapDropZone").Find("USBCable") != null &&
            PC.transform.Find("PC").Find("USBPort2").Find("SnapDropZone").Find("USBCable") != null &&
            PC.transform.Find("Monitor").Find("MicroUSBPort").Find("SnapDropZone").Find("MicroUSBCable") != null &&
            PC.transform.Find("Keyboard").Find("MicroUSBPort").Find("SnapDropZone").Find("MicroUSBCable") != null)
        {
            MonitorRenderer.material = PCOnMaterial;
            CompleteEvent();
        }
    }

    public override void AbortEvent()
    {
        base.AbortEvent();
        MonitorRenderer.material = PCOffMaterial;
        Destroy(USB1Ins);
        Destroy(USB2Ins);
    }

    public override void CompleteEvent()
    {
        base.CompleteEvent();
    }
}