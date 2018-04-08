using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rysowanie : MonoBehaviour {
	public int brushSize;
    //Bottom left corner is 18 x 273 Top right is 461 x 493
    public Vector2[] TemplateShape;
    bool interpolate = false;
    bool gameInProgress = true;
    int currentTarget = 0;
    int comboCounter = 0;
    int templateThickness = 8;
    Texture2D drawingTexture;
    Vector2 lastUV;

    float DistanceFromLine(Vector2 start, Vector2 end, Vector2 point) {
        float dist = Vector2.Distance(start, end);
        if (dist == 0) {
            return Vector2.Distance(start, point);
        } else {
            float t = Mathf.Clamp01(Vector2.Dot(point - start, end - start) / (dist * dist));
            Vector2 projection = start + Vector2.Scale((end - start), new Vector2(t, t));
            return Vector2.Distance(projection, point);
        }
    }

    void DrawLine(Vector2 start, Vector2 end, int thickness, Color color) {
        for (int i = (int)Mathf.Min(start.x, end.x) - thickness / 2; i <= Mathf.Max(start.x, end.x) + thickness / 2; i++) {
            for (int j = (int)Mathf.Min(start.y, end.y) - thickness / 2; j <= Mathf.Max(start.y, end.y) + thickness / 2; j++) {
                Vector2 point = new Vector2(i, j);
                if (DistanceFromLine(start, end, point) <= thickness / 2) {
                    drawingTexture.SetPixel(i, j, color);
                }
            }
        }
        drawingTexture.Apply();
    }

    void ClearBoard() {
        for(int i = 18; i <= 461; i++) {
            for(int j = 273; j <= 493; j++) {
                drawingTexture.SetPixel(i, j, Color.white);
            }
        }

        for (int i = 0; i < TemplateShape.GetLength(0) - 1; i++) {
            DrawLine(TemplateShape[i], TemplateShape[i + 1], templateThickness, Color.gray);
        }

        if (TemplateShape.GetLength(0) >= 2) {
            DrawLine(TemplateShape[0], TemplateShape[0], templateThickness, Color.green);
        }
        currentTarget = 0;
        comboCounter = 0;
        gameInProgress = true;
    }
    
    void Start ()
    {
        Renderer rend = gameObject.GetComponent<Renderer>();
		Texture2D original = rend.material.mainTexture as Texture2D;
		drawingTexture = new Texture2D (original.width, original.height, original.format, true);
		Graphics.CopyTexture (original, drawingTexture);
		rend.material.mainTexture = drawingTexture;

        for (int i = 0; i < TemplateShape.GetLength(0) - 1; i++) {
            DrawLine(TemplateShape[i], TemplateShape[i + 1], templateThickness, Color.gray);
        }
        if (TemplateShape.GetLength(0) >= 2) {
            DrawLine(TemplateShape[0], TemplateShape[0], templateThickness, Color.green);
        }
    }

	void Update ()
    {
        if (GameController.Instance.RysObject == null) return;

        RaycastHit hit = GameController.Instance.RysObject.GetComponent<VRTK.VRTK_Pointer>().pointerRenderer.GetDestinationHit();
        if (hit.transform == null)
        {
            interpolate = false;
            return;
        }

        Renderer rend = hit.transform.GetComponent<Renderer>();

        if (rend == null || rend.sharedMaterial == null || rend.sharedMaterial.mainTexture == null || rend != gameObject.GetComponent<Renderer>())
        {
            interpolate = false;
            return;
        }

        if (!interpolate)
        {
            ClearBoard();
        }

        if (!gameInProgress)
        {
            return;
        }

        Texture2D tex = hit.transform.GetComponent<Renderer>().material.mainTexture as Texture2D;
        Vector2 pixelUV = hit.textureCoord;
        pixelUV.x *= tex.width;
        pixelUV.y *= tex.height;

        int lineStart = currentTarget == 0 ? 0 : currentTarget - 1;
        bool mistake = false;

        //Check against the template only if it exists
        if (TemplateShape.GetLength(0) > 1)
        {
            if (DistanceFromLine(TemplateShape[lineStart], TemplateShape[currentTarget], pixelUV) > templateThickness / 2)
            {
                //Player made a mistake
                comboCounter = 0;
                mistake = true;
                Debug.Log("A mistake! Combo: " + comboCounter);
            }

            if (DistanceFromLine(TemplateShape[currentTarget], TemplateShape[currentTarget], pixelUV) <= templateThickness / 2)
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
                    DrawLine(TemplateShape[currentTarget], TemplateShape[currentTarget], templateThickness, Color.green);
                }
            }
        }


        //draw a line between the 2 last known cursor positions so that the drawing is continous
        if (interpolate)
        {
            DrawLine(pixelUV, lastUV, brushSize, mistake ? Color.red : Color.black);
        }

        interpolate = true;
        lastUV = pixelUV;
    }
}
