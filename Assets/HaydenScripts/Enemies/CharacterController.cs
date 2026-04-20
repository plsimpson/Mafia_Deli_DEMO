using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float mouseSensitivity = 0.1f;

    public Transform cameraTransform;
    public CharacterController cc;

    private Vector2 moveInput;
    private Vector2 lookInput;

    private float xRotation = 0f;

    // Jump and Gravity Variables
    public float jumpHeight = 2f; // Height of the jump
    public float gravity = -9.81f; // Gravity force
    private float verticalVelocity = 0f; // Vertical velocity for gravity and jumping
    private bool isGrounded; // Check if the player is grounded

    // INPUT CALLBACKS
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded)
        {
            verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity); // Calculate jump velocity
        }
    }

    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    void Update()
    {
        HandleLook();
        HandleMovement();
    }

    void HandleLook()
    {
        float mouseX = lookInput.x * mouseSensitivity;
        float mouseY = lookInput.y * mouseSensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    void HandleMovement()
    {
        // Check if the player is grounded
        isGrounded = cc.isGrounded;

        if (isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f; // Reset vertical velocity when grounded
        }

        // Calculate movement
        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;

        // Apply gravity
        verticalVelocity += gravity * Time.deltaTime;

        // Apply movement and vertical velocity
        Vector3 velocity = move * speed + Vector3.up * verticalVelocity;
        cc.Move(velocity * Time.deltaTime);
    }
}