using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjectOnTriggerEnter : MonoBehaviour
{
    public string[] tag;
    public bool DestroyObjectsEvent = true;

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
                    gameController.SoundManager.Play3DAt(SamplesList.Correct, transform.position,0.01f);
                    gameController.Particles.CreateParticle(Particles.NaszeParticle.FiftyPoints,transform.position);
                    gameController.ScoreBoard.PointsAdd(50);
                }
                Destroy(transform.root.gameObject);
            }
        }
    }
}