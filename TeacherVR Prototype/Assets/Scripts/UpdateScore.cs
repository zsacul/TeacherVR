using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdateScore : MonoBehaviour
{
    public TextMeshPro TextMeshPro;
    
    void Update()
    {
        TextMeshPro.text = "Score - " + GameController.Instance.ScoreBoard.GetPoints();
    }
}