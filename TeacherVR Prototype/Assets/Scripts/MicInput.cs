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

    static public bool bookNoise = false;
    private bool initDet = false;
    private TextMeshProUGUI tm;
    private LineRenderer lrCurr;
    private LineRenderer lrS;
    public GameObject DetectorPrefab;
    private GameObject Detector;
   // private TextMeshPro tmPro;
    private float[] peakArray;
    private int pointer;
    private float sampleTime;
  //  private 
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
   //     tm.enabled = b;
    }
    void InitMic()
    {
        
        if (deviceName == null) deviceName = Microphone.devices[0];
      //  Debug.Log("devicename " + deviceName);
        audioClipRec = Microphone.Start(deviceName, true, 128, 44100);
      //  Debug.Log(Microphone.IsRecording(deviceName));
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
        _legacyScore = 0;

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

            //   tm = Detector.transform.Find("Canvas").gameObject.GetComponentInChildren<TextMeshProUGUI>();
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
                if (!initDet)
                    InitDetect();
             //   Detector.SetActive(true);
                if (peakArrayDisplayTime + 1f <= time)
                {
                    DisplayLoudness();
                    peakArrayDisplayTime = time;
                }
                float MaxPeak = PeakDetection();
                if (sampleTime + 0.1f <= time)
                {
                   // Debug.Log(MaxPeak);
                    if (MaxPeak >= detectionLevel)
                    {
                        peakArray[pointer] = MaxPeak;
                        pointer++;
                        pointer %= 9;
                        sampleTime = time;
                   }
                   /* else //???? average glob
                    {
                        int buff = pointer-1;
                        if (buff == -1)
                            buff = 9;
                        if (peakArray[buff] != -1)
                        {
                            peakArray[pointer] = (peakArray[buff] + average) / 3;
                            pointer++;
                            pointer %= 9;
                            sampleTime = time;
                        }
                    }*/
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
          //          text += peakArray[(pointer + i) % 10].ToString() + "\n";
                }
              //  else
          //      {
          //          lrCurr.SetPosition(i, new Vector3(i / 10f, average / minSilencingVolume, 0));
         //           text += "usr: "+ average + "\n";
         //       }
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
        //    text += "Srednia: "+average+"\n";
            if (numb == 0)
                minSilencingVolume = 1f;
            else if (numb < 5)
            {
                minSilencingVolume = average * 10 + 0.1f;
            }
            else
                minSilencingVolume = average * 5 + 0.1f;
            if (minSilencingVolume > 2f)
                minSilencingVolume = 2f;
            else if (minSilencingVolume < 0.35f)
                minSilencingVolume = 0.35f;
            lrS.positionCount = 2;
            lrS.SetPositions(new Vector3[2] { new Vector3(0, 1, 0), new Vector3(9 / 10f, 1, 0) }); // 1 -> 100%
         //   text+="To silence: " + minSilencingVolume;
           // tm.text = text;

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

