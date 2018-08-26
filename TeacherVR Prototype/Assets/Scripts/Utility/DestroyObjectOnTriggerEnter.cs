using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjectOnTriggerEnter : MonoBehaviour
{
    public string[] tag;
    public bool DestroyObjectsEvent = true;
    public float LifeTime = 0;
    public bool DestroyRoot = true;

    private GameController gameController;

    void Start()
    {
        gameController = GameController.Instance;
    }

    private void OnTriggerEnter(Collider col)
    {
        foreach (var str in tag)
        {
            if (col.tag.Equals(str))
            {
                if (DestroyObjectsEvent)
                {
                    gameController.Particles.CreateParticle(Particles.NaszeParticle.FiftyPoints,transform.position + Vector3.up/4);
                    gameController.ScoreBoard.PointsAddAnim(50);
                }
                if(DestroyRoot) Destroy(transform.root.gameObject, LifeTime);
                else Destroy(gameObject, LifeTime);
            }
        }
    }
}