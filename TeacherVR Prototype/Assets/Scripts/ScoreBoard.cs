using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreBoard : MonoBehaviour
{

    private TextMeshPro textMesh;

    private int points = 0;
    private bool Anim = false;
    private int How_Many = 0;

    void Start ()
    {
        textMesh = gameObject.GetComponent<TextMeshPro>();
        textMesh.text = "Score : " + points;
    }


    private void Update()
    {
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
        textMesh.text = "Score : " + points;
    }

    public void PointsAnim(int How)
    {
        How_Many += How;
        Anim = true;
    }

    public void PointsChange(int change)
    {
        points = change;
        textMesh.text = "Score : " + points;
    }

    public int GetPoints()
    {
        return points;
    }

    /* JAK SIĘ ODWOŁAC?
      
    private GameObject Target;                          // Jeśli obiekt ma pamiętać drugi obiekt
    Target = GameObject.Find("Score");                 // Ten obiekt (Score) trzyma teksty, więc go szukamy
    Target.GetComponent<ScoreBoard>().funkcja(argument);     // Tutaj odwołujemy się do skrypu (funkcji w nim)

    */

}
