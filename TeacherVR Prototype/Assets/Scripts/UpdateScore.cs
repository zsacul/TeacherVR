using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdateScore : MonoBehaviour
{
    public TextMeshPro TextMeshPro;
    
    void Update()
    {
        TextMeshPro.text = "Your score - " + GameController.Instance.ScoreBoard.GetPoints();
        TextMeshPro.text += "\nTop 1 - " + GameController.Instance.ScoreBoard.GetTopScore()[0];
        TextMeshPro.text += "\nTop 2 - " + GameController.Instance.ScoreBoard.GetTopScore()[1];
        TextMeshPro.text += "\nTop 3 - " + GameController.Instance.ScoreBoard.GetTopScore()[2];
        TextMeshPro.text += "\nTop 4 - " + GameController.Instance.ScoreBoard.GetTopScore()[3];
        TextMeshPro.text += "\nTop 5 - " + GameController.Instance.ScoreBoard.GetTopScore()[4];
    }
}