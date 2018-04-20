using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Drawing Event", menuName = "Events/Drawing Event")]
public class Drawing : Events
{
    [Range(0, 5)]
    public int Board = 2;
    public Vector2[] TemplateShape;
    public GameObject Chalk_Tutorial_Point;

    private Rysowanie rysowanie;
    private VRTK_Senello_TexturePainter painter;
    private GameObject Chalk_Tutorial_Point_Inst;

    public override void StartEvent()
    {
        base.StartEvent();
        GameController.Instance.MessageSystem.ShowButtonOnControllers(MessageSystem.Button.Trigger, "Grab", 60);
        Message(5, description, MessageSystem.ObjectToFollow.Headset, MessageSystem.Window.W800H400);
        GameController.Instance.DrawingManager.TemplateShape = TemplateShape;
        rysowanie = GameController.Instance.DrawingManager.Boards[Board];
        painter = rysowanie.Paint;
        painter.Clear();
        rysowanie.gameInProgress = true;
        rysowanie.enabled = true;
        Chalk_Tutorial_Point_Inst = Instantiate(Chalk_Tutorial_Point);
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
        painter.Clear();
        GameController.Instance.DrawingManager.TemplateShape = null;
        Destroy(Chalk_Tutorial_Point_Inst);
    }

    public override void CompleteEvent()
    {
        base.CompleteEvent();
        rysowanie.enabled = false;
        painter.Clear();
        GameController.Instance.DrawingManager.TemplateShape = null;
        Destroy(Chalk_Tutorial_Point_Inst);
    }
}