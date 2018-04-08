using TMPro;
using UnityEngine;
using VRTK;

public class MessageSystem : MonoBehaviour
{
    public GameObject MessageWindow;

    public GameObject HeadsetFollower;
    public GameObject RightHandFollower;
    public GameObject LeftHandFollower;

    public ObjectToFollow ActiveFollower = ObjectToFollow.Headset;

    public GameObject Window6x7;
    public GameObject Window8x4;

    private VRTK_TransformFollow TransformFollow;

    private void Start()
    {
        TransformFollow = MessageWindow.GetComponent<VRTK_TransformFollow>();
        ChangeActiveFollower(ActiveFollower);
    }

    public enum ObjectToFollow
    {
        Headset,
        RightHand,
        LeftHand
    }

    public enum Window
    {
        W600H700,
        W800H400
    }

    public void ChangeActiveFollower(ObjectToFollow val)
    {
        ActiveFollower = val;
        switch (ActiveFollower)
        {
            case ObjectToFollow.Headset:
                TransformFollow.gameObjectToFollow = HeadsetFollower;
                break;
            case ObjectToFollow.RightHand:
                TransformFollow.gameObjectToFollow = RightHandFollower;
                break;
            case ObjectToFollow.LeftHand:
                TransformFollow.gameObjectToFollow = LeftHandFollower;
                break;
        }

        TransformFollow.enabled = !TransformFollow.enabled;
        TransformFollow.enabled = !TransformFollow.enabled;
    }

    public void ChangeState()
    {
        MessageWindow.SetActive(!MessageWindow.activeSelf);
    }

    public void ShowCustomText(string text, Window window, bool withClear)
    {
        if (withClear)
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }

        MessageWindow.SetActive(true);

        switch (window)
        {
            case Window.W600H700:
                Window6x7.SetActive(true);
                Window6x7.GetComponentInChildren<TextMeshProUGUI>().text = text;
                break;
            case Window.W800H400:
                Window8x4.SetActive(true);
                Window8x4.GetComponentInChildren<TextMeshProUGUI>().text = text;
                break;
        }
    }

    public void HideAllText()
    {
        MessageWindow.SetActive(false);

        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    public void HideWindow(Window window)
    {
        switch (window)
        {
            case Window.W600H700:
                Window6x7.SetActive(false);
                break;
            case Window.W800H400:
                Window8x4.SetActive(false);
                break;
        }
    }
}