using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private CharacterController characterController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveInput = playerInput.actions["Move"].ReadValue<Vector2>();
        Debug.Log("Move input: " + moveInput);

        characterController.Move(new Vector3(moveInput.x, 0, moveInput.y) * Time.deltaTime * 5f);
        if (playerInput.actions["Jump"].triggered)
        {
           //  EnterStation();
        }
    }

}
