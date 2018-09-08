using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using VRTK;
[CreateAssetMenu(fileName = "Lecture", menuName = "Events/Lecture")]
public class Lecture : Events
{
    [Header("Custom Settings")]
    public GameObject knowledgeBeam_prefab;
    public float knowledge_beam_SpawnDelay;
    private float currentTime;
    public  GameObject lightbulb_prefab;
    public GameObject[] lightbulbs;
    private float lastSpawnTime;
    public int[] Students_ToLecture;
    public static int remainingStudents;
    private int notYETPointedStudents;
    public Vector3 shootPos;
    public List<GameObject> studentsToLect_GOs; 

    public override void StartEvent()
    {
        base.StartEvent();
        remainingStudents = 100;
        currentTime = Time.time;
        lastSpawnTime = 0;
       /* if (Lvl < 2)
            Lvl=2;
        int maxLvl = Lvl + 5;
        if (maxLvl > GameController.Instance.Students.Students.Length - 1)
            maxLvl = GameController.Instance.Students.Students.Length - 1;*/
        // Students_ToLecture = new int[Random.Range(Lvl,maxLvl)];
        Students_ToLecture = new int[5];
       // Debug.Log("Tyle studentów " + (Students_ToLecture.Length-1));
        studentsToLect_GOs = new List<GameObject>();
        for (int i = 0; i < Students_ToLecture.Length-1; i++)
        {
            bool goodStudentToLecture = true;
            Students_ToLecture[i] = -1;
            int newStudentToLecture = Random.Range(0, GameController.Instance.Students.Students.Length - 1);
            for (int j = 0; j <= i; j++)
            {
                if(Students_ToLecture[j]==-1)
                {
                    Students_ToLecture[j] = newStudentToLecture;
                    break;
                }
                else
                if(Students_ToLecture[j]==newStudentToLecture)
                {
                    i--;
                    goodStudentToLecture = false;
                    break;
                }
                else if(j == i)
                {
                    Students_ToLecture[i] = newStudentToLecture;
                }
            }
            if (goodStudentToLecture)
            {
                studentsToLect_GOs.Add(GameController.Instance.Students.Students[newStudentToLecture]);
               // Debug.Log("Student no." + newStudentToLecture + "does not understand. Lecture him/her.");
                GameController.Instance.Students.Students[newStudentToLecture].GetComponentInChildren<AnimationControll>().Talk(true);
            }
            
        }
        remainingStudents = studentsToLect_GOs.Count;
        notYETPointedStudents = remainingStudents;
        lightbulbs = new GameObject[remainingStudents];
        for (int i = 0; i < remainingStudents; i++)
        {
            lightbulbs[i] = Instantiate(lightbulb_prefab, studentsToLect_GOs[i].transform);
        }

        Message(10, description, MessageSystem.ObjectToFollow.Headset, MessageSystem.Window.W800H400);

        MicInput.typeOfInput= MicInput.MicInputType.speechDetection;


    }

    public override void CompleteEvent()
    {
        base.CompleteEvent();
        for (int i = 0; i < lightbulbs.Length; i++)
        {
            if(lightbulbs[i]!=null)
            Destroy(lightbulbs[i]);
        }
        for (int i = 0; i < studentsToLect_GOs.Count; i++)
        {
            studentsToLect_GOs[i].GetComponentInChildren<AnimationControll>().Talk(false);
        }
        MicInput.typeOfInput = MicInput.MicInputType.noone;
    }
    public override void AbortEvent()
    {
        base.AbortEvent();
        for (int i = 0; i < lightbulbs.Length; i++)
        {
            if (lightbulbs[i] != null)
                Destroy(lightbulbs[i]);
        }
        for (int i = 0; i < studentsToLect_GOs.Count; i++)
        {
            studentsToLect_GOs[i].GetComponentInChildren<AnimationControll>().Talk(false);
        }
        MicInput.typeOfInput = MicInput.MicInputType.noone;
    }




    public override void CallInUpdate()
    {
        base.CallInUpdate();
        currentTime = Time.time;
        if(GameController.Instance.MicInput.isSpeaking && (currentTime >= lastSpawnTime + 0.25f))
        {

            Transform headseatTransform = VRTK_DeviceFinder.HeadsetCamera();
            
            Vector3 spawnPosition = headseatTransform.position;
            spawnPosition = new Vector3(spawnPosition.x + shootPos.x, spawnPosition.y + shootPos.y, spawnPosition.z + shootPos.z);
            Quaternion spawnRotation = headseatTransform.rotation;
            
                
            Instantiate(knowledgeBeam_prefab, spawnPosition, spawnRotation);
           
            
            lastSpawnTime = Time.time;

        }
        while(notYETPointedStudents>remainingStudents)
        {
            AddPoints(50);
            notYETPointedStudents--;
        }
        if (remainingStudents == 0)
            CompleteEvent();
    }


}
