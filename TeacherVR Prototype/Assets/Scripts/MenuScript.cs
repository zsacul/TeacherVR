using System.Collections;
using TMPro;

namespace VRTK.Examples
{
    using UnityEngine;
    using UnityEventHelper;

    public class MenuScript : MonoBehaviour
    {
        public TextMeshProUGUI TMPUGUI;
        public Transform MenuDestinationPoint;

        public Mode mode;

        private Vector3 pos;
        private Quaternion rot;

        public enum Mode
        {
            StartGame,
            Volume,
            Tooltips,
            Messages,
            ArmSwinger
        }

        private VRTK_Control_UnityEvents controlEvents;

        private void Start()
        {
            controlEvents = GetComponent<VRTK_Control_UnityEvents>();
            if (controlEvents == null)
            {
                controlEvents = gameObject.AddComponent<VRTK_Control_UnityEvents>();
            }
            controlEvents.OnValueChanged.AddListener(HandleChange);

            DoAction();
        }

        public void DoAction()
        {
            switch (mode)
            {
                case Mode.StartGame:
                    TMPUGUI.text = "Open to start";
                    pos = transform.GetChild(0).position;
                    rot = transform.GetChild(0).rotation;
                    break;
                case Mode.Volume:
                    TMPUGUI.text = "Sound (" + GameController.Instance.SoundManager.GetGlobalVolume() * 10 + "%)";
                    break;
                case Mode.Tooltips:
                    if (GameController.Instance.Tooltips) TMPUGUI.text = "Tooltips On";
                    else TMPUGUI.text = "Tooltips Off";
                    break;
                case Mode.Messages:
                    if (GameController.Instance.Messages) TMPUGUI.text = "Messages On";
                    else TMPUGUI.text = "Messages Off";
                    break;
                case Mode.ArmSwinger:
                    if (GameController.Instance.MoveInPlaceL.enabled) TMPUGUI.text = "ArmSwinger On";
                    else TMPUGUI.text = "ArmSwinger Off";
                    break;
            }
        }

        private void TeleportToStart()
        {
            GetComponent<ModelVillage_TeleportLocation>().ForceTeleport();
        }

        public void TeleportToMenu()
        {
            GetComponent<ModelVillage_TeleportLocation>().ForceTeleportTo(MenuDestinationPoint);
        }

        private void HandleChange(object sender, Control3DEventArgs e)
        {
            switch (mode)
            {
                case Mode.StartGame:
                    if (e.normalizedValue > 50)
                    {
                        TMPUGUI.text = "";
                        TeleportToStart();
                        transform.GetChild(0).position = pos;
                        transform.GetChild(0).rotation = rot;
                        GameController.Instance.EventsManager.Restart();
                        GameController.Instance.ScoreBoard.PointsChange(0);
                        GameController.Instance.ScoreBoard.ChangeTime(GameController.Instance.GameTime, 0);
                        GameController.Instance.ScoreBoard.ChangeTimeCounting(true);
                    }
                    break;
                case Mode.Volume:
                    TMPUGUI.text = "Sound (" + (100 - e.normalizedValue) + "%)";
                    GameController.Instance.SoundManager.SetGlobalVolume((100 - e.normalizedValue) / 10);
                    break;
                case Mode.Tooltips:
                    if (e.normalizedValue < 50) GameController.Instance.Tooltips = true;
                    else GameController.Instance.Tooltips = false;
                    if (GameController.Instance.Tooltips) TMPUGUI.text = "Tooltips On";
                    else TMPUGUI.text = "Tooltips Off";
                    break;
                case Mode.Messages:
                    if (e.normalizedValue < 50) GameController.Instance.Messages = true;
                    else GameController.Instance.Messages = false;
                    if (GameController.Instance.Messages) TMPUGUI.text = "Messages On";
                    else TMPUGUI.text = "Messages Off";
                    break;
                case Mode.ArmSwinger:
                    if (e.normalizedValue < 50) GameController.Instance.ChangeLocomotion(false);
                    else GameController.Instance.ChangeLocomotion(true);
                    if (GameController.Instance.MoveInPlaceL.enabled) TMPUGUI.text = "ArmSwinger On";
                    else TMPUGUI.text = "ArmSwinger Off";
                    break;
            }
        }
    }
}