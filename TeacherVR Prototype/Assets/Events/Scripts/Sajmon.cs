using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sajmon", menuName = "Events/Sajmon Event")]
public class Sajmon : Events
{
    private GameObject PC;

    public GameObject ButtonsPrefab;
    private GameObject Buttons;

    private Renderer MonitorRenderer;

   // public static AudioSource aSource;

    public int sequence;
    private int buffor;
    public static int PlayerSequence = 0;

    private Color color;

    float _time;
    public float Start_time = 5f; //czas, kiedy ma pokazywać po starcie
    public float deltaTime = 2f; //różnice czasu w pokazywaniu kolejnych kroków sekwencji

    public static bool needToCheck = false;

    int played; // -1 czeka na interakcję, 0 not shown, 1 showing, 2 shown

    float progress;

    public override void StartEvent()
    {
        base.StartEvent();

        PC = GameObject.FindGameObjectWithTag("PCEvent");
        Message(10, description, MessageSystem.ObjectToFollow.Headset, MessageSystem.Window.W800H400);
        MonitorRenderer = PC.transform.Find("Monitor").gameObject.GetComponent<MeshRenderer>();
        color = MonitorRenderer.material.color;
        MonitorRenderer.material.color = Color.red;
        Buttons = Instantiate(ButtonsPrefab);
       // aSource = Buttons.transform.GetChild(Buttons.transform.childCount - 1).GetComponent<AudioSource>();
        Buttons.SetActive(true);
        GenerateSequence(Lvl);
        buffor = sequence;
        _time = Time.time;
        played = -1;
        progress = 0;
    }

    public override void CallInUpdate()
    {
        //base.CallInUpdate();

        if ((played == 0 && _time + Start_time <= Time.time) || (played == 1 && _time + deltaTime <= Time.time))
        {
            if (played == 0)
            {
                played = 1;
                canPlayerIteract();
                _time = Time.time;
            }
            else if (played == 1)
            {
                if (buffor >= 1)
                {
                    ShowPressing(buffor % 10);
                    buffor /= 10;
                    _time = Time.time;
                }
                else
                {
                    played = 2;
                    //  PlayerSequence = 0;
                    canPlayerIteract(true);
                    _time = Time.time;
                }
            }
        }
        if (needToCheck)
        {
            if (played == -1)
            {
                PlayerSequence = 0;
                played = 0;
                ButtonTouch.resetIndex();
            }
            else
            {
                Debug.Log(PlayerSequence);
                if (PlayerSequence - sequence == 0)
                {
                    AddPoints(Lvl * 10);
                    CompleteEvent();
                }
                else
                {
                    string seq = sequence.ToString();
                    string pseq = PlayerSequence.ToString();
                    if (!(seq[seq.Length - pseq.Length].Equals(pseq[0])))
                    {
                        Debug.Log("You've failed");
                        //tutaj wywołaj particle wrong
                        GameController.Instance.Particles.CreateOnePoint(Buttons.transform.position,0);
                        PlayerSequence = 0;
                        ButtonTouch.resetIndex();
                        played = 0;
                        buffor = sequence;
                        progress = 0;

                    }
                    else
                    {
                        //tutaj wywołaj particle good 
                        GameController.Instance.Particles.CreateOnePoint(Buttons.transform.position, 0);
                        progress += 100 / seq.Length;
                        SetProgressBar(progress);
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
        Destroy(Buttons);
    }

    public override void CompleteEvent()
    {
        base.CompleteEvent();
        MonitorRenderer.material.color = color;
        Destroy(Buttons);
    }

    void GenerateSequence(int difficulty)
    {
        int b = Random.Range(1, 4);
        for (int i = 0; i < difficulty; i++)
        {
            b = b * 10 + Random.Range(1, 5);
        }
        sequence = b;
        Debug.Log(sequence + "<- od końca.");
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
        Buttons.transform.GetChild(id - 1).GetComponent<ButtonTouch>().PushButton();
    }
}