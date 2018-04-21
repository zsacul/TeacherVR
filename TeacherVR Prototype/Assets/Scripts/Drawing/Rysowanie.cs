using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rysowanie : MonoBehaviour
{
    private float templateThickness;
    private float pointThickness;
    private float templateThicknessAccept;
    private float pointThicknessAccept;
    public bool gameInProgress = false;
    private int currentTarget = 0;
    private int comboCounter = 0;
    private Vector2[] TemplateShape;
    public VRTK_Senello_TexturePainter Paint;
    private Color chalkColor;
    private Vector3 brushLocation;
    private Vector2 brushLocation2;

    float DistanceFromLine(Vector2 start, Vector2 end, Vector2 point)
    {
        float dist = Vector2.Distance(start, end);
        if (dist == 0)
        {
            return Vector2.Distance(start, point);
        }
        else
        {
            float t = Mathf.Clamp01(Vector2.Dot(point - start, end - start) / (dist * dist));
            Vector2 projection = start + Vector2.Scale((end - start), new Vector2(t, t));
            return Vector2.Distance(projection, point);
        }
    }

    void OnEnable()
    {
        currentTarget = 0;
        comboCounter = 0;
        templateThickness = GameController.Instance.DrawingManager.templateThickness;
        pointThickness = GameController.Instance.DrawingManager.pointThickness;
        templateThicknessAccept = GameController.Instance.DrawingManager.templateThicknessAccept;
        pointThicknessAccept = GameController.Instance.DrawingManager.pointThicknessAccept;
        chalkColor = GameController.Instance.ChalkColor;
        TemplateShape = GameController.Instance.DrawingManager.TemplateShape;
        brushLocation = Paint.brushContainer.transform.position;
        brushLocation2 = new Vector2(brushLocation.x, brushLocation.y);

        for (int i = 0; i < TemplateShape.GetLength(0); i++)
        {
            if (i < TemplateShape.GetLength(0) - 1)
                Paint.DrawLine(brushLocation2 + TemplateShape[i], brushLocation2 + TemplateShape[i + 1],
                    templateThickness, Color.white);
            Paint.DrawPoint(new Vector3(TemplateShape[i].x, TemplateShape[i].y, -0.01f), pointThickness,
                Color.red);
        }
        if (TemplateShape.GetLength(0) >= 2)
        {
            Paint.DrawPoint(new Vector3(TemplateShape[0].x, TemplateShape[0].y, -0.02f), pointThickness,
                Color.green);
        }
    }

    void Update()
    {
        if (!gameInProgress) return;
        if (GameController.Instance.DrawingManager.RysObject == null) return;

        RaycastHit hit = GameController.Instance.DrawingManager.RysObject.GetComponent<VRTK.VRTK_Pointer>().pointerRenderer
            .GetDestinationHit();
        if (hit.transform == null)
        {
            return;
        }

        Renderer rend = hit.transform.GetComponent<Renderer>();

        if (rend == null || rend.sharedMaterial == null || rend.sharedMaterial.mainTexture == null ||
            rend != gameObject.GetComponent<Renderer>())
        {
            return;
        }

        TemplateShape = GameController.Instance.DrawingManager.TemplateShape;

        /*if (!interpolate)
        {
            ClearBoard();
        }*/

        Vector2 pixelUV = Paint.GetLastPoint();
        bool mistake = false;

        //Check against the template only if it exists
        if (TemplateShape.GetLength(0) > 1)
        {
            if (pixelUV == Vector2.zero || currentTarget != 0 && DistanceFromLine(
                    brushLocation2 + TemplateShape[currentTarget - 1],
                    brushLocation2 + TemplateShape[currentTarget], pixelUV) >
                templateThicknessAccept)
            {
                //Player made a mistake
                comboCounter = 0;
                mistake = true;
                Debug.Log("A mistake! Combo: " + comboCounter);
            }

            if (DistanceFromLine(brushLocation2 + TemplateShape[currentTarget],
                    brushLocation2 + TemplateShape[currentTarget], pixelUV) <=
                pointThicknessAccept)
            {
                comboCounter++;
                //Player reached a checkpoint
                if (currentTarget == TemplateShape.GetLength(0) - 1)
                {
                    Debug.Log("You finished the shape! Combo: " + comboCounter);
                    GameController.Instance.MessageSystem.SetProgressBar(100);
                    gameInProgress = false;
                }
                else
                {
                    Debug.Log("Good job! Combo: " + comboCounter);
                    currentTarget++;
                    Paint.DrawPoint(new Vector3(TemplateShape[currentTarget].x, TemplateShape[currentTarget].y, -0.02f),
                        pointThickness,
                        Color.green);
                    GameController.Instance.MessageSystem.SetProgressBar((float)currentTarget / TemplateShape.GetLength(0) * 100);
                }
            }
        }

        if (mistake || currentTarget == 0 || !gameInProgress) GameController.Instance.ChalkColor = chalkColor;
        else GameController.Instance.ChalkColor = Color.grey;
    }
}