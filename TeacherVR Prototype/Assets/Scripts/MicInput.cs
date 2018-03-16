using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicInput : MonoBehaviour
{
    public GameObject SpeakingInd;


    bool _isInitialized;
    private string deviceName;
    public bool isSpeaking;

    float time;
    int current_time;
    int lastPointsTime;

    public int totalScore;
    int scoreBuffer;

    public float detectionLevel = 0.25f; 
    public float eps = 0.0000001f;

    void InitMic()
    {
        if (deviceName == null) deviceName = Microphone.devices[0];
        audioClipRec = Microphone.Start(deviceName, true, 999, 44100);
    }

    void StopMicrophone()
    {
        Microphone.End(deviceName);
    }


    public AudioClip audioClipRec = new AudioClip();
    int sampleWindow = 128;


    int DetectionOfSpeech()
    {

        float[] waveData = new float[sampleWindow];
        int micPosition = Microphone.GetPosition(null) - (sampleWindow + 1); 
        if (micPosition < 0) return 0;
        audioClipRec.GetData(waveData, micPosition);
        int currentSpeakingTime = 0;
        bool signal = false;

        for (int i = 0; i < sampleWindow; i++)
        {
            if(!signal)
            {

                
                if (waveData[i] > detectionLevel)
                {
               
                    isSpeaking = true;
                    currentSpeakingTime ++;
                    signal = true;
                }
            }
            else
            {
                if(i>1)
                {
                    if (Mathf.Abs(waveData[i - 1] - waveData[i]) > eps)
                    {
                        isSpeaking = true;
                        currentSpeakingTime++;
                    }
                    else
                    {
                        isSpeaking = false;
                        signal = false;

                    }
                }

            }
            SpeakingInd.SetActive(isSpeaking);
        }

        return currentSpeakingTime;
    }

    private void Start()
    {
        totalScore = 0;
        isSpeaking = false;
        SpeakingInd.SetActive(false);
    }

    void Update()
    {
        time += Time.deltaTime;
        current_time = (int)time;

        scoreBuffer = DetectionOfSpeech();
        if (scoreBuffer != 0)
        {
            lastPointsTime = current_time;
        }
        if (lastPointsTime + 5 <= current_time)
        {
            isSpeaking = false;
            SpeakingInd.SetActive(false);
        }
        totalScore += scoreBuffer;

    }


 
    void OnEnable()
    {
        InitMic();
        _isInitialized = true;
    }

    void OnDisable()
    {
        StopMicrophone();
    }

    void OnDestroy()
    {
        StopMicrophone();
    }

    void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            if (!_isInitialized)
            {
                InitMic();
                _isInitialized = true;
            }
        }
        if (!focus)
        {
            StopMicrophone();
            _isInitialized = false;

        }
    }
}
