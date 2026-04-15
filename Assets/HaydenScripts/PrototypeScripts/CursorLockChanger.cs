using UnityEngine;

public class CursorLockChanger : MonoBehaviour
{
    [SerializeField] private bool lockCursor;

    private void OnLevelWasLoaded(int level)
    {
        Cursor.lockState = lockCursor ? CursorLockMode.Locked : CursorLockMode.None;
    }
}
