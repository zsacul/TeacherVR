using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeScript : MonoBehaviour
{
    public bool ReturnToStart = true;
    public float TimeToReturn = 3f;
    public float DodgeMaxVelocity = 10f;
    public float Smoothing = 0.2f;
    public float SmoothingReturn = 0.07f;
    public Vector2 WaitToStartDodge = new Vector2(0f,0f);
    public Vector2 DodgeTime = new Vector2(1f, 2f);
    public Vector2 AfterDodgeWait = new Vector2(0f, 0f);

    private float lastDodge;
    private Vector3 startTransform;
    private float targetDodgeX;
    private float targetDodgeZ;
    private Rigidbody rb;

    void Start()
    {
        startTransform = transform.position;
        rb = GetComponent<Rigidbody>();
        StartCoroutine(EvadeX());
        StartCoroutine(EvadeZ());
    }

    IEnumerator EvadeX()
    {
        yield return new WaitForSeconds(Random.Range(WaitToStartDodge.x, WaitToStartDodge.y));

        while (true)
        {
            targetDodgeX = DodgeMaxVelocity;
            yield return new WaitForSeconds(Random.Range(DodgeTime.x, DodgeTime.y));
            targetDodgeX = 0;
            yield return new WaitForSeconds(Random.Range(AfterDodgeWait.x, AfterDodgeWait.y));
            targetDodgeX = -DodgeMaxVelocity;
            yield return new WaitForSeconds(Random.Range(DodgeTime.x, DodgeTime.y));
            targetDodgeX = 0;
            yield return new WaitForSeconds(Random.Range(AfterDodgeWait.x, AfterDodgeWait.y));
        }
    }

    IEnumerator EvadeZ()
    {
        yield return new WaitForSeconds(Random.Range(WaitToStartDodge.x, WaitToStartDodge.y));

        while (true)
        {
            targetDodgeZ = DodgeMaxVelocity;
            yield return new WaitForSeconds(Random.Range(DodgeTime.x, DodgeTime.y));
            targetDodgeZ = 0;
            yield return new WaitForSeconds(Random.Range(AfterDodgeWait.x, AfterDodgeWait.y));
            targetDodgeZ = -DodgeMaxVelocity;
            yield return new WaitForSeconds(Random.Range(DodgeTime.x, DodgeTime.y));
            targetDodgeZ = 0;
            yield return new WaitForSeconds(Random.Range(AfterDodgeWait.x, AfterDodgeWait.y));
        }
    }
/*
    float Distance(Transform from, Transform to) //temporary
    {
        return Mathf.Sqrt((to.position.x - from.position.x) * (to.position.x - from.position.x) +
                          (to.position.z - from.position.z) * (to.position.z - from.position.z));
    }*/

    void Update()
    {
        if (ReturnToStart && Time.time > TimeToReturn + lastDodge)
        {
            rb.velocity = new Vector3(0.0f, 0.0f, 0.0f);
            transform.position =
                new Vector3(Mathf.SmoothStep(transform.position.x, startTransform.x, SmoothingReturn),
                    transform.position.y, Mathf.SmoothStep(transform.position.z, startTransform.z, SmoothingReturn));
            if (Vector3.Distance(transform.position, startTransform) < 0.1f)
            {
                lastDodge = Time.time;
            }
        }
        else
        {
            float newDodgeX = Mathf.MoveTowards(rb.velocity.x, targetDodgeX, Time.deltaTime * Smoothing);
            float newDodgeZ = Mathf.MoveTowards(rb.velocity.z, targetDodgeZ, Time.deltaTime * Smoothing);
            rb.velocity = new Vector3(newDodgeX, 0.0f, newDodgeZ);
        }
    }
}