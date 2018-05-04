using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipsForButtonsController : MonoBehaviour
{
    public enum Button
    {
        Touchpad,
        SteamMenu,
        Button2,
        Grip,
        Trigger
    }

    public Button ButtonTip;

    private GameObject child;

    void OnEnable()
    {
        child = transform.GetChild(0).gameObject;
    }

    void Update()
    {
        if (GameController.Instance.Tooltips)
        {
            child.SetActive(true);
            GetComponentInChildren<Animator>().SetFloat("Blend", (float) ButtonTip * 0.21f);
        }
        else child.SetActive(false);
    }
}