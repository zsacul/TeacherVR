using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Drawing Event", menuName = "Events/Drawing Event")]
public class Drawing : Events
{
    [Header("Custom Settings")]
    [Range(0, 5)]
    public int Board = 2;
    public Vector2[] TemplateShape;

    private Rysowanie rysowanie;
    private VRTK_Senello_TexturePainter painter;
    private GameObject Chalk_Tutorial_Point_Inst;

    public override void StartEvent()
    {
        base.StartEvent();
        Message(5, description, MessageSystem.ObjectToFollow.Headset, MessageSystem.Window.W800H400);
        GameController.Instance.DrawingManager.TemplateShape = TemplateShape;
        rysowanie = GameController.Instance.DrawingManager.Boards[Board];
        painter = rysowanie.Paint;
        painter.TotalClear();
        rysowanie.gameInProgress = true;
        rysowanie.enabled = true;
        Chalk_Tutorial_Point_Inst = GameObject.Find("PackOfChalk 2").transform.Find("Chalk_Grab_Tutorial_Point").gameObject;
        Chalk_Tutorial_Point_Inst.GetComponent<Tutorial_Point_Anim_Control>().Resurrect();
    }

    public override void CallInUpdate()
    {
        if (!rysowanie.gameInProgress)
        {
            CompleteEvent();
        }
    }

    public override void AbortEvent()
    {
        base.AbortEvent();
        rysowanie.gameInProgress = false;
        rysowanie.enabled = false;
        painter.TotalClear();
        GameController.Instance.DrawingManager.TemplateShape = null;
        Chalk_Tutorial_Point_Inst.GetComponent<Tutorial_Point_Anim_Control>().Abort();
    }

    public override void CompleteEvent()
    {
        base.CompleteEvent();
        rysowanie.enabled = false;
        painter.TotalClear();
        GameController.Instance.DrawingManager.TemplateShape = null;
        Chalk_Tutorial_Point_Inst.GetComponent<Tutorial_Point_Anim_Control>().Abort();
    }
}