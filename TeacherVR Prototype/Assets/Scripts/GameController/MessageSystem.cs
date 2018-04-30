using System.Collections;
using TMPro;
using UnityEngine;
using VRTK;

public class MessageSystem : MonoBehaviour
{
    public GameObject MessageWindow;

    public GameObject LeftTooltips;
    public GameObject RightTooltips;

    private GameObject HeadsetFollower;

    public ObjectToFollow ActiveFollower = ObjectToFollow.Headset;

    public GameObject Window6x7;
    public GameObject Window8x4;

  /*public GameObject ProgressBar;
    public Color ProgressColor;
    public Color EndColor;*/

    private VRTK_TransformFollow TransformFollow;

    private VRTK_ControllerEvents RightHand;
    private VRTK_ControllerEvents LeftHand;

    private void Start()
    {
        TransformFollow = MessageWindow.GetComponent<VRTK_TransformFollow>();
        ChangeActiveFollower(ActiveFollower);
        StartCoroutine(FindDevices());
    }

    private IEnumerator FindDevices()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();

        HeadsetFollower = VRTK_DeviceFinder.HeadsetTransform().gameObject;
        RightHand = VRTK_DeviceFinder.GetControllerRightHand().GetComponent<VRTK_ControllerEvents>();
        LeftHand = VRTK_DeviceFinder.GetControllerLeftHand().GetComponent<VRTK_ControllerEvents>();

