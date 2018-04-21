using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Tu należy zmienić Events/Basic Event na Events/MojaNazwaEventu Event podczas dziedziczenia
//Dziedziczymy tworząc nowy skrypt i wpisując public class MojaNazwaEventu : Events {}
[CreateAssetMenu(fileName = "New Event", menuName = "Events/Basic Event")]
public class Events : ScriptableObject
{
    //Nazwa eventu
    new public string name = "New Event";

    //Opis eventu
    public string description;

    //Status eventu
    public Status status = Status.Nothing;

    //Aktualny poziom trudności eventu, jeśli event nie posiada różnych poziomów powinno być 0 w przeciwnym wypadku >=1
    public int Lvl = 0;

    //Sredni poziom trudnosci w razie powtarzania tego samego
    public int MediumLvl = 0;

    //Możliwe odchlenie poziomu w razie powtarzania tego samego
    public int DeviationLvlRange = 0;

    //Czy event jest powtarzalny
    public bool Repeatable = true;

    public delegate void AddPointsEventHandler(int numb);

    public delegate void MessageEventHandler(float time, string txt, MessageSystem.ObjectToFollow objectToFollow,
        MessageSystem.Window window);

    public event AddPointsEventHandler AddPointsEvent;

    public event MessageEventHandler MessageEvent;

    //Podświetlone miejsca do których trzeba podejść
    public GameObject GoTo;

    private GameObject GoToInst;

    public Transform GoToTransform;

    private void ShowGoTo()
    {
        if (GoTo != null && GoToTransform != null)
            GoToInst = Instantiate(GoTo, GoToTransform.position, GoToTransform.rotation);
    }

    private void DestroyGotTo()
    {
        if (GoToInst != null) Destroy(GoToInst);
    }

    //Funkcja po której wywołaniu startuje event
    //Powinna zapamiętać na starcie parametry zmienianych obiektów
    public virtual void StartEvent()
    {
        Debug.Log("Starting " + name);
        status = Status.Progress;
        ShowGoTo();
        ResetProgressBar();
    }

    //Funkcja która przerywa w dowolnym momencie event i przywraca scenę do stanu z przed eventu
    public virtual void AbortEvent()
    {
        Debug.Log("Aborting " + name);
        status = Status.Abort;
        DestroyGotTo();
        GameController.Instance.MessageSystem.HideAllButtons();
        ResetProgressBar();
    }

    //Funkcja która poprawnie konczy Event
    public virtual void CompleteEvent()
    {
        Debug.Log("Complete " + name);
        status = Status.Complete;
        DestroyGotTo();
        CompleteProgressBar();
    }

    public virtual void CallInUpdate()
    {
        Debug.Log("Doing " + name);
    }

    //Status eventu kolejno nie wystartował, jest wykonywany, został przerwany, został poprawnie zakończony
    public enum Status
    {
        Nothing,
        Progress,
        Abort,
        Complete
    }

    //Funkcja dodająca punkty dla gracza
    protected void AddPoints(int pkt)
    {
        if (AddPointsEvent != null)
        {
            Debug.Log(pkt + " points for " + name);
            AddPointsEvent(pkt);
        }
    }

    //Funkcja wysylajaca wiadomosc do gracza
    protected void Message(float time, string txt, MessageSystem.ObjectToFollow objectToFollow,
        MessageSystem.Window window)
    {
        if (MessageEvent != null)
        {
            Debug.Log(txt + " for " + time + " from " + name);
            MessageEvent(time, txt, objectToFollow, window);
        }
    }

    protected void CompleteProgressBar()
    {
        SetProgressBar(100);
    }

    protected void ResetProgressBar()
    {
        SetProgressBar(0);
    }

    protected void SetProgressBar(float progress)
    {
        GameController.Instance.MessageSystem.SetProgressBar(progress);
    }

    protected void AddProgress(float progress)
    {
        GameController.Instance.MessageSystem.SetProgressBar(
            GameController.Instance.MessageSystem.GetProgress() + progress);
    }
}