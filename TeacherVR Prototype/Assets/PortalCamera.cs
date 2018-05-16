using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class PortalCamera : MonoBehaviour
{
    private Transform playerCamera;
    public Transform portal;
    public Transform otherPortal;

    void Start()
    {
        playerCamera = VRTK_DeviceFinder.HeadsetCamera();
    }

    void Update()
    {
        if (playerCamera == null)
        {
            playerCamera = VRTK_DeviceFinder.HeadsetCamera();
            return;
        }
        Vector3 playerOffsetFromPortal = playerCamera.position - otherPortal.position;
        transform.position = portal.position + playerOffsetFromPortal;

        float angulatDiffrenceBetweenPoeralRotations = Quaternion.Angle(portal.rotation, otherPortal.rotation);

        Quaternion portalRotationDiffrence = Quaternion.AngleAxis(angulatDiffrenceBetweenPoeralRotations, Vector3.up);
        Vector3 newCameraDirection = portalRotationDiffrence * playerCamera.forward;
        transform.rotation = Quaternion.LookRotation(newCameraDirection, Vector3.up);
    }
}