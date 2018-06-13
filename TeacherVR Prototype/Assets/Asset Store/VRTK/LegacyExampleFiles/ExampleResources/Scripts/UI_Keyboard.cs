namespace VRTK.Examples
{
    using UnityEngine;
    using UnityEngine.UI;

    public class UI_Keyboard : MonoBehaviour
    {
        private InputField input;

        private string nick = "BSTTE";

        public void ClickKey(string character)
        {
            input.text += character;
        }

        public void Backspace()
        {
            if (input.text.Length > 0)
            {
                input.text = input.text.Substring(0, input.text.Length - 1);
            }
        }

        public void Enter()
        {
            VRTK_Logger.Info("You've typed [" + input.text + "]");
            if (nick.Length > 5) nick = input.text.Remove(5);
            else nick = input.text;
            input.text = "";
            gameObject.SetActive(false);
        }

        public string getnick()
        {
            return nick;
        }

        private void Start()
        {
            input = GetComponentInChildren<InputField>();
        }
    }
}