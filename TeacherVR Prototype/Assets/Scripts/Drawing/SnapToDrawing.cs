using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
public class SnapToDrawing : MonoBehaviour {

    bool grabbed;
    bool nearBoard;
    public float min_x;
    public float max_x = 100f;
    // Use this for initialization
    void Start () {
        GetComponent<VRTK_InteractableObject>().InteractableObjectGrabbed +=
           ChangeCenterOfMassOnThrow_InteractableObjectGrabbed;
        GetComponent<VRTK_InteractableObject>().InteractableObjectUngrabbed +=
    ChangeCenterOfMassOnThrow_InteractableObjectUngrabbed;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "HitBoxBoard")
        {
        //    Debug.Log(other.transform.parent.parent.name);
            switch (other.transform.parent.parent.name[0])
            {
                case '3':
                  //  Debug.Log("3: " + gameObject.transform.position);
                    min_x = 1.5f;
                 //   max_x = 100f;
                    break;
                case '2':
                //    Debug.Log("2: " + gameObject.transform.position);
                    min_x = 1.4f;
                //    max_x = 100f;
                    break;
                case '1':
                //    Debug.Log("1: " + gameObject.transform.position);
                    min_x = 1.3f;
                  //  max_x = 100f;
                    break;
                default:
                    break;
            }
            blockRotation();
            nearBoard = true;
        }
    //    Debug.Log(other.gameObject.tag);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "HitBoxBoard")
        {
            nearBoard = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "HitBoxBoard")
        {
            nearBoard = false;
            blockRotation(false);
        }
    }
    void blockRotation(bool b = true)
    {
        gameObject.GetComponent<Rigidbody>().freezeRotation = b;
    }

    // Update is called once per frame
    void LateUpdate () {
		if(grabbed && nearBoard)
        {
            transform.localPosition = new Vector3(
                          // Mathf.Clamp(transform.localPosition.x, Sta.x, End.x),
                          Mathf.Clamp(transform.localPosition.x, min_x, max_x),
               transform.localPosition.y,
               transform.localPosition.z);
            //nearBoard = false;
        }
	}
    private void ChangeCenterOfMassOnThrow_InteractableObjectGrabbed(object sender, InteractableObjectEventArgs e)
    {
        grabbed = true;
    }
    private void ChangeCenterOfMassOnThrow_InteractableObjectUngrabbed(object sender, InteractableObjectEventArgs e)
    {
        grabbed = false;
    }


}
