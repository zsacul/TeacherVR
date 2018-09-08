using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//using UnityEngine.Windows.Speech;
//using System.Linq;
/*  Source atomtwist's comment from:
https://forum.unity.com/threads/check-current-microphone-input-volume.133501/ */
public class MicInput : MonoBehaviour
{

    public GameObject SpeakingInd;

    static public MicInputType typeOfInput;
    bool _isInitialized;
    private string deviceName;
    public bool isSpeaking;


    static public bool bookNoise = false;
    public int multiplier = 5;
    private bool initDet = false;
    private TextMeshProUGUI tm;
    private LineRenderer lrCurr;
    private LineRenderer lrS;
    public GameObject DetectorPrefab;
    private GameObject Detector;

    private float[] peakArray;
    private int pointer;
    private float sampleTime;
  
    private float peakArrayDisplayTime;


    public enum MicInputType
    {
        noone,
        speechDetection,
        peakDetection
    }
    float time;
    int current_time;
    int lastSpeakingPointsTime;


    int speakingScore;

    public float minSilencingForce = 6f;
    public float minSilencingVolume = 1f;
    public float detectionLevel = 0.25f; 
    public float eps = 0.0000001f;

    void InitMic()
    {
        
        if (deviceName == null && Microphone.devices.Length >0) deviceName = Microphone.devices[0];
  
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
        return levelMax;
    }
    private void Start()
    {


        bookNoise = false;
        typeOfInput = MicInputType.noone;
 

        isSpeaking = false;
        if (SpeakingInd != null)
            SpeakingInd.SetActive(false);

        
    }

    void InitDetect()
    {
        pointer = 0;
        if (Detector == null)
        {
            Detector = Instantiate(DetectorPrefab);
            Noise.detector = Detector;


            lrCurr = Detector.transform.Find("Canvas/Line").gameObject.GetComponent<LineRenderer>();
            lrCurr.positionCount = 0;
            lrS = Detector.transform.Find("Canvas/ShoutLine").gameObject.GetComponent<LineRenderer>();
            lrS.positionCount = 0;
        }
        else
            Detector.SetActive(true);
        peakArray = new float[10] { -1f, -1f, -1f, -1f, -1f, -1f, -1f, -1f, -1f, -1f };
        initDet = true;
    }
    void Update()
    {
        

        if (typeOfInput != MicInputType.noone)
        {
            if (!Microphone.IsRecording(deviceName))
                OnEnable();
                time += Time.deltaTime;
            current_time = (int)time;
            if (typeOfInput == MicInputType.speechDetection)
            {

                
                speakingScore = DetectionOfSpeech();
                if (speakingScore != 0)
                {
                    lastSpeakingPointsTime = current_time;
                }
                if (lastSpeakingPointsTime + 1 <= current_time)
                {
                    isSpeaking = false;
                    if (SpeakingInd != null)
                        SpeakingInd.SetActive(false);
                }

            }
            else if (typeOfInput == MicInputType.peakDetection)
            {
                if (!initDet)
                    InitDetect();

                if (peakArrayDisplayTime + 1f <= time)
                {
                    DisplayLoudness();
                    peakArrayDisplayTime = time;
                }
                float MaxPeak = PeakDetection();
                if (sampleTime + 0.1f <= time)
                {

                    if (MaxPeak >= detectionLevel)
                    {
                        peakArray[pointer] = MaxPeak;
                        pointer++;
                        pointer %= 9;
                        sampleTime = time;
                   }

                }
                if (MaxPeak >= minSilencingVolume || bookNoise)
                {
                    Noise.shoutedLoudEnough = true;
                    typeOfInput = MicInputType.noone;
                    Detector.SetActive(false);
                    initDet = false;
                }
                if (bookNoise)
                    initDet = false;
            }
        }

    }


    public void DisplayLoudness()
    {
        if (typeOfInput == MicInputType.peakDetection)
        {
            
          //  string text = "";
            lrCurr.positionCount=10;
            for (int i = 0; i < 10; i++)
            {
                if (peakArray[(pointer + i) % 10] != -1f)
                {
                    lrCurr.SetPosition(i, new Vector3(i/10f, (peakArray[(pointer + i) % 10])/minSilencingVolume, 0));

                }

            }
            int numb = 10;
            float average = 0;
            for (int i=0; i <10; i++)
            {
                if (peakArray[i] == -1f)
                    numb--;
                else 
                    average += peakArray[i];
            }
            if (numb != 0)
                average /= numb;
            else
                average = 0;

            if (numb == 0)
                minSilencingVolume = 1f;
            else if (numb < 5)
            {
                minSilencingVolume = average * 10 + 0.1f;
            }
            else
                minSilencingVolume = average * multiplier + 0.1f;
            if (minSilencingVolume > 2f)
                minSilencingVolume = 2f;
            else if (minSilencingVolume < 0.35f)
                minSilencingVolume = 0.35f;
            lrS.positionCount = 2;
            lrS.SetPositions(new Vector3[2] { new Vector3(0, 1, 0), new Vector3(9 / 10f, 1, 0) }); // 1 -> 100%


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

