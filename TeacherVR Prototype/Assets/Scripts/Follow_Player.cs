using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;
using System.Collections;
 
public class Follow_Player : MonoBehaviour {

    public GameObject target;
    public int speed;
    private Vector3 targetPoint;
    private Quaternion targetRotation;

 
 
     void Update()
     {
        targetPoint = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z) - transform.position;
        targetRotation = Quaternion.LookRotation (targetPoint, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * speed);
     }
}