using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdateScore : MonoBehaviour
{
    public TextMeshPro TextMeshPro;
    
    void Update()
    {
        TextMeshPro.text = "You - " + GameController.Instance.ScoreBoard.GetNick() + " " + GameController.Instance.ScoreBoard.GetPoints();
        TextMeshPro.text += "\n1. - " + GameController.Instance.ScoreBoard.GetTopNick()[0] + " " + GameController.Instance.ScoreBoard.GetTopScore()[0];
        TextMeshPro.text += "\n2. - " + GameController.Instance.ScoreBoard.GetTopNick()[1] + " " + GameController.Instance.ScoreBoard.GetTopScore()[1];
        TextMeshPro.text += "\n3. - " + GameController.Instance.ScoreBoard.GetTopNick()[2] + " " + GameController.Instance.ScoreBoard.GetTopScore()[2];
        TextMeshPro.text += "\n4. - " + GameController.Instance.ScoreBoard.GetTopNick()[3] + " " + GameController.Instance.ScoreBoard.GetTopScore()[3];
        TextMeshPro.text += "\n5. - " + GameController.Instance.ScoreBoard.GetTopNick()[4] + " " + GameController.Instance.ScoreBoard.GetTopScore()[4];
    }
}