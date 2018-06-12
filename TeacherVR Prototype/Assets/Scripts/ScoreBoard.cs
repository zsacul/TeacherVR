using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ScoreBoard : MonoBehaviour
{
    private TextMeshPro textMesh;

    private int[] TopScore = new int[5];
    private string[] TopNick = new string[5];

    private string nick = "BSTTE";
    private int points = 0;
    private float timer = 0f;
    public VRTK.Examples.UI_Keyboard keybo;

    private int minutes = 0;
    private int seconds = 0;

    private int MaxAnimSpeed = 100;
    private bool Anim = false;
    private int How_Many = 0;

    private bool CountDown = false;
    private bool OutOfTime = false;

    private bool Active = false;

    private bool Alarm = true;

    private float LastAnimTime = 0;
    private float AnimSoundDelay = 0.2f;

    GameObject ParticleSystem;
    GameObject Where;
    public enum WhatOver
    {
        Time,
        Events,
        TopPointsTime,
        TopPointsEvents
    }

    public delegate void GameOverDelegate(WhatOver over);

    public event GameOverDelegate GameOver;

    void Start()
    {
        ParticleSystem = GameObject.Find("ParticleSystem");
        Where = GameObject.Find("WhereAreParticles");
        textMesh = gameObject.GetComponent<TextMeshPro>();
        textMesh.text = "Tap here to start game!";
    }

    public void RestartBoard()
    {
        textMesh.text = "Tap here to start game!";
        PointsChange(0);
        ChangeTime(GameController.Instance.GameTime, 0);
        ChangeTimeCounting(true);
        SetOutOfTime(false);
        Alarm = true;
        nick = keybo.getnick();
    }

    private void Update()
    {
        if (Active)
        {
            if (CountDown && !OutOfTime)
            {
                timer -= Time.deltaTime;
                seconds = Convert.ToInt32(timer % 60);
                if (Alarm && minutes == 0 && seconds == 3)
                {
                    Alarm = false;
                    GameController.Instance.SoundManager.Play3DAt(SamplesList.AlarmClock, transform.position, 0.1f);
                }
                if (seconds == 0)
                {
                    seconds = 59;
                    timer = 59f;
                    minutes--;
                    if (minutes < 0)
                    {
                        OutOfTime = true;
                        if (GameOver != null)
                        {
                            if (SetNewTopScore()) GameOver(WhatOver.TopPointsTime);
                            else GameOver(WhatOver.Time);
                        }
                    }
                }
            }
            else if (!OutOfTime)
            {
                timer += Time.deltaTime;
                seconds = Convert.ToInt32(timer % 60);
                if (seconds == 60)
                {
                    seconds = 0;
                    timer = 0f;
                    minutes++;
                }
            }

            if (OutOfTime)
            {
                textMesh.text = "Score -  " + points + "\n\n" + "OUT OF TIME!";
            }
            else
            {
                if ((seconds >= 10) && (minutes >= 10))
                {
                    textMesh.text = "Score -  " + points + "\n\n" + "Time  -  " + minutes + ":" + seconds;
                }
                if ((seconds < 10) && (minutes >= 10))
                {
                    textMesh.text = "Score -  " + points + "\n\n" + "Time  -  " + minutes + ":0" + seconds;
                }
                if ((seconds >= 10) && (minutes < 10))
                {
                    textMesh.text = "Score -  " + points + "\n\n" + "Time  -  0" + minutes + ":" + seconds;
                }
                else
                {
                    textMesh.text = "Score -  " + points + "\n\n" + "Time  -  0" + minutes + ":0" + seconds;
                }
            }
            for (int speed = 0; speed <= MaxAnimSpeed * Time.deltaTime; speed++)
                if ((Anim) && (How_Many > 0))
                {
                    if (Time.time > LastAnimTime + AnimSoundDelay)
                    {
                        LastAnimTime = Time.time;
                        GameController.Instance.SoundManager.Play3DAt(SamplesList.CoinArcade, transform.position,
                            0.5f);
                    }
                    PointsAdd(1);
                    How_Many--;
                    if (How_Many == 0)
                    {
                        Anim = false;
                    }
                }
        }
    }

    private void FixedUpdate()
    {
        //KLAWISZE TYLKO DO TESTÓW!
        /*if (Input.GetKeyDown(KeyCode.P))
        {
            ParticleSystem.GetComponent<Particles>().CreateOnePoint(Where.transform.position, 0f);
            PointsAdd(1);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            ParticleSystem.GetComponent<Particles>().CreateParticle(Particles.NaszeParticle.HundredPoints, Where.transform.position);
            PointsAddAnim(100);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            ChangeTimeCounting();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            ChangeTime(1,59);
        }*/
    }

    // Dodawanie punktów bez animacji
    public void PointsAdd(int add)
    {
        points += add;
    }

    // Dodawanie punktów + "animacja"
    public void PointsAddAnim(int how)
    {
        How_Many += how;
        Anim = true;
    }

    // Dodawanie punktów za czas + "animacja"
    public void PointsAddForTime()
    {
        if (!OutOfTime)
        {
            How_Many += (minutes * 60 + seconds) * 10;
            minutes = 0;
            seconds = 0;
            timer = 0;
            Anim = true;
            OutOfTime = true;
            if (GameOver != null)
            {
                if (SetNewTopScore()) GameOver(WhatOver.TopPointsEvents);
                else GameOver(WhatOver.Events);
            }
        }
    }

    // Statyczna zmiana punktów
    public void PointsChange(int change)
    {
        points = change;
        //textMesh.text = "Score - " + points + "\n\n" + "Time  - " + minutes + ":" + seconds;
    }

    // Zwracanie ilości punktów
    public int GetPoints()
    {
        return points;
    }

    // Zmiana czasu
    public void ChangeTime(int new_minutes, int new_seconds)
    {
        minutes = new_minutes;
        seconds = new_seconds;
    }

    // Zmiana sposobu odliczania
    public void ChangeTimeCounting(bool _CountDown)
    {
        CountDown = _CountDown;
        /*if (CountDown)
        {
            CountDown = false;
        }
        else
        {
            //ChangeTime(10, 0);
            CountDown = true;
        }*/
    }

    // Zwracanie ilości punktów
    public void SetActive()
    {
        Active = true;
    }

    public bool IsOutOfTime()
    {
        return OutOfTime;
    }

    public void SetOutOfTime(bool val)
    {
        OutOfTime = val;
    }
    public string GetNick()
    {
        return nick;
    }

    public int[] GetTopScore()
    {
        return TopScore;
    }

    public string[] GetTopNick()
    {
        return TopNick;
    }

    public void SetTopScore(int[] _TopScore)
    {
        int i = 0;
        foreach (var score in _TopScore)
        {
            TopScore[i] = score;
            i++;
        }
    }

    public void SetTopNick(string[] _TopScore)
    {
        int i = 0;
        foreach (var score in _TopScore)
        {
            TopNick[i] = score;
            i++;
        }
    }

    public bool SetNewTopScore()
    {
        for (int i = 0; i < TopScore.Length; i++)
        {
            if (points + How_Many >= TopScore[i])
            {
                for (int j = TopScore.Length - 1; j > i; j--)
                {
                    TopNick[j] = TopNick[j - 1];
                    TopScore[j] = TopScore[j - 1];
                }
                TopNick[i] = nick;
                TopScore[i] = points + How_Many;
                return true;
            }
        }
        return false;
    }

    /*
     
    JAK SIĘ ODWOŁAC?
      
    private GameObject Target;                               // Jeśli obiekt ma pamiętać drugi obiekt
    Target = GameObject.Find("Score");                      // Ten obiekt (Score) trzyma teksty, więc go szukamy
    Target.GetComponent<ScoreBoard>().funkcja(argument);   // Tutaj odwołujemy się do skrypu (funkcji w tym skrypcie)

    */
}