        RightHand.GetComponent<VRTK_ControllerEvents>().TriggerPressed += TriggerPressed;
        LeftHand.GetComponent<VRTK_ControllerEvents>().TriggerPressed += TriggerPressed;
        RightHand.GetComponent<VRTK_ControllerEvents>().GripPressed += GripPressed;
        LeftHand.GetComponent<VRTK_ControllerEvents>().GripPressed += GripPressed;
        RightHand.GetComponent<VRTK_ControllerEvents>().TouchpadPressed += TouchpadPressed;
        LeftHand.GetComponent<VRTK_ControllerEvents>().TouchpadPressed += TouchpadPressed;
        RightHand.GetComponent<VRTK_ControllerEvents>().ButtonTwoPressed += ButtonTwoPressed;
        LeftHand.GetComponent<VRTK_ControllerEvents>().ButtonTwoPressed += ButtonTwoPressed;
        RightHand.GetComponent<VRTK_ControllerEvents>().StartMenuPressed += StartMenuPressed;
        LeftHand.GetComponent<VRTK_ControllerEvents>().StartMenuPressed += StartMenuPressed;
    }

    /*private void OnDestroy()
    {
        RightHand.GetComponent<VRTK_ControllerEvents>().TriggerPressed -= TriggerPressed;
        LeftHand.GetComponent<VRTK_ControllerEvents>().TriggerPressed -= TriggerPressed;
        RightHand.GetComponent<VRTK_ControllerEvents>().GripPressed -= GripPressed;
        LeftHand.GetComponent<VRTK_ControllerEvents>().GripPressed -= GripPressed;
        RightHand.GetComponent<VRTK_ControllerEvents>().TouchpadPressed -= TouchpadPressed;
        LeftHand.GetComponent<VRTK_ControllerEvents>().TouchpadPressed -= TouchpadPressed;
        RightHand.GetComponent<VRTK_ControllerEvents>().ButtonTwoPressed -= ButtonTwoPressed;
        LeftHand.GetComponent<VRTK_ControllerEvents>().ButtonTwoPressed -= ButtonTwoPressed;
        RightHand.GetComponent<VRTK_ControllerEvents>().StartMenuPressed -= StartMenuPressed;
        LeftHand.GetComponent<VRTK_ControllerEvents>().StartMenuPressed -= StartMenuPressed;
    }*/

    private void TriggerPressed(object sender, ControllerInteractionEventArgs e)
    {
        HideButton(Button.Trigger);
    }

    private void GripPressed(object sender, ControllerInteractionEventArgs e)
    {
        HideButton(Button.Grip);
    }

    private void TouchpadPressed(object sender, ControllerInteractionEventArgs e)
    {
        HideButton(Button.Touchpad);
    }

    private void ButtonTwoPressed(object sender, ControllerInteractionEventArgs e)
    {
        HideButton(Button.ButtonTwo);
    }

    private void StartMenuPressed(object sender, ControllerInteractionEventArgs e)
    {
        HideButton(Button.StartMenu);
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

    public enum Button
    {
        Trigger,
        Grip,
        Touchpad,
        ButtonTwo,
        StartMenu
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
                TransformFollow.gameObjectToFollow = RightHand.gameObject;
                break;
            case ObjectToFollow.LeftHand:
                TransformFollow.gameObjectToFollow = LeftHand.gameObject;
                break;
        }

        TransformFollow.enabled = !TransformFollow.enabled;
        TransformFollow.enabled = !TransformFollow.enabled;
    }

    public void ChangeState()
    {
        MessageWindow.SetActive(!MessageWindow.activeSelf);
    }

    public void ShowCustomText(string text, Window window)
    {
        if (!GameController.Instance.Messages) return;
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

    public void ShowButtonOnControllers(Button button, string txt, float time)
    {
        return;
        if (!GameController.Instance.Tooltips) return;
        switch (button)
        {
            case Button.Trigger:
                LeftTooltips.GetComponent<VRTK_ControllerTooltips>().triggerText = txt;
                RightTooltips.GetComponent<VRTK_ControllerTooltips>().triggerText = txt;
                break;
            case Button.Grip:
                LeftTooltips.GetComponent<VRTK_ControllerTooltips>().gripText = txt;
                RightTooltips.GetComponent<VRTK_ControllerTooltips>().gripText = txt;
                break;
            case Button.Touchpad:
                LeftTooltips.GetComponent<VRTK_ControllerTooltips>().touchpadText = txt;
                RightTooltips.GetComponent<VRTK_ControllerTooltips>().touchpadText = txt;
                break;
            case Button.ButtonTwo:
                LeftTooltips.GetComponent<VRTK_ControllerTooltips>().buttonTwoText = txt;
                RightTooltips.GetComponent<VRTK_ControllerTooltips>().buttonTwoText = txt;
                break;
            case Button.StartMenu:
                LeftTooltips.GetComponent<VRTK_ControllerTooltips>().startMenuText = txt;
                RightTooltips.GetComponent<VRTK_ControllerTooltips>().startMenuText = txt;
                break;
        }

        ChangTooltipsState(true);
        StartCoroutine(HideAllButtons(time));
    }

    /*public void SetProgressBar(float progress)
    {
        if (progress < 0) progress = 0;
        if (progress > 100) progress = 100;

        if (progress > 99) ProgressBar.GetComponentInChildren<TextMeshProUGUI>().color = EndColor;
        else ProgressBar.GetComponentInChildren<TextMeshProUGUI>().color = ProgressColor;

        ProgressBar.GetComponentInChildren<TextMeshProUGUI>().text = new string('█',
            (int) progress / 10);
    }


    public float GetProgress()
    {
        return ProgressBar.GetComponentInChildren<TextMeshProUGUI>().text.Length * 10;
    }*/

    private void HideButton(Button button)
    {
        if (!LeftTooltips.activeSelf || !RightTooltips.activeSelf) return;
        switch (button)
        {
            case Button.Trigger:
                LeftTooltips.GetComponent<VRTK_ControllerTooltips>().triggerText = "";
                RightTooltips.GetComponent<VRTK_ControllerTooltips>().triggerText = "";
                break;
            case Button.Grip:
                LeftTooltips.GetComponent<VRTK_ControllerTooltips>().gripText = "";
                RightTooltips.GetComponent<VRTK_ControllerTooltips>().gripText = "";
                break;
            case Button.Touchpad:
                LeftTooltips.GetComponent<VRTK_ControllerTooltips>().touchpadText = "";
                RightTooltips.GetComponent<VRTK_ControllerTooltips>().touchpadText = "";
                break;
            case Button.ButtonTwo:
                LeftTooltips.GetComponent<VRTK_ControllerTooltips>().buttonTwoText = "";
                RightTooltips.GetComponent<VRTK_ControllerTooltips>().buttonTwoText = "";
                break;
            case Button.StartMenu:
                LeftTooltips.GetComponent<VRTK_ControllerTooltips>().startMenuText = "";
                RightTooltips.GetComponent<VRTK_ControllerTooltips>().startMenuText = "";
                break;
        }

        ChangTooltipsState(false);
        ChangTooltipsState(true);
    }

    private void ChangTooltipsState(bool val)
    {
        LeftTooltips.SetActive(val);
        RightTooltips.SetActive(val);
    }

    public void HideAllButtons()
    {
        HideAllButtonsOnController(LeftTooltips.GetComponent<VRTK_ControllerTooltips>());
        HideAllButtonsOnController(RightTooltips.GetComponent<VRTK_ControllerTooltips>());
        ChangTooltipsState(false);
    }

    public IEnumerator HideAllButtons(float time)
    {
        yield return new WaitForSeconds(time);
        HideAllButtonsOnController(LeftTooltips.GetComponent<VRTK_ControllerTooltips>());
        HideAllButtonsOnController(RightTooltips.GetComponent<VRTK_ControllerTooltips>());
        ChangTooltipsState(false);
    }

    private void HideAllButtonsOnController(VRTK_ControllerTooltips tooltips)
    {
        tooltips.triggerText = "";
        tooltips.gripText = "";
        tooltips.touchpadText = "";
        tooltips.buttonOneText = "";
        tooltips.buttonTwoText = "";
        tooltips.startMenuText = "";
    }

    public void HideAllWindows()
    {
        MessageWindow.SetActive(false);

        for (int i = 0; i < System.Enum.GetValues(typeof(Window)).Length; i++)
            HideWindow((Window) i);
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