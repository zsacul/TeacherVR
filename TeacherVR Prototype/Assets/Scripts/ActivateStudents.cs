using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateStudents : MonoBehaviour
{
    public GameObject[] firstlRow;
    public GameObject[] secondlRow;
    public GameObject[] thirdlRow;
    public GameObject[] fourthlRow;

    public GameObject[] firstrRow;
    public GameObject[] secondrRow;
    public GameObject[] thirdrRow;

    private bool allKilled = false;
    private int targetsNum = 0;
    private int targetsKilled = 0;

    void Update()
    {
        if (targetsNum != 0 && targetsKilled == targetsNum) allKilled = true;
    }

    public void Killed()
    {
        targetsKilled++;
        GameController.Instance.MessageSystem.SetProgressBar((float) targetsKilled / targetsNum * 100);
        Debug.Log(targetsNum);
        Debug.Log(targetsKilled);
    }

    public bool Destruction()
    {
        return allKilled;
    }

    public void Restart()
    {
        allKilled = false;
        targetsKilled = 0;
        targetsNum = 0;
    }

    public void throwAbort(GameObject[] row)
    {
        allKilled = false;
        targetsKilled = 0;
        targetsNum = 0;
        foreach (GameObject obj in row)
        {
            obj.transform.Find("Chalk_Throw_Tutorial_Point").GetComponent<Tutorial_Point_Anim_Control>().Abort();
        }
    }

    //right true, left false

    private void throwActive(GameObject[] row)
    {
        foreach (GameObject obj in row)
        {
            obj.transform.Find("Chalk_Throw_Tutorial_Point").GetComponent<Tutorial_Point_Anim_Control>().Resurrect();
            targetsNum++;
        }
    }

    public void throwRowAbort(int row, bool lr)
    {
        if (row == 1)
        {
            if (lr)
            {
                throwAbort(firstrRow);
            }
            else
            {
                throwAbort(firstlRow);
            }
        }
        else if (row == 2)
        {
            if (lr)
            {
                throwAbort(secondrRow);
            }
            else
            {
                throwAbort(secondlRow);
            }
        }
        else if (row == 3)
        {
            if (lr)
            {
                throwAbort(thirdrRow);
            }
            else
            {
                throwAbort(thirdlRow);
            }
        }
        else if (row == 4)
        {
            if (lr)
            {
                throwAbort(fourthlRow);
            }
        }
    }

    public void throwRow(int row, bool lr)
    {
        if (row == 1)
        {
            if (lr)
            {
                throwActive(firstrRow);
            }
            else
            {
                throwActive(firstlRow);
            }
        }
        else if (row == 2)
        {
            if (lr)
            {
                throwActive(secondrRow);
            }
            else
            {
                throwActive(secondlRow);
            }
        }
        else if (row == 3)
        {
            if (lr)
            {
                throwActive(thirdrRow);
            }
            else
            {
                throwActive(thirdlRow);
            }
        }
        else if (row == 4)
        {
            if (lr)
            {
                throwActive(fourthlRow);
            }
        }
    }
}