using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chalk_Hit : MonoBehaviour
{
    public enum HitSound
    {
        Male,
        Female
    }

    public HitSound Gender = HitSound.Male;

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
            if (tag == "Chalk")
            {
                if (Gender == HitSound.Male)
                    GameController.Instance.SoundManager.Play3DAt(SamplesList.MaleGrunt, transform.position, 0.01f);
                if (Gender == HitSound.Female)
                    GameController.Instance.SoundManager.Play3DAt(SamplesList.FemaleOof, transform.position, 0.01f);

                if (GameController.Instance.EventsManager.GetCurrentEvent() != null &&
                    GameController.Instance.EventsManager.GetCurrentEvent().name == "Throw Chalk" &&
                    tutorial_point_user.gameObject.activeSelf && !tutorial_point_user.GetCorutineStatus())
                {
                    GameController.Instance.Particles.CreateParticle(Particles.NaszeParticle.Small_Good_Correct_Ok,
                        transform.position + Vector3.up);
                    GameController.Instance.ScoreBoard.PointsAddAnim(100);
                    tutorial_point_user.Kill();
                }
            }
        }
    }

    void OnCollisionEnter(Collision col)
    {
        TakeHit(col.gameObject.tag);
    }
}