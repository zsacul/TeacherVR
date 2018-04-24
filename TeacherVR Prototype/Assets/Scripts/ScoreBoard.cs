using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ScoreBoard : MonoBehaviour
{
    private TextMeshPro textMesh;

    private int points = 0;
    private float timer = 0f;

    private int minutes = 0;
    private int seconds = 0;

    private bool Anim = false;
    private int How_Many = 0;

    private bool CountDown = false;
    private bool OutOfTime = false;

    private bool Active = false;

    GameObject ParticleSystem;
    GameObject Where;

    public delegate void GameOverDelegate();

    public event GameOverDelegate GameOver;

    void Start ()
    {
        ParticleSystem = GameObject.Find("ParticleSystem");
        Where = GameObject.Find("WhereAreParticles");
        textMesh = gameObject.GetComponent<TextMeshPro>();
        textMesh.text = "Tap here to start game!";
    }

    private void Update()
    {
        if (Active)
        {
            if (CountDown)
            {
                timer -= Time.deltaTime;
                seconds = Convert.ToInt32(timer % 60);
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
                            GameOver();
                        }
                    }
                }
            }
            else
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

            if ((Anim) && (How_Many > 0))
            {
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
        /*/ /* KLAWISZE TYLKO DO TESTÓW!
        if (Input.GetKeyDown(KeyCode.P))
        {
            ParticleSystem.GetComponent<Particles>().CreateOnePoint(Where.transform.position, 0f);
            PointsAdd(1);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            ParticleSystem.GetComponent<Particles>().CreateParticle(Particles.NaszeParticle.HundredPoints, Where.transform.position);
            PointsAddAnim(10);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            ChangeTimeCounting();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            ChangeTime(1,59);
        }
        //*/
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

    /*
     
    JAK SIĘ ODWOŁAC?
      
    private GameObject Target;                               // Jeśli obiekt ma pamiętać drugi obiekt
    Target = GameObject.Find("Score");                      // Ten obiekt (Score) trzyma teksty, więc go szukamy
    Target.GetComponent<ScoreBoard>().funkcja(argument);   // Tutaj odwołujemy się do skrypu (funkcji w tym skrypcie)

    */

}
