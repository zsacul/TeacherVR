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
        public Vector3 defaultLocation;
        public bool nomed = false;


        private bool follow = false;
        private GameObject student;
        private Transform studentTransform;
        private Transform clawTransform;
        private AnimatorClipInfo[] currentClipInfo;
        private IEnumerator coroutine;
        private Vector3 offset;
        private AnimationControll studentAnimator;
        private GameObject destination;
        private Vector3 destinationTransform;
        private bool fin = false;
        
        


        private void Start()
        {
            
            
            offset = new Vector3(0.1f, 1.8f, 0.0f);
            clawGameObject = GameObject.Find("Claw/ClawBelka/ClawHolder/Claw/ClawPiston/Head");
            students = GameObject.Find("Students").GetComponent<ActivateStudents>();
            cntrl = GameObject.FindGameObjectWithTag("Claw").GetComponent<ClawControl>();
            head = clawGameObject.GetComponent<Animator>();
            clawNeck = GameObject.Find("Claw/ClawBelka/ClawHolder/Claw").GetComponent<Animator>();
            student = RandStudent();
            studentTransform = student.transform;
            defaultLocation = studentTransform.position;
            clawTransform = clawGameObject.transform;
            studentAnimator = student.gameObject.transform.GetChild(1).GetChild(0).gameObject.GetComponent<AnimationControll>();
            destination = GameObject.Find("Destination");
            destinationTransform = destination.transform.position;
            
            

        }



        void Update()
        {

            if (Input.GetKeyDown("r")) 
            {
                Reset();
            }

            Debug.Log(Finished());
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
                        fin = true;
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
            destinationTransform = defaultLocation;


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

        public GameObject RandStudent()
        {
            System.Random rnd = new System.Random();
            int row = rnd.Next(0, 6);
            if (row == 0)
            {
                return students.firstlRow[rnd.Next(0, students.firstlRow.Length)];
            }
            else if (row == 1)
            {
                return students.secondlRow[rnd.Next(0, students.secondlRow.Length)];
            }
            else if (row == 2)
            {
                return students.thirdlRow[rnd.Next(0, students.thirdlRow.Length)];
            }
            else if (row == 3)
            {
                return students.fourthlRow[rnd.Next(0, students.fourthlRow.Length)];
            }
            else if (row == 4)
            {
                return students.firstrRow[rnd.Next(0, students.firstrRow.Length)];
            }
            else if (row == 5)
            {
                return students.secondrRow[rnd.Next(0, students.secondrRow.Length)];
            }
            else
            {
                return students.thirdrRow[rnd.Next(0, students.thirdrRow.Length)];
            }
            

        }

        public void Reset()
        {
            follow = false;
           
            studentTransform.position = Vector3.MoveTowards(studentTransform.position, defaultLocation, 0.5f);
           
           // studentTransform.position = defaultLocation;

            studentAnimator.Hanging(false);
            studentAnimator.Shake(false);
        }

        public bool Finished()
        {
            if ((Math.Abs(studentTransform.position.x - defaultLocation.x) < 0.2f && Math.Abs(studentTransform.position.z - defaultLocation.z ) < 0.2f && (studentTransform.position.y - defaultLocation.y) < 0.2f) && !nomed && fin)
            {
                //play some animation maybe?
                Reset();
               return true;
            } else
            {
               return false;
            }
        }

    }
}


