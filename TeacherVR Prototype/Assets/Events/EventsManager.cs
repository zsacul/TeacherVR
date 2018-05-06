﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsManager : MonoBehaviour
{
    public List<Events> ListOfEvents = new List<Events>();

    public float delay = 2;

    private List<Events> EventsToMix = new List<Events>();
    private List<Events> ListOfEventsCopy = new List<Events>();

    private Events currentEvent;

    private bool CoroutineRunning;

    public delegate void EventsManagerEventHandler();

    public event EventsManagerEventHandler EventsManagerStartNext;

    private int EventNumber = 0;

    public void EndAllEvents()
    {
        ListOfEvents.Clear();
        EventsToMix.Clear();
        AbortCurrentEvent();
        EventNumber = 0;
    }

    public void Restart()
    {
        EndAllEvents();

        foreach (var var in ListOfEventsCopy)
        {
            AddEvent(var);
        }
        foreach (Events e in ListOfEvents)
        {
            if (e.Repeatable) EventsToMix.Add(e);
        }
        FillList();
        foreach (Events toDel in EventsToMix)
        {
            ListOfEvents.Remove(toDel);
        }
    }

    private void AddPoints(int pkt)
    {
        GameController.Instance.ScoreBoard.PointsAddAnim(pkt);
    }

    private void Message(float time, string txt, MessageSystem.ObjectToFollow objectToFollow,
        MessageSystem.Window window)
    {
        Debug.Log("Start Message");
        GameController.Instance.MessageSystem.ChangeActiveFollower(objectToFollow);
        GameController.Instance.MessageSystem.ShowCustomText(txt, window);
        StopAllCoroutines();
        StartCoroutine(Hide(time, window));
    }

    private IEnumerator Hide(float time, MessageSystem.Window window)
    {
        yield return new WaitForSeconds(time);
        GameController.Instance.MessageSystem.HideWindow(window);
    }

    public void StartNextEvent()
    {
        if (EventNumber == 0) StartNewEventFunction();
        else if (!CoroutineRunning) StartCoroutine(StartNewEventDelay(delay));
    }

    private void StartNewEventFunction()
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
            FillList();
            EventNumber++;
        }
        if (EventsManagerStartNext != null)
        {
            EventsManagerStartNext();
        }
    }

    private IEnumerator StartNewEventDelay(float time)
    {
        CoroutineRunning = true;
        yield return new WaitForSeconds(time);
        StartNewEventFunction();
        CoroutineRunning = false;
    }

    public void AbortCurrentEvent()
    {
        if (currentEvent != null)
        {
            currentEvent.AbortEvent();
            currentEvent.AddPointsEvent -= AddPoints;
            currentEvent.MessageEvent -= Message;
            currentEvent = null;
        }
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

    void Start()
    {
        foreach (var var in ListOfEvents)
        {
            ListOfEventsCopy.Add(var);
        }
        foreach (Events e in ListOfEvents)
        {
            if (e.Repeatable)
                for (int i = 0; i < e.Multiple; i++)
                    EventsToMix.Add(e);
        }
        FillList();
        foreach (Events toDel in EventsToMix)
        {
            ListOfEvents.Remove(toDel);
        }
    }

    void Update()
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

    void FillList()
    {
        string lastName = ListOfEvents[ListOfEvents.Count - 1].name;
        while (ListOfEvents.Count < 11)
        {
            Events tmp = EventsToMix[Random.Range(0, EventsToMix.Count)];
            while (lastName.Equals(tmp.name) && EventsToMix.Count >= 2)
            {
                tmp = EventsToMix[Random.Range(0, EventsToMix.Count)];
            }
            if (tmp.DeviationLvlRange >= 1)
                tmp.Lvl = tmp.MediumLvl + Random.Range(-1, 1) * Random.Range(1, tmp.DeviationLvlRange);
            AddEvent(tmp);
            lastName = tmp.name;
        }
    }
}