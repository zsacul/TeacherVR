using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Point_Anim_Control : MonoBehaviour
{
    public ActivateStudents act;

    private IEnumerator coroutine;
    private Animator anim;
    
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private IEnumerator anim_finished() {
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length+anim.GetCurrentAnimatorStateInfo(0).normalizedTime);
        gameObject.SetActive(false);
    }
    
    public void Kill()
    {
        act.Killed();
        anim.SetBool("Alive", false);
        coroutine = anim_finished();
        StartCoroutine(coroutine);
    }
}