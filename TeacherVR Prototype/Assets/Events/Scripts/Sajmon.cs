using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[CreateAssetMenu(fileName = "Sajmon", menuName = "Events/Sajmon Event")]
public class Sajmon : Events
{
    private GameObject PC;

    public GameObject ButtonsPrefab;
    private GameObject Buttons;

    private Renderer MonitorRenderer;
    private TextMeshProUGUI TextMeshProMonitor;

   // public static AudioSource aSource;

    public string sequen;
    private int seqBufforPtr;
    public static string seqPlayerSequence = "";

    private Color color;

    float _time;
    public float Start_time = 5f; //czas, kiedy ma pokazywać po starcie
    public float deltaTime = 2f; //różnice czasu w pokazywaniu kolejnych kroków sekwencji

    public static bool needToCheck = false;

    SeqStatus stat;
    enum SeqStatus
    {
        waitingForInteraction,
        NotShown,
        Showing,
        Shown
    }

    float progress;

    public override void StartEvent()
    {
        base.StartEvent();

        PC = GameObject.FindGameObjectWithTag("PCEvent");
        Message(10, description, MessageSystem.ObjectToFollow.Headset, MessageSystem.Window.W800H400);
        MonitorRenderer = PC.transform.Find("Monitor").gameObject.GetComponent<MeshRenderer>();
        TextMeshProMonitor = PC.transform.Find("Monitor/Canvas").gameObject.GetComponentInChildren<TextMeshProUGUI>();
        color = MonitorRenderer.material.color;
        MonitorRenderer.material.color = Color.red;
        Buttons = Instantiate(ButtonsPrefab);
       // aSource = Buttons.transform.GetChild(Buttons.transform.childCount - 1).GetComponent<AudioSource>();
        Buttons.SetActive(true);
        GenerateSequence(Lvl);
        _time = Time.time;
        seqBufforPtr = 0;
        stat = SeqStatus.waitingForInteraction;
        progress = 0;
        TextMeshProMonitor.text = "Press any key";
    }

    public override void CallInUpdate()
    {
        //base.CallInUpdate();

        if (( stat == SeqStatus.NotShown && _time + Start_time <= Time.time) || ( stat == SeqStatus.Showing && _time + deltaTime <= Time.time))
        {
            if (stat == SeqStatus.NotShown)
            {
                stat = SeqStatus.Showing;
                canPlayerIteract();
                _time = Time.time;
            }
            else if (stat == SeqStatus.Showing)
            {
                if (seqBufforPtr < sequen.Length)
                {
                    TextMeshProMonitor.text = "Watch";
                    ShowPressing(sequen[seqBufforPtr]);
                    //   buffor /= 10;
                    seqBufforPtr++;
                    _time = Time.time;
                }
                else
                {
                    stat = SeqStatus.Shown;
                    canPlayerIteract(true);
                    TextMeshProMonitor.text = "Repeat";
                    _time = Time.time;
                }
            }
        }
        if (needToCheck)
        {
            if (stat == SeqStatus.waitingForInteraction)
            {
                seqPlayerSequence = "";
                stat = SeqStatus.NotShown;
                seqBufforPtr = 0;
            }
            else
            {
                if (seqPlayerSequence.Equals(sequen))
                {
                    GameController.Instance.Particles.CreateParticle(Particles.NaszeParticle.ThreeHundredPoints, new Vector3(Buttons.transform.position.x, Buttons.transform.position.y + 0.5f, Buttons.transform.position.z));
                    AddPoints(300);
                    CompleteEvent();
                }
                else
                {
                    if (!(sequen[seqPlayerSequence.Length-1].Equals(seqPlayerSequence[seqPlayerSequence.Length-1])))
                    {
                        TextMeshProMonitor.text = "Wait";
                        GameController.Instance.Particles.CreateParticle(Particles.NaszeParticle.Small_Wrong,new Vector3(Buttons.transform.position.x, Buttons.transform.position.y+0.5f, Buttons.transform.position.z));
                        seqPlayerSequence = "";
                        stat = SeqStatus.NotShown;
                        canPlayerIteract();
                        seqBufforPtr = 0;
                        progress = 0;

                    }
                    else
                    {
                        GameController.Instance.Particles.CreateParticle(Particles.NaszeParticle.Small_Good_Correct_Ok, new Vector3(Buttons.transform.position.x, Buttons.transform.position.y+0.5f, Buttons.transform.position.z));
                        progress += 100 / sequen.Length;

                    }
                }
            }
            needToCheck = false;
        }
    }

    public override void AbortEvent()
    {
        base.AbortEvent();
        MonitorRenderer.material.color = color;
        TextMeshProMonitor.text = "";
        Destroy(Buttons);
    }

    public override void CompleteEvent()
    {
        base.CompleteEvent();
        MonitorRenderer.material.color = color;
        TextMeshProMonitor.text = "";
        Destroy(Buttons);
    }
    

    void GenerateSequence(int difficulty)
    {
        int b = Random.Range(1, 4);
        string seq = b.ToString();
        for (int i = 0; i < difficulty; i++)
        {
            seq += Random.Range(1, 5).ToString();
        }

        sequen = seq;
        Debug.Log(sequen);
    }

    void canPlayerIteract(bool can = false)
    {
        foreach (Transform child in Buttons.transform)
        {
            if (child.GetComponent<ButtonTouch>() != null)
                child.GetComponent<ButtonTouch>().showing = !can;
        }
    }

    private void ShowPressing(int id)
    {
        //Hidden conversion string -> int
        Buttons.transform.GetChild(id%49).GetComponent<ButtonTouch>().PushButton();
    }
}