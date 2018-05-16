using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class PortalTeleporter : MonoBehaviour
{
    public Transform receiver;
    public Skybox CameraSkybox;
    
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "RController" || other.tag == "LController")
        {
            Transform player;
            if(other.tag == "RController")  player = VRTK_DeviceFinder.GetControllerRightHand().transform;
            else player = VRTK_DeviceFinder.GetControllerLeftHand().transform;

            Vector3 portalToPlayer = player.position - transform.position;
            float dotProduct = Vector3.Dot(transform.up, portalToPlayer);
            Debug.Log(dotProduct);
            if (dotProduct > 0.1f)
            {
                if (receiver.name == "ColliderPlane_A") GameController.Instance.ForceTeleportScript.ForceTeleportToTeleportA();
                else GameController.Instance.ForceTeleportScript.ForceTeleportToTeleportB();

                RenderSettings.skybox = CameraSkybox.material;
            }
        }
    }
}