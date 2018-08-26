namespace VRTK.Examples
{
    using UnityEngine;
    using UnityEngine.UI;

    public class UI_Keyboard : MonoBehaviour
    {
        private InputField input;

        private string nick;

        public void LateUpdate()
        {
            if (input.text.Length > 10) input.text = input.text.Remove(10);
        }

        public void Submit()
        {
            nick = input.text;
            PlayerPrefs.SetString("LastNick", nick);
            gameObject.SetActive(false);
        }

        public string getnick()
        {
            return nick;
        }

        private void Start()
        {
            input = GetComponentInChildren<InputField>();
            nick = PlayerPrefs.GetString("LastNick", "BSTTE");
        }
    }
}