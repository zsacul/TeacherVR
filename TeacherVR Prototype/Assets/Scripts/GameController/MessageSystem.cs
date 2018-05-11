using System.Collections;
using TMPro;
using UnityEngine;
using VRTK;

public class MessageSystem : MonoBehaviour
{
    public GameObject MessageWindow;

    public TipsForButtonsController LeftTooltips;
    public TipsForButtonsController RightTooltips;

    private GameObject HeadsetFollower;

    public ObjectToFollow ActiveFollower = ObjectToFollow.Headset;

    public GameObject Window6x7;
    public GameObject Window8x4;

    /*public GameObject ProgressBar;
      public Color ProgressColor;
      public Color EndColor;*/

    private VRTK_TransformFollow TransformFollow;

    private VRTK_ControllerEvents LeftHand;
    private VRTK_ControllerEvents RightHand;


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
        StartCoroutine(ShowButtonDelay(button, txt, time));
    }

    IEnumerator ShowButtonDelay(Button button, string txt, float time)
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        if (GameController.Instance.Tooltips) {
            switch (button)
            {
                case Button.Trigger:
                    LeftTooltips.ButtonTip = TipsForButtonsController.Button.Trigger;
                    RightTooltips.ButtonTip = TipsForButtonsController.Button.Trigger;
                    LeftTooltips.tmpugui.text = txt;
                    RightTooltips.tmpugui.text = txt;
                    break;
                case Button.Grip:
                    LeftTooltips.ButtonTip = TipsForButtonsController.Button.Grip;
                    RightTooltips.ButtonTip = TipsForButtonsController.Button.Grip;
                    LeftTooltips.tmpugui.text = txt;
                    RightTooltips.tmpugui.text = txt;
                    break;
                case Button.Touchpad:
                    LeftTooltips.ButtonTip = TipsForButtonsController.Button.Touchpad;
                    RightTooltips.ButtonTip = TipsForButtonsController.Button.Touchpad;
                    LeftTooltips.tmpugui.text = txt;
                    RightTooltips.tmpugui.text = txt;
                    break;
                case Button.ButtonTwo:
                    LeftTooltips.ButtonTip = TipsForButtonsController.Button.Button2;
                    RightTooltips.ButtonTip = TipsForButtonsController.Button.Button2;
                    LeftTooltips.tmpugui.text = txt;
                    RightTooltips.tmpugui.text = txt;
                    break;
                case Button.StartMenu:
                    LeftTooltips.ButtonTip = TipsForButtonsController.Button.SteamMenu;
                    RightTooltips.ButtonTip = TipsForButtonsController.Button.SteamMenu;
                    LeftTooltips.tmpugui.text = txt;
                    RightTooltips.tmpugui.text = txt;
                    break;
            }
            InvokeRepeating("RepeatPulse", 1, 2);
            ChangTooltipsState(true);
            StartCoroutine(HideAllButtons(time));
        }
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
        if (!LeftTooltips.gameObject.activeSelf || !RightTooltips.gameObject.activeSelf) return;
        switch (button)
        {
            case Button.Trigger:
                if (LeftTooltips.ButtonTip == TipsForButtonsController.Button.Trigger)
                {
                    CancelInvoke("RepeatPulse");
                    LeftTooltips.tmpugui.text = "";
                    RightTooltips.tmpugui.text = "";
                    ChangTooltipsState(false);
                }
                break;
            case Button.Grip:
                if (LeftTooltips.ButtonTip == TipsForButtonsController.Button.Grip)
                {
                    CancelInvoke("RepeatPulse");
                    LeftTooltips.tmpugui.text = "";
                    RightTooltips.tmpugui.text = "";
                    ChangTooltipsState(false);
                }
                break;
            case Button.Touchpad:
                if (LeftTooltips.ButtonTip == TipsForButtonsController.Button.Touchpad)
                {
                    CancelInvoke("RepeatPulse");
                    LeftTooltips.tmpugui.text = "";
                    RightTooltips.tmpugui.text = "";
                    ChangTooltipsState(false);
                }
                break;
            case Button.ButtonTwo:
                if (LeftTooltips.ButtonTip == TipsForButtonsController.Button.Button2)
                {
                    CancelInvoke("RepeatPulse");
                    LeftTooltips.tmpugui.text = "";
                    RightTooltips.tmpugui.text = "";
                    ChangTooltipsState(false);
                }
                break;
            case Button.StartMenu:
                if (LeftTooltips.ButtonTip == TipsForButtonsController.Button.SteamMenu)
                {
                    CancelInvoke("RepeatPulse");
                    LeftTooltips.tmpugui.text = "";
                    RightTooltips.tmpugui.text = "";
                    ChangTooltipsState(false);
                }
                break;
        }
    }

    IEnumerator ActiveDelay(bool val)
    {
        yield return new WaitForEndOfFrame();
        LeftTooltips.gameObject.SetActive(val);
        RightTooltips.gameObject.SetActive(val);
    }

    private void ChangTooltipsState(bool val)
    {
        StartCoroutine(ActiveDelay(val));
    }

    void RepeatPulse()
    {
        VRTK_ControllerHaptics.TriggerHapticPulse(
            VRTK_ControllerReference.GetControllerReference(LeftHand.gameObject), 0.63f, 0.2f, 0.01f);
        VRTK_ControllerHaptics.TriggerHapticPulse(
            VRTK_ControllerReference.GetControllerReference(RightHand.gameObject), 0.63f, 0.2f, 0.01f);
    }

    public void HideAllButtons()
    {
        HideAllButtonsOnController(LeftTooltips);
        HideAllButtonsOnController(RightTooltips);
        ChangTooltipsState(false);
        CancelInvoke("RepeatPulse");
    }

    public IEnumerator HideAllButtons(float time)
    {
        yield return new WaitForSeconds(time);
        HideAllButtonsOnController(LeftTooltips);
        HideAllButtonsOnController(RightTooltips);
        ChangTooltipsState(false);
        CancelInvoke("RepeatPulse");
    }

    private void HideAllButtonsOnController(TipsForButtonsController tooltips)
    {
        tooltips.tmpugui.text = "";
        CancelInvoke("RepeatPulse");
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