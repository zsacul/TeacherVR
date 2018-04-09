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

public class AnimationControll : MonoBehaviour {

	private Animator anim;
	private Material[] materials;
	private Material eyeDefault;
	private IEnumerator coroutine;

    public Material eyeHit;
    public Material eyeDizzy;


	void Start () {
		anim = GetComponent<Animator>();
		materials = GetComponent<Renderer>().materials;
		eyeDefault = materials[2];
	}

	private IEnumerator func(string name) {
		anim.SetBool(name, true);
        yield return new WaitForSeconds(0.5f);
        anim.SetBool(name, false);
    }

	protected void Walk(bool state) {
		if (state) {
			anim.SetBool("isWalking", true);
		} else {
			anim.SetBool("isWalking", false);
		}
	}

	protected void Talk(bool state) {
		if (state) {
			anim.SetBool("isTalking", true);
		} else {
			anim.SetBool("isTalking", false);
		}
	}

	protected void HandThingy() {
		coroutine = func("HandThingy");
		StartCoroutine(coroutine);
	}

	protected void Clap() {
		coroutine = func("Clap");
		StartCoroutine(coroutine);
	}

	protected void Hit() {
		coroutine = func("Hit");
		materials[3] = eyeHit;
		StartCoroutine(coroutine);
		materials[3] = eyeDefault;		
	}
	
	protected void Wave() {
		anim.SetTrigger("Wave");
	}

	protected void Give() {
		anim.SetTrigger("Give");
	}

	
}
