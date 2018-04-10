using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chalk_Hit : MonoBehaviour
{
    private Renderer rend;
    private int hits = 0;
    private AnimationControll script;

    public Tutorial_Point_Anim_Control tutorial_point_user;

    void Start()
    {
        rend = transform.GetComponent<Renderer>();
        script = gameObject.GetComponent<AnimationControll>();
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Chalk"))
        {
            if (hits == 0)
            {
                //rend.material.color = Color.red;
                script.Hit();
                hits++;
            }
            else if (hits == 1)
            {
                script.Hit();
                tutorial_point_user.Kill();
                hits++;
            }
        }
    }
}