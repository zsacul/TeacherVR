using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;

namespace VRTK.Examples
{
    using UnityEngine;
    using UnityEventHelper;

    public class MenuScript : MonoBehaviour
    {
        public TextMeshProUGUI TMPUGUI;

        public Mode mode;

        private Vector3 pos;
        private Quaternion rot;

        private float lastTime = 0;
        private float delay = 1;

        public enum Mode
        {
            StartGame,
            PlayAgain,
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

            SetUI();
        }

        private void SetUI()
        {
            switch (mode)
            {
                case Mode.StartGame:
                    TMPUGUI.text = "Open to start game!";
                    pos = transform.GetChild(0).position;
                    rot = transform.GetChild(0).rotation;
                    transform.GetChild(0).GetComponent<Rigidbody>().freezeRotation = false;
                    break;
                case Mode.PlayAgain:
                    TMPUGUI.text = "Play again!";
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
                    if (GameController.Instance.TeleportR.enabled) TMPUGUI.text = "Teleport On";
                    else TMPUGUI.text = "ArmSwinger On";
                    break;
            }
        }

        private void HandleChange(object sender, Control3DEventArgs e)
        {
            switch (mode)
            {
                case Mode.StartGame:
                    if (e.normalizedValue <= 40)
                    {
                        TMPUGUI.text = "";
                        transform.GetChild(0).position = pos;
                        transform.GetChild(0).rotation = rot;
                        transform.GetChild(0).GetComponent<Rigidbody>().freezeRotation = true;
                        GameController.Instance.EventsManager.Restart();
                        GameController.Instance.ScoreBoard.RestartBoard();
                        GameController.Instance.ForceTeleportScript.ForceTeleportToStart();
                        GameController.Instance.StartGame();
                    }
                    break;
                case Mode.PlayAgain:
                    if (e.normalizedValue > 30)
                    {
                        GameController.Instance.RestartGame();
                    }
                    break;
                case Mode.Volume:
                    TMPUGUI.text = "Sound (" + (100 - e.normalizedValue) + "%)";
                    if (Time.time > lastTime + delay)
                    {
                        lastTime = Time.time;
                        GameController.Instance.SoundManager.Play3DAt(SamplesList.Correct, transform.position, 0.01f);
                    }
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
                    if (e.normalizedValue < 50) GameController.Instance.ChangeLocomotion(true);
                    else GameController.Instance.ChangeLocomotion(false);
                    if (GameController.Instance.TeleportR.enabled) TMPUGUI.text = "Teleport On";
                    else TMPUGUI.text = "ArmSwinger On";
                    break;
            }
        }
    }
}