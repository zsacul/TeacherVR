using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Point_Anim_Control : MonoBehaviour
{
    public Target_Control tc;

    private Animator anim;
    
    void Start()
    {
        anim = GetComponent<Animator>();
    }


    public void Kill()
    {
        tc.Killed();
        anim.SetBool("Alive", false);
        Destroy(gameObject, anim.GetCurrentAnimatorStateInfo(0).length + 2.0F);
    }
}