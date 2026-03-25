using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private float gravity = -9.81f;

    private CharacterController controller;
    private Transform cam;

    private float verticalVelocity;
    private float xRotation = 0f;

    // NEW: this stores mouse delta from the Input System
    private Vector2 lookInput;

    // NEW: Input System callback
    public void OnLook(InputAction.CallbackContext ctx)
    {
        lookInput = ctx.ReadValue<Vector2>();
    }

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        cam = Camera.main.transform;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void Update()
    {
        HandleLook();
        HandleMovement();
    }

    private void HandleLook()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Vertical rotation (camera only)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);
        cam.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Horizontal rotation (player body)
        transform.Rotate(Vector3.up * mouseX);
    }



    private void HandleMovement()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 move = (transform.right * h + transform.forward * v).normalized;

        if (controller.isGrounded && verticalVelocity < 0)
            verticalVelocity = -2f;

        verticalVelocity += gravity * Time.deltaTime;

        Vector3 velocity = move * moveSpeed + Vector3.up * verticalVelocity;

        controller.Move(velocity * Time.deltaTime);
    }
}



