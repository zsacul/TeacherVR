using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCollider : MonoBehaviour
{
    public GameObject ColliderToIgnore;
    void Start()
    {
        if(ColliderToIgnore!=null)
            foreach (var col in ColliderToIgnore.GetComponents<Collider>())
            {
                Physics.IgnoreCollision(GetComponent<Collider>(), col);
            }
    }
}