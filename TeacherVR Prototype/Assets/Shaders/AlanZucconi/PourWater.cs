using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PourWater : MonoBehaviour
{
    public Material material;

    public float speed;
    public float cost;
    public float maxFuel;
    public float offset;

    public Transform Top;

    private float fuel;
    private float diff;
    private void Start()
    {
        fuel = 0;
    }

    private void Update()
    {
        diff = (Top.transform.position.y - transform.position.y);
        if(diff>0) material.SetFloat("_ConstructY", transform.position.y - offset + (fuel/maxFuel)*diff);
        else material.SetFloat("_ConstructY", Top.transform.position.y - offset + (fuel / maxFuel) * (transform.position.y - Top.transform.position.y));
    }

    public void OnTriggerStay(Collider col)
    {
        if (col.tag.Equals("WaterSource"))
        {
            if (fuel <= maxFuel)
                fuel = Mathf.Lerp(fuel, fuel + cost,
                    speed * Time.deltaTime);
        }
    }

    public bool Use()
    {
        if (fuel > cost)
        {
            fuel -= cost;
            return true;
        }
        return false;
    }
}