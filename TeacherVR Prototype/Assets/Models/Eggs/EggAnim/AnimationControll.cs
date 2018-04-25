using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//po tym skrypcie dziedziczymy funkcje do animacji :3
//trzeba go sobie podpiąć do jajka
//w tutorialu rzucania powinien byc jakiś przykład 

//nie dodałem jeszcze wszystkiego, gdyż czekam na grafiki, ale z grubsza już działa
//normalnie chodzenie i mówienie odpala się true i kończy false
//mowienie nakłada się bezproblemowo z resztą animacji
//generalnie wszystkie ładnie powinny działać razem

public class AnimationControll : MonoBehaviour
{
    private Animator anim;
    private Material[] materials;
    private Texture eyeDefault;
    private IEnumerator coroutine;
    private bool coroutineInProgress = false;

    public Material eyeHit;
    public Material eyeDizzy;
    public Material mouthAwkward;


    void Start()
    {
        anim = GetComponent<Animator>();
        materials = GetComponent<Renderer>().materials;
        eyeDefault = materials[2].mainTexture;
    }


    private IEnumerator expression_hit(string name)
    {
        if (!coroutineInProgress)
        {
            coroutineInProgress = true;
            anim.SetTrigger(name);
            materials[2].mainTexture = eyeHit.mainTexture;
            yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(3).length);
            materials[2].mainTexture = eyeDefault;
            coroutineInProgress = false;
        }
    }

    private IEnumerator expression_fear(string name)
    {
        anim.SetBool(name, true);
        materials[2] = eyeHit;
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length +
                                        anim.GetCurrentAnimatorStateInfo(0).normalizedTime);
        yield return new WaitForSeconds(0.1f);
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length +
                                        anim.GetCurrentAnimatorStateInfo(0).normalizedTime);
    }

    public void Walk(bool state)
    {
        if (state)
        {
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }
    }

    public void Talk(bool state)
    {
        if (state)
        {
            anim.SetBool("isTalking", true);
        }
        else
        {
            anim.SetBool("isTalking", false);
        }
    }

    public void HandThingy()
    {
        anim.SetTrigger("HandThingy");
    }

    public void Clap()
    {
        anim.SetTrigger("Clap");
    }

    public void Shake(bool state)
    {
        if (state)
        {
            coroutine = expression_fear("Fear");
            StartCoroutine(coroutine);
        }
        else
        {
            anim.SetBool("Fear", false);
            materials[2].mainTexture = eyeDefault;
        }
    }

    public void Hit()
    {
        anim.SetBool("Fear", false);
        coroutine = expression_hit("Hit");
        StartCoroutine(coroutine);	
    }

    public void Wave()
    {
        anim.SetTrigger("Wave");
    }

    public void Give()
    {
        anim.SetTrigger("Give");
    }
}