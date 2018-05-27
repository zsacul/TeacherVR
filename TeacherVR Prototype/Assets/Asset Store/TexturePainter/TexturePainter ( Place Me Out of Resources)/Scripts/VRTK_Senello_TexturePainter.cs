/// <summary>
/// CodeArtist.mx 2015
/// This is the main class of the project, its in charge of raycasting to a model and place brush prefabs infront of the canvas camera.
/// If you are interested in saving the painted texture you can use the method at the end and should save it to a file.
/// </summary>


using System.Collections;
using UnityEngine;
using VRTK;

public class VRTK_Senello_TexturePainter : MonoBehaviour
{
    private VRTK_Pointer chalkPointer;
    private VRTK_Pointer spongePointer;

    public GameObject Board;
    public GameObject brushContainer; //The cursor that overlaps the model and our container for the brushes painted

    public Camera canvasCam; //The camera that looks at the model, and the camera that looks at the canvas.
    public Sprite ChalkSprite; // Cursor for the differen functions
    public RenderTexture canvasTexture; // Render Texture that looks at our Base Texture and the painted brushes
    public Material ChalkLieneMaterial;

    public float brushSize = 0.1f; //The size of our brush

    Color brushColor; //The selected color
    private Vector3 lastPoint;
    private bool wasZero = false;
    GameObject brushObj;

    public Vector3 GetLastPoint()
    {
        return lastPoint;
    }

    public bool GetWasZero()
    {
        bool tmp = wasZero;
        wasZero = false;
        return tmp;
    }

    void Update()
    {
        if (GameController.Instance.DrawingManager.RysObject != null)
            chalkPointer = GameController.Instance.DrawingManager.RysObject.GetComponent<VRTK_Pointer>();
        else if (GameController.Instance.DrawingManager.SpongeObject != null)
            spongePointer = GameController.Instance.DrawingManager.SpongeObject.GetComponent<VRTK_Pointer>();
        else return;
        brushColor = GameController.Instance.DrawingManager.CurrentChalkColor;
        if (chalkPointer != null && chalkPointer.enabled)
        {
            DoAction();
        }
        if (spongePointer != null && spongePointer.enabled)
        {
            DoSpongeAction();
        }
    }

    public void DrawLine(Vector3 start, Vector3 end, float thickness, Color col)
    {
        GameObject line = new GameObject();
        LineRenderer lineRenderer = line.AddComponent<LineRenderer>();
        lineRenderer.material = ChalkLieneMaterial;
        lineRenderer.material.color = col;
        lineRenderer.material.SetColor("_EmissionColor", col);
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);
        lineRenderer.widthMultiplier = thickness / 2;

        line.transform.parent = brushContainer.transform;
        line.transform.position = start;

        StartCoroutine(ClearLast(line));
    }

    public Vector3 DrawPoint(Vector3 uvWorldPosition, float size, Color color)
    {
        //brushColor.a = brushSize * 2.0f; // Brushes have alpha to have a merging effect when painted over.
        brushObj = (GameObject) Instantiate(Resources.Load("TexturePainter-Instances/BrushEntity")); //Paint a brush
        brushObj.GetComponent<SpriteRenderer>().sprite = ChalkSprite;
        brushObj.GetComponent<SpriteRenderer>().color = color; //Set the brush color

        brushObj.transform.parent = brushContainer.transform; //Add the brush to our container to be wiped later
        brushObj.transform.localPosition = uvWorldPosition; //The position of the brush (in the UVMap)
        brushObj.transform.localScale = Vector3.one * size; //The size of the brush

        StartCoroutine(ClearLast(brushObj));

        return brushObj.transform.position;
    }

    //The main action, instantiates a brush or decal entity at the clicked position on the UV map

    void DoAction()
    {
        Vector3 uvWorldPosition = Vector3.zero;
        if (HitTestUVPosition(ref uvWorldPosition))
        {
            DrawPoint(uvWorldPosition, brushSize, brushColor);
            if (Vector3.Distance(lastPoint, brushObj.transform.position) < brushSize / 4)
            {
                Destroy(brushObj);
                return;
            }
            if (lastPoint != Vector3.zero)
            {
                if (Vector3.Distance(lastPoint, brushObj.transform.position) > brushSize / 2)
                {
                    DrawLine(lastPoint, brushObj.transform.position, brushSize, brushColor);
                }
            }
            lastPoint = brushObj.transform.position;
        }
    }

    void DoSpongeAction()
    {
        Vector3 uvWorldPosition = Vector3.zero;
        if (HitTestUVPosition(ref uvWorldPosition, true))
        {
            DrawPoint(uvWorldPosition, brushSize * 5, Color.black);
        }
    }


    //Returns the position on the texuremap according to a hit in the mesh collider
    bool HitTestUVPosition(ref Vector3 uvWorldPosition, bool sponge = false)
    {
        RaycastHit hit;
        if (!sponge)
        {
            if (GameController.Instance.DrawingManager.RysObject == null) return false;
            hit = GameController.Instance.DrawingManager.RysObject.GetComponent<VRTK_Pointer>()
                .pointerRenderer
                .GetDestinationHit();
        }
        else
        {
            if (GameController.Instance.DrawingManager.SpongeObject == null) return false;
            hit = GameController.Instance.DrawingManager.SpongeObject.GetComponent<VRTK_Pointer>()
                .pointerRenderer
                .GetDestinationHit();
        }

        if (hit.transform != null)
        {
            Renderer rend = hit.transform.GetComponent<Renderer>();

            if (rend == null || rend != Board.GetComponent<Renderer>())
            {
                return false;
            }

            MeshCollider meshCollider = hit.collider as MeshCollider;
            if (meshCollider == null || meshCollider.sharedMesh == null)
                return false;
            if (sponge && meshCollider.gameObject.GetComponent<Rysowanie>().enabled)
                return false;

            Vector2 pixelUV = new Vector2(hit.textureCoord.x, hit.textureCoord.y);
            uvWorldPosition.x = pixelUV.x; // - canvasCam.orthographicSize; //To center the UV on X
            uvWorldPosition.y = pixelUV.y; // - canvasCam.orthographicSize; //To center the UV on Y
            uvWorldPosition.z = 0.0f;
            return true;
        }

        if (!sponge)
        {
            lastPoint = Vector3.zero;
            wasZero = true;
        }

        return false;
    }

    private IEnumerator ClearLast(GameObject del)
    {
        yield return new WaitForEndOfFrame();
        Destroy(del);
    }

    public void TotalClear()
    {
        StartCoroutine(CanvasCameraClearFlags());
    }

    private IEnumerator CanvasCameraClearFlags()
    {
        canvasCam.clearFlags = CameraClearFlags.SolidColor;
        yield return new WaitForEndOfFrame();
        canvasCam.clearFlags = CameraClearFlags.Nothing;
    }
}