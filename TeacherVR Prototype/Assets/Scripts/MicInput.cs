using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
/* Main source atomtwist's comment from:
https://forum.unity.com/threads/check-current-microphone-input-volume.133501/ */
public class MicInput : MonoBehaviour
{
    public GameObject SpeakingInd;

    static public MicInputType typeOfInput;
    bool _isInitialized;
    private string deviceName;
    public bool isSpeaking;
    
    private TextMeshProUGUI tm;
    public GameObject DetectorPrefab;
    private GameObject Detector;
   // private TextMeshPro tmPro;
    private float[] peakArray;
    private int pointer;
    private float sampleTime;
    private float peakArrayDisplayTime;
    //public float minRequiredVolume;

    public enum MicInputType
    {
        noone,
        speechDetection,
        peakDetection
    }
    float time;
    int current_time;
    int lastPointsTime;

    public int _legacyScore;
    int scoreBuffer;

    public float minSilencingForce = 6f;
    public float minSilencingVolume = 1f;
    public float detectionLevel = 0.25f; 
    public float eps = 0.0000001f;
    public void enableDetector(bool b = true)
    {
        tm.enabled = b;
    }
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
  //      Debug.Log(levelMax);
        return levelMax;
    }
    private void Start()
    {
        typeOfInput = MicInputType.noone;
        _legacyScore = 0;
        pointer = 0;
        isSpeaking = false;
        if (SpeakingInd != null)
            SpeakingInd.SetActive(false);
        Detector = Instantiate(DetectorPrefab);
        tm = Detector.transform.Find("Canvas").gameObject.GetComponentInChildren<TextMeshProUGUI>();
        //        Debug.Log("null");
        peakArray = new float[10] { -1f, -1f, -1f, -1f, -1f, -1f, -1f, -1f,-1f, -1f};
        
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
                _legacyScore += scoreBuffer;
            }
            else if (typeOfInput == MicInputType.peakDetection)
            {
                if (Detector == null)
                    Detector = Instantiate(DetectorPrefab);
                Detector.SetActive(true);
                if (peakArrayDisplayTime + 1f <= time)
                {
                    DisplayLoudness();
                    peakArrayDisplayTime = time;
                }
                float MaxPeak = PeakDetection();
                if (sampleTime + 0.5f <= time)
                {
                    if (MaxPeak >= detectionLevel)
                    {
                        peakArray[pointer] = MaxPeak;
                        pointer++;
                        pointer %= 9;
                        sampleTime = time;
                    }
                }
                if (MaxPeak >= minSilencingVolume)
                {
                    Noise.shoutedLoudEnough = true;
                    typeOfInput = MicInputType.noone;
                    Destroy(Detector);
                }
            }
        }
    }

    public void DisplayLoudness()
    {
        if (typeOfInput == MicInputType.peakDetection)
        {
            string text = "";
            for (int i = 0; i < 10; i++)
            {
                 if(peakArray[(pointer+i)%10]!=-1f)
                text += peakArray[(pointer+i)%10].ToString() + "\n";
            }
            int numb = 10;
            float sum = 0;
            for (int i=0; i <10; i++)
            {
                if (peakArray[i] == -1f)
                    numb--;
                else 
                    sum += peakArray[i];
            }
            if (numb != 0)
                sum /= numb;
            else
                sum = 0;
            text += "Srednia: "+sum+"\n";
            if (numb == 0)
                minSilencingVolume = 1f;
                else
                minSilencingVolume = sum * 3 + 0.1f;
            text+="To silence: " + minSilencingVolume;
            tm.text = text;

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

