using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rysowanie : MonoBehaviour {
	public int brushSize;
	public GameObject rightHand;

	// Use this for initialization
	void Start () {
        Renderer rend = gameObject.GetComponent<Renderer>();
		Texture2D original = rend.material.mainTexture as Texture2D;
		Texture2D copy = new Texture2D (original.width, original.height, original.format, true);
		Graphics.CopyTexture (original, copy);
		rend.material.mainTexture = copy;
	}
	
	// Update is called once per frame
	void Update () {
		//if (!Input.GetMouseButton(0))
		//	return;

		RaycastHit hit;

		VRTK.VRTK_Pointer pointer = rightHand.GetComponent<VRTK.VRTK_Pointer>();

		hit = pointer.pointerRenderer.GetDestinationHit();

		if (!pointer.IsActivationButtonPressed())
			return;

        //if (!Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit))
        //	return;

        if (hit.transform == null)
            return;

        Renderer rend = hit.transform.GetComponent<Renderer>();

        //MeshCollider meshCollider = hit.collider as MeshCollider;

        if (rend == null || rend.sharedMaterial == null || rend.sharedMaterial.mainTexture == null /*|| meshCollider == null*/)
			return;

		if (rend != gameObject.GetComponent<Renderer>())
			return;

		Texture2D tex = hit.transform.GetComponent<Renderer>().material.mainTexture as Texture2D;
		Vector2 pixelUV = hit.textureCoord;
		pixelUV.x *= tex.width;
		pixelUV.y *= tex.height;

		pixelUV.x -= brushSize / 2.0f;
		pixelUV.y -= brushSize / 2.0f;
		for (int i = 0; i < brushSize; i++) {
			for (int j = 0; j < brushSize; j++) {
				tex.SetPixel ((int)pixelUV.x + i, (int)pixelUV.y + j, Color.black);
			}
		}
		
		tex.Apply();
	}
}
