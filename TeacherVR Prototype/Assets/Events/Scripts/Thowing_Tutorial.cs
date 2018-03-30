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

    public override void StartEvent()
    {
        base.StartEvent();
        targetsInstance = Instantiate(targets);
        chalkInstance = Instantiate(chalk);
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
        //Debug.Log("Doing " + name);
    }

    
}