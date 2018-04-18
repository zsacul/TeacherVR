using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Drawing Event", menuName = "Events/Drawing Event")]
public class Drawing : Events
{
    public Vector2[] TemplateShape;
    public GameObject Chalk_Tutorial_Point;

    private Rysowanie rysowanie;
    private VRTK_Senello_TexturePainter painter;
    private GameObject Chalk_Tutorial_Point_Inst;

    public override void StartEvent()
    {
        base.StartEvent();
        Message(5,description,MessageSystem.ObjectToFollow.Headset,MessageSystem.Window.W800H400);
        GameController.Instance.TemplateShape = TemplateShape;
        rysowanie = GameObject.Find("Triple_Board/Right/3rd_layer/Board").GetComponent<Rysowanie>();
        rysowanie.enabled = true;
        rysowanie.gameInProgress = true;
        painter = rysowanie.Paint;
        painter.Clear();
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
        GameController.Instance.TemplateShape = null;
        Destroy(Chalk_Tutorial_Point_Inst);
    }

    public override void CompleteEvent()
    {
        base.CompleteEvent();
        rysowanie.enabled = false;
        GameController.Instance.TemplateShape = null;
        Destroy(Chalk_Tutorial_Point_Inst);
    }
}