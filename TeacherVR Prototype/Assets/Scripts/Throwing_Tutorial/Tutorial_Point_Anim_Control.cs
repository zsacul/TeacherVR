using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Point_Anim_Control : MonoBehaviour
{
    public ActivateStudents act;
    
    private Animator anim;
    
    void OnEnable()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("Alive", true);
    }

    private IEnumerator anim_finished() {
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        gameObject.SetActive(false);
    }

    public void Resurrect()
    {
        gameObject.SetActive(true);
    }

    public void Abort()
    {
        anim.SetBool("Alive", false);
        gameObject.SetActive(false);
    }
    
    public void Kill()
    {
        act.Killed();
        anim.SetBool("Alive", false);
        StartCoroutine(anim_finished());
    }
}