using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* Main source atomtwist's comment from:
https://forum.unity.com/threads/check-current-microphone-input-volume.133501/ */
public class MicInput : MonoBehaviour
{
    public GameObject SpeakingInd;

    static public MicInputType typeOfInput;
    bool _isInitialized;
    private string deviceName;
    public bool isSpeaking;
    public enum MicInputType
    {
        noone,
        speechDetection,
        peakDetection
    }
    float time;
    int current_time;
    int lastPointsTime;

    public int totalScore;
    int scoreBuffer;

    public float minSilencingVolume = 1f;
    public float detectionLevel = 0.25f; 
    public float eps = 0.0000001f;

    void InitMic()
    {
        
        if (deviceName == null) deviceName = Microphone.devices[0];
        audioClipRec = Microphone.Start(deviceName, true, 128, 44100);
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
            if(SpeakingInd!=null)
            SpeakingInd.SetActive(isSpeaking);
        }

        return currentSpeakingTime;
    }
    float PeakDetection()
    {
        float levelMax = 0;
        float[] waveData = new float[sampleWindow];
        int micPosition = Microphone.GetPosition(null) - (sampleWindow + 1); // null means the first microphone
        if (micPosition < 0) return 0;
        audioClipRec.GetData(waveData, micPosition);
        // Getting a peak on the last 128 samples
        for (int i = 0; i < sampleWindow; i++)
        {
            float wavePeak = waveData[i];
            if (levelMax < wavePeak)
            {
                levelMax = wavePeak;
            }
        }
        Debug.Log(levelMax);
        return levelMax;
    }
    private void Start()
    {
        typeOfInput = MicInputType.noone;
        totalScore = 0;
        isSpeaking = false;
        if (SpeakingInd != null)
            SpeakingInd.SetActive(false);
    }

    void Update()
    {
        if (typeOfInput != MicInputType.noone)
        {
            time += Time.deltaTime;
            current_time = (int)time;
            if (typeOfInput == MicInputType.speechDetection)
            {
                scoreBuffer = DetectionOfSpeech();
                if (scoreBuffer != 0)
                {
                    lastPointsTime = current_time;
                }
                if (lastPointsTime + 3 <= current_time)
                {
                    isSpeaking = false;
                    if (SpeakingInd != null)
                        SpeakingInd.SetActive(false);
                }
                totalScore += scoreBuffer;
            }
            else if (typeOfInput == MicInputType.peakDetection)
            {
                if (PeakDetection() >= minSilencingVolume)
                    Noise.shoutedLoudEnough = true;
            }
        }
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

