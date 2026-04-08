using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Transform cam;

    private void Start()
    {
        // Cache the main camera's transform
        cam = Camera.main.transform;
    }

    // LateUpdate is recommended for camera/transform following
    void LateUpdate()
    {
        // Point the sprite's forward direction towards the camera's position
        transform.LookAt(transform.position + cam.forward, cam.up);
    }
}