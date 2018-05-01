using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class ChangeStateOnButtonPress : MonoBehaviour
{
    public GameObject HandScript;

    void Start()
    {
        HandScript.GetComponent<VRTK_UIPointer>().ActivationButtonPressed +=
            ChangeStateOnButtonPress_ActivationButtonPressed;
        gameObject.SetActive(false);
    }

    private void ChangeStateOnButtonPress_ActivationButtonPressed(object sender, ControllerInteractionEventArgs e)
    {
        gameObject.SetActive(HandScript.GetComponent<VRTK_UIPointer>().PointerActive());
    }
}