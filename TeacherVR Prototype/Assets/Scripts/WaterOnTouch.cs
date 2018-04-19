using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class WaterOnTouch : MonoBehaviour
{
    public GameObject WaterSource;
    public float WaterDurration = 5;

    private Animator anim;
    private float lastTime;
    private float soundDurration;

    void Start()
    {
        anim = GetComponent<Animator>();
        WaterSource.SetActive(anim.GetBool("On"));
        lastTime = Time.time;
        soundDurration = GameController.Instance.SoundManager.SfxWaterRunning.length-0.1f;
    }

    void Update()
    {
        if (WaterSource.activeSelf && Time.time > lastTime + soundDurration)
        {
            lastTime = Time.time;
            GameController.Instance.SoundManager.Play3DAt(SamplesList.WaterRunning, WaterSource.transform.position,0.05f);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag.Equals("LController") || col.tag.Equals("RController"))
        {
            StopAllCoroutines();
            ChangeState();
            StartCoroutine(TurnOff());
        }
    }

    private void ChangeState()
    {
        anim.SetBool("On", !anim.GetBool("On"));
        WaterSource.SetActive(anim.GetBool("On"));
    }

    IEnumerator TurnOff()
    {
        yield return new WaitForSeconds(WaterDurration);
        anim.SetBool("On", false);
        WaterSource.SetActive(false);
    }
}