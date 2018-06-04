namespace VRTK.Examples
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using VRTK.Controllables;
    using System;



    public class ClawController : MonoBehaviour
    {

        public bool target = true;
        //public bool destination = false;
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
        private AnimationControll studentAnimator;
        private GameObject destination;
        private Vector3 destinationTransform;
        private Vector3 deafultLocation;
        


        private void Start()
        {
            
            
            offset = new Vector3(0.0f, 1.8f, 0.0f);
            clawGameObject = GameObject.Find("Claw/ClawBelka/ClawHolder/Claw/ClawPiston/Head");
            students = GameObject.Find("Students").GetComponent<ActivateStudents>();
            cntrl = GameObject.FindGameObjectWithTag("Claw").GetComponent<ClawControl>();
            head = clawGameObject.GetComponent<Animator>();
            clawNeck = GameObject.Find("Claw/ClawBelka/ClawHolder/Claw").GetComponent<Animator>();
            student = students.thirdlRow[1];
            studentTransform = student.transform;
            deafultLocation = studentTransform.position;
            clawTransform = clawGameObject.transform;
            studentAnimator = student.gameObject.transform.GetChild(1).GetChild(0).gameObject.GetComponent<AnimationControll>();
            destination = GameObject.Find("Destination");
            destinationTransform = destination.transform.position;
            
            

        }



        void Update()
        {

            //Debug.Log(clawTransform.position);
            //Debug.Log(studentTransform.position);
            //TargetCloseEnough();
            cntrl.moveX = LeverX.val/10;
            cntrl.moveY = LeverY.val/10;
            Follow(follow);
            //if (target)
            //{
                //button.SetTrigger("Green");
                
                //button green
                if (!nomed)
                {
                    studentAnimator.Shake(true);
                    TargetCloseEnough(studentTransform);
                    if (target)
                    {   
                        if (Button.reachedmaxOut)
                        {
                            //target = false;
                            //destination = true;
                            nomed = true;
                            coroutine = Nom();
                            StartCoroutine(coroutine);
                        }
                    }
                    
                    
                }
                //button.SetTrigger("Red");

            //}
           // else if (destination)
            //{

                //AnimButton.SetTrigger("Green");

                //button green
                else
                {
                    DestinationCloseEnough(destinationTransform);
                    if (target)
                    {
                        if (Button.reachedmaxOut)
                        {
                            //target = false;
                            //destination = false;
                            
                            nomed = false;
                            coroutine = OdNom();
                            StartCoroutine(coroutine);
                            //AnimButton.SetTrigger("Red");
                        }
                    }
                    
                }
                


            //}
            

            

            
            
        }

        private IEnumerator Nom()
        {
            clawNeck.SetTrigger("Down");
            head.SetTrigger("Open");
            yield return new WaitForSeconds(1f);
            head.SetTrigger("Nom");
            yield return new WaitForSeconds(0.77f);
            studentAnimator.Shake(false);
            studentAnimator.Hanging(true);
            follow = true;
            clawNeck.SetTrigger("Up");
            //AnimButton.SetTrigger("Red");

        }

        private IEnumerator OdNom()
        {
            
            clawNeck.SetTrigger("Down");
            yield return new WaitForSeconds(1.50f);
            head.SetTrigger("Open");
            yield return new WaitForSeconds(0.2f);
            studentAnimator.Hanging(false);
            studentAnimator.Shake(true);
            follow = false;
            clawNeck.SetTrigger("Up");
            yield return new WaitForSeconds(1.0f);
            head.SetTrigger("Nom");
            destinationTransform = deafultLocation;


        }

        private void Follow(bool f)
        {
            if (f)
            {
                studentTransform.position = Vector3.MoveTowards(studentTransform.position, clawTransform.position - offset, 0.1f);
                //Debug.Log(studentTransform.position);

                //Debug.Log(clawTransform.position);
            }
        }

        //x z
        private void TargetCloseEnough(Transform obj)
        {
            //Debug.Log("claw");
            ////Debug.Log(clawTransform.position);
            //Debug.Log("object");
            //Debug.Log(obj.position);
            if (Math.Abs(clawTransform.position.x - obj.position.x) < 0.2f && Math.Abs(clawTransform.position.z - obj.position.z) < 0.2f)
            {
               // Debug.Log("green");
                target = true;
                AnimButton.SetBool("Green", true);
            } else
            {
                target = false;
                AnimButton.SetBool("Green", false);
            }
        }

        private void DestinationCloseEnough(Vector3 obj)
        {

            if (Math.Abs(clawTransform.position.x - obj.x) < 0.2f && Math.Abs(clawTransform.position.z - obj.z) < 0.2f)
            {
                // Debug.Log("green");
                target = true;
                AnimButton.SetBool("Green", true);
            }
            else
            {
                target = false;
                AnimButton.SetBool("Green", false);
            }
        }


    }
}


