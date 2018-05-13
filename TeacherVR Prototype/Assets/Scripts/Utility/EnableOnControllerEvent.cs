using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class EnableOnControllerEvent : MonoBehaviour
{
    private GameObject RightHand;
    private GameObject LeftHand;

    void Start()
    {
        StartCoroutine(FindDevices());
    }

    private IEnumerator FindDevices()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        RightHand = VRTK_DeviceFinder.GetControllerRightHand();
        LeftHand = VRTK_DeviceFinder.GetControllerLeftHand();

        RightHand.GetComponent<VRTK_ControllerEvents>().TouchpadPressed += TouchpadPressed;
        LeftHand.GetComponent<VRTK_ControllerEvents>().TouchpadPressed += TouchpadPressed;
        RightHand.GetComponent<VRTK_ControllerEvents>().TouchpadReleased += TouchpadReleased;
        LeftHand.GetComponent<VRTK_ControllerEvents>().TouchpadReleased += TouchpadReleased;

        gameObject.SetActive(false);
    }

    /*void OnDestroy()
    {
        RightHand.GetComponent<VRTK_ControllerEvents>().TouchpadPressed -= RightTouchpadPressed;
        LeftHand.GetComponent<VRTK_ControllerEvents>().TouchpadPressed -= LeftTouchpadPressed;
        RightHand.GetComponent<VRTK_ControllerEvents>().TouchpadTouchEnd -= RightTouchpadReleased;
        LeftHand.GetComponent<VRTK_ControllerEvents>().TouchpadTouchEnd -= LeftTouchpadReleased;
    }*/
    /*
    private void OnDestroy()
    {
        RightHand.GetComponent<VRTK_ControllerEvents>().TouchpadPressed -= RightTouchpadPressed;
        LeftHand.GetComponent<VRTK_ControllerEvents>().TouchpadPressed -= LeftTouchpadPressed;
        RightHand.GetComponent<VRTK_ControllerEvents>().TouchpadTouchEnd -= RightTouchpadReleased;
        LeftHand.GetComponent<VRTK_ControllerEvents>().TouchpadTouchEnd -= LeftTouchpadReleased;
    }*/

    private void TouchpadPressed(object sender, ControllerInteractionEventArgs e)
    {
        gameObject.SetActive(true);
    }

    private void TouchpadReleased(object sender, ControllerInteractionEventArgs e)
    {
        gameObject.SetActive(false);
    }
}