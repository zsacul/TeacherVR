using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rysowanie : MonoBehaviour
{
    public int brushSize;

    private bool canApply = true;
    private bool interpolate = false;
    private bool gameInProgress = true;
    private int currentTarget = 0;
    private int comboCounter = 0;
    private float templateThickness = 0.02f;
    private Texture2D drawingTexture;
    private Vector2 lastUV;
    private Vector2[] TemplateShape;
    private Vector2 bottomLeftCorner = new Vector2(72, 1092);
    private Vector2 topRightCorner = new Vector2(1844, 1972);
    public VRTK_Senello_TexturePainter Paint;
    private Color chalkColor;

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

    void Start()
    {
        chalkColor = GameController.Instance.ChalkColor;
        TemplateShape = GameController.Instance.TemplateShape;
        Renderer rend = gameObject.GetComponent<Renderer>();
        rend.material.mainTexture = drawingTexture;

        for (int i = 0; i < TemplateShape.GetLength(0) - 1; i++)
        {
            Paint.DrawLine(TemplateShape[i], TemplateShape[i + 1], templateThickness, Color.white);
        }
        if (TemplateShape.GetLength(0) >= 2)
        {
            Paint.DrawPoint(new Vector3(TemplateShape[0].x, TemplateShape[0].y, 0), templateThickness, Color.green);
        }
    }

    void Update()
    {
        if (GameController.Instance.RysObject == null) return;

        RaycastHit hit = GameController.Instance.RysObject.GetComponent<VRTK.VRTK_Pointer>().pointerRenderer
            .GetDestinationHit();
        if (hit.transform == null)
        {
            interpolate = false;
            return;
        }

        Renderer rend = hit.transform.GetComponent<Renderer>();

        if (rend == null || rend.sharedMaterial == null || rend.sharedMaterial.mainTexture == null ||
            rend != gameObject.GetComponent<Renderer>())
        {
            interpolate = false;
            return;
        }

        TemplateShape = GameController.Instance.TemplateShape;

        /*if (!interpolate)
        {
            ClearBoard();
        }*/

        Vector2 pixelUV = hit.textureCoord;
        int lineStart = currentTarget == 0 ? 0 : currentTarget - 1;
        bool mistake = true;

        //Check against the template only if it exists
        if (TemplateShape.GetLength(0) > 1)
        {
            mistake = false;
            if (DistanceFromLine(TemplateShape[lineStart], TemplateShape[currentTarget], pixelUV) >
                templateThickness / 2)
            {
                //Player made a mistake
                comboCounter = 0;
                mistake = true;
                Debug.Log("A mistake! Combo: " + comboCounter);
            }

            if (DistanceFromLine(TemplateShape[currentTarget], TemplateShape[currentTarget], pixelUV) <=
                templateThickness / 2)
            {
                comboCounter++;
                //Player reached a checkpoint
                if (currentTarget == TemplateShape.GetLength(0) - 1)
                {
                    Debug.Log("You finished the shape! Combo: " + comboCounter);
                    gameInProgress = false;
                }
                else
                {
                    Debug.Log("Good job! Combo: " + comboCounter);
                    currentTarget++;
                    Paint.DrawLine(TemplateShape[currentTarget], TemplateShape[currentTarget], templateThickness,
                        Color.green);
                }
            }
        }


        //draw a line between the 2 last known cursor positions so that the drawing is continous
        if (interpolate)
        {
            
            if (mistake) GameController.Instance.ChalkColor = Color.grey;
            else GameController.Instance.ChalkColor = chalkColor;
        }

        interpolate = true;
        lastUV = pixelUV;
    }
}