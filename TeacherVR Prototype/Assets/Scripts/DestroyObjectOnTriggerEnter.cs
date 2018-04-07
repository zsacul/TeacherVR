using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjectOnTriggerEnter : MonoBehaviour
{
    public string tag;

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag.Equals(tag)) Destroy(transform.root.gameObject);
    }
}