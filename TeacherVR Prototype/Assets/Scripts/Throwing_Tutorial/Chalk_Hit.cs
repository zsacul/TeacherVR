using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chalk_Hit : MonoBehaviour
{
    private AnimationControll script;

    private Tutorial_Point_Anim_Control tutorial_point_user;

    void Start()
    {
        script = gameObject.GetComponent<AnimationControll>();
        tutorial_point_user = transform.parent.parent.Find("Chalk_Throw_Tutorial_Point")
            .GetComponent<Tutorial_Point_Anim_Control>();
    }

    private void TakeHit(string tag)
    {
        if (tag == "Chalk" || tag == "Water")
        {
            script.Hit();
            if (tag == "Chalk" && GameController.Instance.EventsManager.GetCurrentEvent().name == "Throw Chalk" &&
                tutorial_point_user.gameObject.activeSelf && !tutorial_point_user.GetCorutineStatus())
            {
                GameController.Instance.Particles.CreateParticle(Particles.NaszeParticle.TwoHundredPoints,
                    transform.position + Vector3.up);
                GameController.Instance.ScoreBoard.PointsAddAnim(200);
                GameController.Instance.SoundManager.Play2D(SamplesList.Correct, 0.1f);
                tutorial_point_user.Kill();
            }
        }
    }

    void OnCollisionEnter(Collision col)
    {
        TakeHit(col.gameObject.tag);
    }

    void OnTriggerEnter(Collider col)
    {
        TakeHit(col.gameObject.tag);
    }
}