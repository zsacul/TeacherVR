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

    public GameObject Board;
    public GameObject brushContainer; //The cursor that overlaps the model and our container for the brushes painted

    public Camera canvasCam; //The camera that looks at the model, and the camera that looks at the canvas.
    public Sprite ChalkSprite; // Cursor for the differen functions
    public RenderTexture canvasTexture; // Render Texture that looks at our Base Texture and the painted brushes
    public Material baseMaterial; // The material of our base texture (Were we will save the painted texture)
    private Texture baseTextureBackup;
    public Material ChalkLieneMaterial;

    public float brushSize = 0.1f; //The size of our brush

    Color brushColor; //The selected color
    private int brushCounter = 0;
    const int MAX_BRUSH_COUNT = 500; //To avoid having millions of brushes
    bool saving = false; //Flag to check if we are saving the texture
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

    void Start()
    {
        brushCounter = 0;
        baseMaterial.DisableKeyword("_EMISSION");
        StartCoroutine(SaveTexture());
        StartCoroutine(SaveBasic());
    }

    void Update()
    {
        if (GameController.Instance.DrawingManager.RysObject != null)
            chalkPointer = GameController.Instance.DrawingManager.RysObject.GetComponent<VRTK_Pointer>();
        else return;
        brushColor = GameController.Instance.DrawingManager.CurrentChalkColor;
        if (chalkPointer != null && chalkPointer.enabled)
        {
            DoAction();
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

        return brushObj.transform.position;
    }

    //The main action, instantiates a brush or decal entity at the clicked position on the UV map

    void DoAction()
    {
        if (saving)
        {
            lastPoint = Vector3.zero;
            return;
        }
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
                    Debug.Log("Line");
                    DrawLine(lastPoint, brushObj.transform.position, brushSize, brushColor);
                }
            }
            lastPoint = brushObj.transform.position;
            brushCounter++; //Add to the max brushes
            if (brushCounter >= MAX_BRUSH_COUNT)
            {
                //If we reach the max brushes available, flatten the texture and clear the brushes
                StartCoroutine(SaveTexture());
            }
        }
    }

    //Returns the position on the texuremap according to a hit in the mesh collider
    bool HitTestUVPosition(ref Vector3 uvWorldPosition)
    {
        if (GameController.Instance.DrawingManager.RysObject == null) return false;
        RaycastHit hit = GameController.Instance.DrawingManager.RysObject.GetComponent<VRTK_Pointer>().pointerRenderer
            .GetDestinationHit();

        if (hit.transform != null)
        {
            Renderer rend = hit.transform.GetComponent<Renderer>();

            if (rend == null || rend.sharedMaterial == null || rend.sharedMaterial.mainTexture == null ||
                rend != Board.GetComponent<Renderer>())
            {
                return false;
            }

            MeshCollider meshCollider = hit.collider as MeshCollider;
            if (meshCollider == null || meshCollider.sharedMesh == null)
                return false;
            Vector2 pixelUV = new Vector2(hit.textureCoord.x, hit.textureCoord.y);
            uvWorldPosition.x = pixelUV.x; // - canvasCam.orthographicSize; //To center the UV on X
            uvWorldPosition.y = pixelUV.y; // - canvasCam.orthographicSize; //To center the UV on Y
            uvWorldPosition.z = 0.0f;
            return true;
        }
        lastPoint = Vector3.zero;
        wasZero = true;
        return false;
    }

    public void Clear()
    {
        if (baseTextureBackup != null) baseMaterial.SetTexture("_EmissionMap", baseTextureBackup);
        foreach (GameObject pack in GameController.Instance.DrawingManager.BrushContainers)
        {
            foreach (Transform child in pack.transform)
            {
                Destroy(child.gameObject);
            }
        }
    }

    IEnumerator SaveTexture()
    {
        saving = true;
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        brushCounter = 0;
        RenderTexture.active = canvasTexture;
        Texture2D tex = new Texture2D(canvasTexture.width, canvasTexture.height, TextureFormat.ARGB32, false);
        tex.ReadPixels(new Rect(0, 0, canvasTexture.width, canvasTexture.height), 0, 0);
        tex.Apply();
        RenderTexture.active = null;
        baseMaterial.SetTexture("_EmissionMap", tex); //Put the painted texture as the base
        baseMaterial.EnableKeyword("_EMISSION");
        foreach (Transform child in brushContainer.transform)
        {
            Destroy(child.gameObject);
        }
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        saving = false;
    }

    IEnumerator SaveBasic()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        baseTextureBackup = baseMaterial.GetTexture("_EmissionMap");
    }
}