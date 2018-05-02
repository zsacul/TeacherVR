using UnityEngine;
using VRTK;

public class AnimControlOnGrab : MonoBehaviour
{
    public float min;
    public float max;
    public string AnimName;
    public Animation anim;
    public Events[] EventsWhenStopGrab;

    private bool IfEndAnim = false;

    private VRTK_InteractableObject interactableObject;

    void Start()
    {
        anim.Play(AnimName);
        anim[AnimName].speed = 0;
        interactableObject = GetComponent<VRTK_InteractableObject>();
        interactableObject.InteractableObjectGrabbed += RestartAnimation;
        interactableObject.InteractableObjectUngrabbed += EndAnimation;
    }

    private void RestartAnimation(object sender, InteractableObjectEventArgs e)
    {
        IfEndAnim = false;
    }

    public void Restart()
    {
        IfEndAnim = false;
        transform.localPosition = new Vector3(min, transform.localPosition.y, transform.localPosition.z);
        interactableObject.isGrabbable = true;
        anim[AnimName].speed = 0;
        anim[AnimName].normalizedTime = 0;
        anim.Play(AnimName);
    }

    private void EndAnimation(object sender, InteractableObjectEventArgs e)
    {
        if (anim[AnimName].normalizedTime > 0.4f)
        {
            IfEndAnim = true;
            anim[AnimName].speed = 0.3f;
            transform.localPosition = new Vector3(max, transform.localPosition.y, transform.localPosition.z);
            
            foreach (var ev in EventsWhenStopGrab)
            {
                if (GameController.Instance.EventsManager.GetCurrentEvent().name == ev.name)
                    interactableObject.isGrabbable = false;
            }
        }
    }
    
    void LateUpdate()
    {
        if (!IfEndAnim)
            anim[AnimName].normalizedTime = (transform.localPosition.x - min) / (max - min);
    }
}