using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class WaterOnTouch : MonoBehaviour
{
    public GameObject WaterSourceModel;
    public float WaterDurration = 7;

    private Animator anim;
    private float lastTime;
    private float soundDurration;

    void Start()
    {
        anim = GetComponent<Animator>();
        WaterSourceModel.SetActive(anim.GetBool("On"));
        lastTime = Time.time;
        soundDurration = GameController.Instance.SoundManager.SfxWaterRunning.length - 0.1f;
    }

    void Update()
    {
        if (WaterSourceModel.activeSelf && Time.time > lastTime + soundDurration)
        {
            lastTime = Time.time;
            GameController.Instance.SoundManager.Play3DAt(SamplesList.WaterRunning, WaterSourceModel.transform.position,
                0.05f);
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
        WaterSourceModel.SetActive(anim.GetBool("On"));
        GameController.Instance.SoundManager.Play3DAt(
            anim.GetBool("On") ? SamplesList.FaucetOpen : SamplesList.FaucetClose, transform.position, 0.1f);
    }

    IEnumerator TurnOff()
    {
        yield return new WaitForSeconds(WaterDurration);
        GameController.Instance.SoundManager.Play3DAt(SamplesList.FaucetClose, transform.position, 0.1f);
        anim.SetBool("On", false);
        WaterSourceModel.SetActive(false);
    }
}