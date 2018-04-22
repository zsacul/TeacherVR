using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateStudents : MonoBehaviour {

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
        if (targetsKilled == targetsNum) allKilled = true;
    }

    public void Killed()
    {
        targetsKilled++;
    }

    public bool Destruction()
    {
        return allKilled;
    }


	//right true, left false

	private void throwActive(GameObject[] row){
		Debug.Log("topkek");
		foreach (GameObject obj in row) {
			obj.transform.Find("Chalk_Throw_Tutorial_Point").gameObject.SetActive(true);
			targetsNum++;
		}
	}

	public void throwRow(int row, bool lr){
		if (row == 1) {
			if (lr) {
 				throwActive(firstrRow);
			} else {
				throwActive(firstlRow);
			}
		}
		else if (row == 2) {
			if (lr) {
				throwActive(secondrRow);
			} else {
				throwActive(secondlRow);	
			}
		}
		else if (row == 3) {
			if (lr) {
				throwActive(thirdrRow);
			} else {
				throwActive(thirdlRow);	
			}
		}
		else if (row == 4) {
			if (lr) {
				throwActive(fourthlRow);
			} //else {
				//throwActive();	
			//}
		}
	}


	
}
