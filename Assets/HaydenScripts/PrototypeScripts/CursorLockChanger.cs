using UnityEngine;

public class CursorLockChanger : MonoBehaviour
{
    [SerializeField] private bool lockCursor;

    private void Start()
    {
        Cursor.lockState = lockCursor ? CursorLockMode.Locked : CursorLockMode.None;
    }
}
