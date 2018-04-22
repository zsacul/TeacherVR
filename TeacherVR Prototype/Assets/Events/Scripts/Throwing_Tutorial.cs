using UnityEngine;

//Tu należy zmienić Events/Basic Event na Events/MojaNazwaEventu Event podczas dziedziczenia
//Dziedziczymy tworząc nowy skrypt i wpisując public class MojaNazwaEventu : Events {}
[CreateAssetMenu(fileName = "Throwing_Tutorial", menuName = "Events/Throwing_Tutorial Event")]
public class Throwing_Tutorial : Events
{
    
    private GameObject targets;
    private GameObject chalk;
    //public int Lvl = 0;

    private Target_Control tc;
    private ActivateStudents act;
    private GameObject chalk_throw_tutorial_point;
    private Tutorial_Point_Anim_Control script;

    public override void StartEvent()
    {
        base.StartEvent();
        targets = GameObject.Find("Students");
        chalk = GameObject.Find("PackOfChalk 2");
        GameController.Instance.MessageSystem.ShowButtonOnControllers(MessageSystem.Button.Trigger, "Grab", 60);
        //tc = targets.GetComponent<Target_Control>();
        act = targets.GetComponent<ActivateStudents>();
        chalk_throw_tutorial_point = chalk.transform.Find("Chalk_Throw_Tutorial_Point").gameObject;
        chalk_throw_tutorial_point.SetActive(true);
        script = chalk_throw_tutorial_point.GetComponent<Tutorial_Point_Anim_Control>();
        activate();

    }

    private void activate() {
        if (Lvl == 1){
            act.throwRow(1, true);
            act.throwRow(1, false);
        }
        else if (Lvl == 2) {
            act.throwRow(2, true);
            act.throwRow(2, false);
        }
        else {
            act.throwRow(3, true);
            act.throwRow(4, false);
            act.throwRow(3, false);
        }
    }
   
    public override void AbortEvent()
    {   
        script.Kill();
        base.AbortEvent();
    }


    public override void CompleteEvent()
    {
        script.Kill();
        base.CompleteEvent();
    }

    public override void CallInUpdate()
    {
        if(act.Destruction())CompleteEvent();
    }

    
}