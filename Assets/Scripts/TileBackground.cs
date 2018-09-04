using UnityEngine;

/// <summary>
/// Class for tiling a material as background, always filling the camera view.
/// </summary>
[RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
public class TileBackground : MonoBehaviour {

    public Camera cam;
    // How many units in each direction the texture should fill.
    public Vector2 textureSize = Vector2.one;
    // Offset the texture.
    public Vector2 textureOffset = Vector2.zero;

    MeshRenderer meshRenderer;
    MeshFilter meshFilter;

    void Start () {
        // Fetch the mesh renderer and mesh filter
        meshRenderer = GetComponent<MeshRenderer> ();
        meshFilter = GetComponent<MeshFilter> ();
        // Create a quad.
        GameObject quad = GameObject.CreatePrimitive (PrimitiveType.Quad);
        // Assign the quad's mesh to this object's mesh filter.
        meshFilter.mesh = quad.GetComponent<MeshFilter> ().mesh;
        // Destroy the quad.
        Destroy (quad);
    }

    void Update () {
        // Fetch the position, size and aspect of the camera.
        Vector3 camPos = cam.transform.position;
        float camSize = cam.orthographicSize * 2;
        float camAspect = cam.aspect;

        // The size of this object is equal to the sum of the 
        // width and height of the camera's view-rectangle
        // This ensures not even a rotated camera will ever see the edge of the background.
        float size = camSize + camSize * camAspect;

        // Get the material of the mesh renderer.
        Material bgMat = meshRenderer.material;
        // Calculate and assign the offset and scaling.
        // A bigger scale vector results in a smaller texture.
        // Therefore textureSize is divided by itself squared.
        bgMat.SetTextureOffset ("_MainTex", camPos / textureSize + textureOffset);
        bgMat.SetTextureScale ("_MainTex", textureSize / (textureSize * textureSize) * size);

        // Assign new location and scale for this object.
        transform.localScale =
            new Vector3 (size, size, 1f);
        transform.position =
            new Vector3 (camPos.x, camPos.y, transform.position.z);
	}
}
