using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsManager : MonoBehaviour
{
    public List<Events> ListOfEvents = new List<Events>();

    private Events currentEvent;

    public delegate void EventsManagerEventHandler();

    public event EventsManagerEventHandler EventsManagerStartNext;

    private void AddPoints(int pkt)
    {
        GameController.Instance.ScoreBoard.PointsAdd(pkt);
    }

    private void Message(float time, string txt, MessageSystem.ObjectToFollow objectToFollow, MessageSystem.Window window)
    {
        Debug.Log("Start Message");
        GameController.Instance.MessageSystem.ChangeActiveFollower(objectToFollow);
        GameController.Instance.MessageSystem.ShowCustomText(txt, window, true);
        StartCoroutine(Hide(time));
    }

    private IEnumerator Hide(float time)
    {
        yield return new WaitForSeconds(time);
        GameController.Instance.MessageSystem.HideAllText();
    }

    public void StartNextEvent()
    {
        if (currentEvent != null)
        {
            AbortCurrentEvent();
        }
        if (ListOfEvents.Count > 0)
        {
            currentEvent = ListOfEvents[0];
            currentEvent.AddPointsEvent += AddPoints;
            currentEvent.MessageEvent += Message;
            currentEvent.StartEvent();
            ListOfEvents.RemoveAt(0);
            
        }
        if (EventsManagerStartNext != null)
        {
            EventsManagerStartNext();
        }
    }

    public void AbortCurrentEvent()
    {
        currentEvent.AbortEvent();
        currentEvent.AddPointsEvent -= AddPoints;
        currentEvent.MessageEvent -= Message;
        currentEvent = null;
    }

    public void AddEvent(Events e)
    {
        ListOfEvents.Add(e);
    }

    public Events.Status CheckCurrentEventStatus()
    {
        return currentEvent.status;
    }

    public Events GetCurrentEvent()
    {
        return currentEvent;
    }

    public void Update()
    {
        if (currentEvent != null)
        {
            if (CheckCurrentEventStatus() == Events.Status.Progress) currentEvent.CallInUpdate();
            else
            {
                currentEvent.AddPointsEvent -= AddPoints;
                currentEvent.MessageEvent -= Message;
                currentEvent = null;
            }
        }
        else
        {
            StartNextEvent();
        }
    }
}