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
    private List<Vector2> Points = new List<Vector2>();
    public VRTK_Senello_TexturePainter Paint;
    private Color chalkColor;
    private bool first = false;
    private float lastWrong;

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
        first = true;
        lastWrong = Time.time;
        currentTarget = 0;
        comboCounter = 0;
        templateThickness = GameController.Instance.DrawingManager.templateThickness;
        pointThickness = GameController.Instance.DrawingManager.pointThickness;
        templateThicknessAccept = GameController.Instance.DrawingManager.templateThicknessAccept;
        pointThicknessAccept = GameController.Instance.DrawingManager.pointThicknessAccept;
        chalkColor = GameController.Instance.DrawingManager.CurrentChalkColor;
        TemplateShape = GameController.Instance.DrawingManager.TemplateShape;

        Points.Clear();

        for (int i = 0; i < TemplateShape.GetLength(0); i++)
        {
            Points.Add(Paint.DrawPoint(new Vector3(TemplateShape[i].x, TemplateShape[i].y, 0.02f), pointThickness,
                GameController.Instance.DrawingManager.DoNotGoToColor));
        }
        for (int i = 0; i < Points.Count - 1; i++)
        {
            Paint.DrawLine(new Vector3(Points[i].x, Points[i].y, 3f), new Vector3(Points[i + 1].x, Points[i + 1].y, 3f),
                templateThickness, GameController.Instance.DrawingManager.ShapeColor);
        }

        if (TemplateShape.GetLength(0) >= 2)
        {
            Paint.DrawPoint(new Vector3(TemplateShape[0].x, TemplateShape[0].y, 0.01f), pointThickness,
                GameController.Instance.DrawingManager.GoToColor);
        }
    }

    void Update()
    {
        if (!gameInProgress) return;
        if (GameController.Instance.DrawingManager.RysObject == null) return;

        Vector2 pixelUV = Paint.GetLastPoint();

        if (Paint.GetWasZero() && !first && currentTarget != 0)
        {
            first = true;
            lastWrong = Time.time;
            comboCounter = 0;
            Debug.Log("A mistake! Combo: " + comboCounter);
            GameController.Instance.Particles.CreateParticle(Particles.NaszeParticle.Small_Wrong,
                GameController.Instance.DrawingManager.RysObject.transform.position + Vector3.right / 10);
            GameController.Instance.SoundManager.Play3DAt(SamplesList.Error,
                GameController.Instance.DrawingManager.RysObject.transform.position, 0.01f);
        }

        RaycastHit hit = GameController.Instance.DrawingManager.RysObject.GetComponent<VRTK.VRTK_Pointer>()
            .pointerRenderer
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

        bool mistake = false;

        //Check against the template only if it exists
        if (TemplateShape.GetLength(0) > 1)
        {
            if (currentTarget != 0 && DistanceFromLine(
                    Points[currentTarget - 1], Points[currentTarget], pixelUV) >
                templateThicknessAccept)
            {
                //Player made a mistake
                comboCounter = 0;
                mistake = true;
                Debug.Log("A mistake! Combo: " + comboCounter);
                if (Time.time > lastWrong + 1)
                {
                    lastWrong = Time.time;
                    GameController.Instance.Particles.CreateParticle(Particles.NaszeParticle.Small_Wrong,
                        GameController.Instance.DrawingManager.RysObject.transform.position + Vector3.right / 10);
                    GameController.Instance.SoundManager.Play3DAt(SamplesList.Error,
                        GameController.Instance.DrawingManager.RysObject.transform.position, 0.01f);
                }
            }

            if (DistanceFromLine(Points[currentTarget], Points[currentTarget], pixelUV) <=
                pointThicknessAccept)
            {
                comboCounter++;
                //Player reached a checkpoint
                GameController.Instance.ScoreBoard.PointsAddAnim(50 * comboCounter);

                if (comboCounter == 1)
                    GameController.Instance.Particles.CreateParticle(Particles.NaszeParticle.FiftyPoints,
                        GameController.Instance.DrawingManager.RysObject.transform.position + Vector3.right / 10);
                else
                    GameController.Instance.Particles.CreateParticle(Particles.NaszeParticle.Small_Good_Correct_Ok,
                        GameController.Instance.DrawingManager.RysObject.transform.position + Vector3.right / 10);

                if (currentTarget == TemplateShape.GetLength(0) - 1)
                {
                    Debug.Log("You finished the shape! Combo: " + comboCounter);
                    //GameController.Instance.MessageSystem.SetProgressBar(100);
                    gameInProgress = false;
                }
                else
                {
                    Debug.Log("Good job! Combo: " + comboCounter);

                    first = false;

                    if (currentTarget > 0)
                        Paint.DrawLine(new Vector3(Points[currentTarget - 1].x, Points[currentTarget - 1].y, 2.99f),
                            new Vector3(Points[currentTarget].x, Points[currentTarget].y, 2.99f),
                            templateThickness, GameController.Instance.DrawingManager.CompleteShapeColor);
                    Paint.DrawPoint(new Vector3(TemplateShape[currentTarget].x, TemplateShape[currentTarget].y, 0f),
                        pointThickness,
                        GameController.Instance.DrawingManager.CompleteShapeColor);

                    currentTarget++;

                    Paint.DrawPoint(new Vector3(TemplateShape[currentTarget].x, TemplateShape[currentTarget].y, 0.01f),
                        pointThickness,
                        GameController.Instance.DrawingManager.GoToColor);
                    //GameController.Instance.MessageSystem.SetProgressBar((float) currentTarget / TemplateShape.GetLength(0) * 100);
                }
            }
        }

        if (mistake || currentTarget == 0 || !gameInProgress)
            GameController.Instance.DrawingManager.CurrentChalkColor = chalkColor;
        else
            GameController.Instance.DrawingManager.CurrentChalkColor =
                GameController.Instance.DrawingManager.OnLineChalkColor;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Sponge" && !enabled)
        {
            GameController.Instance.SoundManager.Play2D(SamplesList.ShortPoof, 0.1f);
            Paint.Clear();
        }
    }
}