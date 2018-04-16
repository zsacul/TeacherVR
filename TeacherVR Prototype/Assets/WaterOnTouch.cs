using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class WaterOnTouch : MonoBehaviour
{
    public GameObject WaterSource;

    private Animator anim;
    private float lastTime;
    private float durration;

    void Start()
    {
        anim = GetComponent<Animator>();
        WaterSource.SetActive(anim.GetBool("On"));
        lastTime = Time.time;
        durration = GameController.Instance.SoundManager.SfxWaterRunning.length-0.1f;
    }

    void Update()
    {
        if (WaterSource.activeSelf && Time.time > lastTime + durration)
        {
            lastTime = Time.time;
            GameController.Instance.SoundManager.Play3DAt(SamplesList.WaterRunning, WaterSource.transform.position);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag.Equals("LController") || col.tag.Equals("RController"))
        {
            anim.SetBool("On", !anim.GetBool("On"));
            WaterSource.SetActive(anim.GetBool("On"));
        }
    }
}