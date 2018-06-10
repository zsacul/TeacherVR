using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
[CreateAssetMenu(fileName = "Speech", menuName = "Events/Speech")]
public class Speech : Events
{
   /* [Header("Custom Settings")]
    public static List <string> TextToSay;
    static bool SaidEverything = false;
    public GameObject TTS_prefab;
    private GameObject TTS_instance;
    private static TextMeshProUGUI txt;
    private static string whole_text;
    static Vector3 particlePos;

    public override void StartEvent()
    {
        base.StartEvent();
        if (!MicInput.isRecognitionSupported)
            CompleteEvent();
        else
        {
            Message(10, description, MessageSystem.ObjectToFollow.Headset, MessageSystem.Window.W800H400);
            TextToSay = new List<string>();
            TTS_instance = Instantiate(TTS_prefab);
            for (int i = 0; i <= Lvl; i++)
            {
                TextToSay.Add(GameController.Instance.MicInput.getRandomWord());
            }
            txt = TTS_instance.GetComponentInChildren<TextMeshProUGUI>();
            particlePos = TTS_instance.transform.GetChild(1).transform.position;
            prepareTextToDisplay();
            txt.text = whole_text;
            SaidEverything = false;
            MicInput.typeOfInput= MicInput.MicInputType.speechDetection;
        }

    }

    public override void CompleteEvent()
    {
        base.CompleteEvent();
        if (MicInput.isRecognitionSupported)
        {
            GameController.Instance.Particles.CreateParticle(Particles.NaszeParticle.ThreeHundredPoints, particlePos);
            AddPoints(300);
            Destroy(TTS_instance);
        }
            MicInput.typeOfInput = MicInput.MicInputType.noone;
    }
    public override void AbortEvent()
    {
        base.AbortEvent();
        Destroy(TTS_instance);
        MicInput.typeOfInput = MicInput.MicInputType.noone;
    }




    public override void CallInUpdate()
    {
        base.CallInUpdate();    
        if (SaidEverything)
            CompleteEvent();
    }
    public static void iSaid(string word)
    {
        if (TextToSay[0].Equals(word))
        {
            TextToSay.RemoveAt(0);
             GameController.Instance.Particles.CreateParticle(Particles.NaszeParticle.Small_Good_Correct_Ok,particlePos);
            if (TextToSay.Count == 0)
            {
                SaidEverything = true;
            }
            else
            {
                prepareTextToDisplay();
                txt.text = whole_text;
            }
        }
    }
    static void prepareTextToDisplay()
    {
        whole_text = "";
        foreach (string word in TextToSay)
        {
            whole_text += " " + word;
        }
    }*/
}
