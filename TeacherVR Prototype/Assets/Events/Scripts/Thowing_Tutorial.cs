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

    public virtual void StartEvent()
    {
        base.StartEvent();
        targetsInstance = Instantiate(targets);
        chalkInstance = Instantiate(chalk);
        //Debug.Log("Starting " + name);
        //status = Status.Progress;
    }

    //Funkcja która przerywa w dowolnym momencie event i przywraca scenę do stanu z przed eventu
    public virtual void AbortEvent()
    {
        base.AbortEvent();
        Destroy(chalkInstance);
        Destroy(targetsInstance);
        //Debug.Log("Aborting " + name);
        //status = Status.Abort;
    }

    //Funkcja która poprawnie konczy Event
    public virtual void CompleteEvent()
    {
        base.AbortEvent();
        Destroy(chalk);
        Destroy(targets);
    }

    public virtual void CallInUpdate()
    {
        Debug.Log("Doing " + name);
    }

    
}