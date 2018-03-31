using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EventsUIController : MonoBehaviour
{
    public TextMeshProUGUI EventName;
    public TextMeshProUGUI Description;
    public GameObject ListOfEvents;
    public GameObject ExampleButton;
    
    void Start()
    {
        GameController.Instance.EventsManager.EventsManagerStartNext += UpdateList;
        StartCoroutine(FirstUpdate());
    }

    private IEnumerator FirstUpdate()
    {
        yield return new WaitForEndOfFrame();
        UpdateList();
    }

    private void UpdateList()
    {
        if (GameController.Instance.EventsManager.GetCurrentEvent() != null)
        {
            EventName.text = GameController.Instance.EventsManager.GetCurrentEvent().name;
            Description.text = GameController.Instance.EventsManager.GetCurrentEvent().description;
        }
        foreach (Events e in GameController.Instance.EventsManager.ListOfEvents)
        {
            GameObject newButton = Instantiate(ExampleButton);
            newButton.transform.Find("Text").GetComponent<Text>().text = e.name;
            newButton.transform.SetParent(ListOfEvents.transform, false);
        }
    }
}