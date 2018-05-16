using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class PortalTeleporter : MonoBehaviour
{
    public Transform receiver;

    private bool playerIsOverlapping = false;

    void Update()
    {
        if (playerIsOverlapping)
        {
            Transform player = VRTK_DeviceFinder.GetControllerRightHand().transform;
            Vector3 portalToPlayer = player.position - transform.position;
            float dotProduct = Vector3.Dot(transform.up, portalToPlayer);

            if (dotProduct < 0f)
            {
                if(receiver.name == "ColliderPlane_A") GameController.Instance.ForceTeleportScript.ForceTeleportToTeleportA();
                else GameController.Instance.ForceTeleportScript.ForceTeleportToTeleportB();

                playerIsOverlapping = false;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "RController" || other.tag == "LController")
        {
            playerIsOverlapping = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "RController" || other.tag == "LController")
        {
            playerIsOverlapping = false;
        }
    }
}