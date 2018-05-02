using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawingManager : MonoBehaviour
{
    public Vector2[] TemplateShape;
    public GameObject RysObject;
    public Color CurrentChalkColor;
    public Color OnLineChalkColor;
    public Color GoToColor;
    public Color DoNotGoToColor;
    public Color ShapeColor;
    public Color CompleteShapeColor;
    public Rysowanie[] Boards;
    public float templateThickness;
    public float pointThickness;
    public float templateThicknessAccept;
    public float pointThicknessAccept;
}