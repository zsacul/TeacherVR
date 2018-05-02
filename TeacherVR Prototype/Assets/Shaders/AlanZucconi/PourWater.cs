using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PourWater : MonoBehaviour
{
    public Material MaterialToCopy;
    public Shader ShaderToCopy;

    public float speed;
    public float cost;
    public float maxFuel;
    public float offset;

    public Transform Top;
    public Transform Bot;

    private float fuel;
    private float diff;

    private GameObject waterSound;
    private Material material;

    private void Start()
    {
        material = new Material(ShaderToCopy);
        material.CopyPropertiesFromMaterial(MaterialToCopy);
        foreach (Transform child in transform)
        {
            child.GetComponent<Renderer>().material = material;
        }
        fuel = 0;
    }

    private void Update()
    {
        diff = (Top.position.y - Bot.position.y);
        if (diff > 0) material.SetFloat("_ConstructY", Bot.position.y - offset + (fuel / maxFuel) * diff);
        else
            material.SetFloat("_ConstructY",
                Top.position.y - offset +
                (fuel / maxFuel) * (Bot.position.y - Top.position.y));
    }

    public void OnTriggerStay(Collider col)
    {
        if (col.tag.Equals("WaterSource"))
        {
            if (canFill())
            {
                fuel = Mathf.Lerp(fuel, fuel + cost, speed * Time.deltaTime);
            }
        }
    }

    bool canFill()
    {
        return fuel <= maxFuel &&
               (transform.eulerAngles.x >= 270 && transform.eulerAngles.x <= 360 ||
                transform.eulerAngles.x >= -0.1 && transform.eulerAngles.x <= 90) &&
               (transform.eulerAngles.z >= 270 && transform.eulerAngles.z <= 360 ||
                transform.eulerAngles.z >= -0.1 && transform.eulerAngles.z <= 90);
    }

    public void OnTriggerEnter(Collider col)
    {
        if (col.tag.Equals("WaterSource"))
        {
            if (canFill())
            {
                waterSound =
                    GameController.Instance.SoundManager.Play3DAt(SamplesList.BottleFilling, transform.position,0.1f);
            }
        }
    }

    public void OnTriggerExit(Collider col)
    {
        if (col.tag.Equals("WaterSource"))
        {
            if (waterSound != null)
            {
                waterSound.GetComponent<AudioSource>().Stop();
            }
        }
    }

    public bool Use()
    {
        if (fuel > cost)
        {
            fuel -= cost;
            GameController.Instance.SoundManager.Play3DAt(SamplesList.BottleSpray, transform.position,0.1f);
            return true;
        }
        return false;
    }
}