using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdateScore : MonoBehaviour
{
    private TextMeshProUGUI TMPUGUI;

    void Start()
    {
        TMPUGUI = GetComponentInChildren<TextMeshProUGUI>();
    }

    void Update()
    {
        TMPUGUI.text = "Score: " + GameController.Instance.ScoreBoard.GetPoints();
    }
}