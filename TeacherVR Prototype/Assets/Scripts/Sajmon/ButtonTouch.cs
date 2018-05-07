using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTouch : MonoBehaviour {

    private int id;
    private bool pressed;
    public bool showing;
    private Color color;
    private SamplesList sound;
    private Animator animator;
    private void Start()
    {
        
        color = gameObject.GetComponent<MeshRenderer>().material.color;
        animator = gameObject.GetComponent<Animator>();

        pressed = false;
        showing = false;
        setId();
    }
    void Update()
    {
        if (!pressed && !showing && gameObject.GetComponent<TouchDetector>().Trigger == true )
        {
            PushButton();
        }
    }
    public void PushButton()
    {
        gameObject.GetComponent<MeshRenderer>().material.color = Color.white;
        pressed = true;

        GameController.Instance.SoundManager.Play3DAt(sound,gameObject.transform);

       
 
        if (!showing)
        {

            animator.SetTrigger(getAnimId());
            Sajmon.seqPlayerSequence += id.ToString();
            Sajmon.needToCheck = true;

        }



        Invoke("UnpushButton", 1.5f);
    }

    void UnpushButton()
    {

        gameObject.GetComponent<MeshRenderer>().material.color = color;
        pressed = false;
    }
    string getAnimId()
    {
        if (id == 1)
            return "AniB";
        if (id == 2)
            return "AniY";
        if (id == 3)
            return "AniR";
        return "AniG";
    }
    void setId()
    {
        switch(gameObject.name)
        {
            case "Blue":
                id = 1;
                sound = SamplesList.Simon1;
                break;
            case "Yellow":
                id = 2;
                sound = SamplesList.Simon2;
                break;
            case "Red":
                id = 3;
                sound = SamplesList.Simon3;
                break;
            case "Green":
                id = 4;
                sound = SamplesList.Simon4;
                break;
        }
    }

}
