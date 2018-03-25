using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Texts : MonoBehaviour
{

    public Text Score;
    private int points = 0;

    private bool Anim = false;
    private int How_Many = 0;

    void Start ()
    {
        Score.text = "Score : " + points;
    }

    private void Update()
    {
        if ((Anim)&&(How_Many>0))
        {
            PointsAdd(1);
            How_Many--;
            if (How_Many == 0)
            {
                Anim = false;
            }
        }
    }

    private void FixedUpdate()
    {
        // KLAWISZE TYLKO DO TESTOW
        if (Input.GetKeyDown(KeyCode.P))
        {
            PointsAdd(1);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            PointsAnim(10);
        }
    }

    public void PointsAdd(int add)
    {
        points += add;
        Score.text = "Score : " + points;
    }

    public void PointsAnim(int How)
    {
        // TEST
        GameObject Target = GameObject.Find("ParticleSystem");
        GameObject Where = GameObject.Find("Kwiatek");
        Target.GetComponent<Particles>().CreateParticle1(Where.transform.position, 0f);
        // TEST END
        How_Many += How;
        Anim = true;
    }

    public void PointsChange(int change)
    {
        points = change;
        Score.text = "Score : " + points;
    }

    public int GetPoints()
    {
        return points;
    }

    /* JAK SIĘ ODWOŁAC?
      
    private GameObject Target;                          // Jeśli obiekt ma pamiętać drugi obiekt
    Target = GameObject.Find("Canvas");                 // Ten obiekt (Canvas) trzyma teksty, więc go szukamy
    Target.GetComponent<Texts>().funkcja(argument);     // Tutaj odwołujemy się do skrypu (funkcji w nim)

    */
}
