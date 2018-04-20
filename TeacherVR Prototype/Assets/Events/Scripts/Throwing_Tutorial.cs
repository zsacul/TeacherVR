using UnityEngine;

//Tu należy zmienić Events/Basic Event na Events/MojaNazwaEventu Event podczas dziedziczenia
//Dziedziczymy tworząc nowy skrypt i wpisując public class MojaNazwaEventu : Events {}
[CreateAssetMenu(fileName = "Throwing_Tutorial", menuName = "Events/Throwing_Tutorial Event")]
public class Throwing_Tutorial : Events
{
    
    public GameObject targets;
    public GameObject chalk;

    private GameObject targetsInstance;
    private GameObject chalkInstance;

    private Target_Control tc;

    public override void StartEvent()
    {
        base.StartEvent();
        GameController.Instance.MessageSystem.ShowButtonOnControllers(MessageSystem.Button.Trigger, "Grab", 60);
        targetsInstance = Instantiate(targets);
        chalkInstance = Instantiate(chalk);
        tc = targetsInstance.GetComponent<Target_Control>();
    }

   
    public override void AbortEvent()
    {
        base.AbortEvent();
        Destroy(chalkInstance);
        Destroy(targetsInstance);
    }


    public override void CompleteEvent()
    {
        base.CompleteEvent();
        Destroy(chalkInstance);
        Destroy(targetsInstance);
    }

    public override void CallInUpdate()
    {
        if(tc.Destruction())CompleteEvent();
    }

    
}