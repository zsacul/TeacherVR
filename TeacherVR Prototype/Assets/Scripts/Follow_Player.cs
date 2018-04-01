using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using UnityEngine;
using System.Collections;

public class Follow_Player : MonoBehaviour
{
    public Transform target;
    public int speed;
    private Vector3 targetPoint;
    private Quaternion targetRotation;


    void Update()
    {
        if (target == null) target = VRTK.VRTK_DeviceFinder.HeadsetTransform();
        else
        {
            targetPoint = new Vector3(target.position.x, transform.position.y, target.position.z) - transform.position;
            targetRotation = Quaternion.LookRotation(targetPoint, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * speed);
        }
    }
}