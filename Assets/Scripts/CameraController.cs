using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour {
    
    // The target this camera should follow (likely the player).
    public GameObject target;
    // Approximately the time it will take to reach the target.
    public float smoothTime = 1f;

    // The camera component on this game object.
    Camera cam;
    // The camera velocity, used for damping.
    Vector3 camVelocity;

    private void Start () {
        // Fetches camera component.
        cam = GetComponent<Camera> ();
    }

    private void FixedUpdate () {
        // Get the position of the target.
        Vector3 targetPos = target.transform.position;
        // Get the position of this object.
        Vector3 camPos = transform.position;
        // The camera should only change its x and y position.
        // Therefore the z position of the target should be equal to the camera's.
        targetPos.z = camPos.z;

        // Smoothly moves the camera towards the player.
        Vector3 newCamPos = Vector3.SmoothDamp (camPos, targetPos, ref camVelocity, smoothTime);
        // Sets the poition of the camera.
        transform.position = newCamPos;
    }
}
