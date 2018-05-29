namespace VRTK.Examples
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using VRTK.Controllables;



    public class ClawController : MonoBehaviour
    {

        public bool target = true;
        public bool destination = false;
        public ClawControl cntrl;
        public Animator head;
        public Animator clawNeck;
        public LeverTempControll LeverX;
        public LeverTempControll LeverY;
        public ControllableReactor Button;
        public Animator AnimButton;
        public ActivateStudents students;
        public GameObject clawGameObject;


        private bool follow = false;
        private GameObject student;
        private Transform studentTransform;
        private Transform clawTransform;
        private AnimatorClipInfo[] currentClipInfo;
        private bool nomed = false;
        private IEnumerator coroutine;
        private Vector3 offset;


        private void Start()
        {
            student = students.thirdlRow[1];
            studentTransform = student.transform;
            clawTransform = clawGameObject.transform;
            offset = new Vector3(0.0f, 1.8f, 0.0f);
        }



        void Update()
        {
            cntrl.moveX = LeverX.val/10;
            cntrl.moveY = LeverY.val/10;
            Follow(follow);
            if (target)
            {
                //button.SetTrigger("Green");
                
                //button green
                if (!nomed)
                {
                    if (Button.reachedmaxOut)
                    {
                        target = false;
                        destination = true;
                        nomed = true;
                        coroutine = Nom();
                        StartCoroutine(coroutine);
                    }
                }
                //button.SetTrigger("Red");

            }
            else if (destination)
            {

                AnimButton.SetTrigger("Green");

                //button green
                if (nomed)
                {
                    if (Button.reachedmaxOut)
                    {
                        target = false;
                        destination = false;
                        nomed = false;
                        coroutine = OdNom();
                        StartCoroutine(coroutine);
                        AnimButton.SetTrigger("Red");
                    }
                }
                


            }
            

            

            
            
        }

        private IEnumerator Nom()
        {
            clawNeck.SetTrigger("Down");
            head.SetTrigger("Open");
            yield return new WaitForSeconds(1f);
            head.SetTrigger("Nom");
            yield return new WaitForSeconds(0.77f);
            follow = true;
            clawNeck.SetTrigger("Up");
        }

        private IEnumerator OdNom()
        {
            clawNeck.SetTrigger("Down");
            yield return new WaitForSeconds(1.50f);
            head.SetTrigger("Open");
            yield return new WaitForSeconds(0.2f);
            follow = false;
            clawNeck.SetTrigger("Up");
            yield return new WaitForSeconds(1.0f);
            head.SetTrigger("Nom");
            
        }

        private void Follow(bool f)
        {
            if (f)
            {
                studentTransform.position = Vector3.MoveTowards(studentTransform.position, clawTransform.position - offset, 0.1f);
                Debug.Log(studentTransform.position);

                Debug.Log(clawTransform.position);
            }
        }
    }
}


