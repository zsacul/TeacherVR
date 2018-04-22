using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Noise", menuName = "Events/Noise")]
public class Noise : Events
{
    public static bool shoutedLoudEnough = false;
    public static bool doneSomethingLoudEnough = false;
    //Funkcja po której wywołaniu startuje event
    //Powinna zapamiętać na starcie parametry zmienianych obiektów
    public override void StartEvent()
    {
        base.StartEvent();
        Message(10, description, MessageSystem.ObjectToFollow.Headset, MessageSystem.Window.W800H400);
        shoutedLoudEnough = false;
        doneSomethingLoudEnough = false;
        Debug.Log("It's loud now!");
        MicInput.typeOfInput = MicInput.MicInputType.peakDetection;
        //Dodać jakiś dźwięk / hałas, animacja zamieszania wśród studentów?
    }

    public override void CompleteEvent()
    {
        base.CompleteEvent();
        Debug.Log("It's quiet now.");
        AddPoints(10);
        AbortEvent();
    }
    public override void AbortEvent()
    {
        base.AbortEvent();
        Debug.Log("End of noise.");
        //Wycofać dodany dźwięk / hałas, animacje zamieszania wśród studentów
    }




    public override void CallInUpdate()
    {
        base.CallInUpdate();
        // Debug.Log("Noise.");
        //GameController.Instance.SoundManager.Play3DAt();
        if (shoutedLoudEnough || doneSomethingLoudEnough)
        {
           // Debug.Log("Endasd");
            CompleteEvent();
        }

    }    
}
