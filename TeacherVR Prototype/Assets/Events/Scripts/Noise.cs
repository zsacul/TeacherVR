using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
[CreateAssetMenu(fileName = "Noise", menuName = "Events/Noise")]
public class Noise : Events
{
    [Header("Custom Settings")]
    public int[] loudStudents;
    public static GameObject detector;
    public static bool shoutedLoudEnough = false;
    public static bool doneSomethingLoudEnough = false;
    //Funkcja po której wywołaniu startuje event
    //Powinna zapamiętać na starcie parametry zmienianych obiektów
    public override void StartEvent()
    {
        base.StartEvent();
        Message(10, description, MessageSystem.ObjectToFollow.Headset, MessageSystem.Window.W800H400);
        GameController.Instance.MessageSystem.ShowButtonOnControllers(MessageSystem.Button.Grip, "Take a book", 60);
        
        shoutedLoudEnough = false;
        doneSomethingLoudEnough = false;
        if (detector != null)
            detector.SetActive(true);
        MicInput.typeOfInput = MicInput.MicInputType.peakDetection;
        //Dodać jakiś dźwięk / hałas, animacja zamieszania wśród studentów?
     //   GameController.Instance.SoundManager.Play3DAt(SamplesList.Murmurs, somewhere);
        loudStudents = new int[Random.Range(1, GameController.Instance.Students.Students.Length - 1)];
        for (int i = 0; i < loudStudents.Length; i++)
        {
            loudStudents[i] = Random.Range(0, GameController.Instance.Students.Students.Length - 1);
            GameController.Instance.Students.Students[loudStudents[i]].GetComponentInChildren<AnimationControll>().Talk(true);
        }
        MurmursManagement.MurmursSource.volume = 0.1f;
    }

    public override void CompleteEvent()
    {
        base.CompleteEvent();
        AddPoints(100);
        for (int i = 0; i < loudStudents.Length; i++)
        {
            GameController.Instance.Students.Students[loudStudents[i]].GetComponentInChildren<AnimationControll>().Talk(false);
        }
        MicInput.typeOfInput = MicInput.MicInputType.noone;
    }
    public override void AbortEvent()
    {
        base.AbortEvent();
        detector.SetActive(false);
        Debug.Log("End of noise.");
        for (int i = 0; i < loudStudents.Length; i++)
        {
            GameController.Instance.Students.Students[loudStudents[i]].GetComponentInChildren<AnimationControll>().Talk(false);
        }
        MicInput.typeOfInput = MicInput.MicInputType.noone;
        MurmursManagement.murmurs = false;
        MurmursManagement.MurmursSource.Stop();
        //Wycofać dodany dźwięk / hałas, animacje zamieszania wśród studentów
    }




    public override void CallInUpdate()
    {
        base.CallInUpdate();

        if (shoutedLoudEnough || doneSomethingLoudEnough)
        {
            GameController.Instance.Particles.CreateParticle(Particles.NaszeParticle.HundredPoints, detector.transform.GetChild(1).position);
            detector.SetActive(false);
            for (int i = 0; i < loudStudents.Length; i++)
            {
                GameController.Instance.SoundManager.Play3DAt(
                     SamplesList.Gasp,
                     GameController.Instance.Students.Students[loudStudents[i]].transform, 0.5f);
                GameController.Instance.Students.Students[loudStudents[i]].GetComponentInChildren<AnimationControll>().Clap();
            }
            MurmursManagement.murmurs = false;
            MurmursManagement.MurmursSource.Stop();

            CompleteEvent();
        }

    }    
}
