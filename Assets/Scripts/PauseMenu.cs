using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenuUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject pausePanel;

    [Header("Input")]
    [SerializeField] private InputActionAsset inputActions;

    private InputAction pauseAction;
    public static bool IsPaused = false;

    void Awake()
    {
        var map = inputActions.FindActionMap("Player");
        pauseAction = map.FindAction("Pause");
    }

    void OnEnable()
    {
        pauseAction.Enable();
        pauseAction.performed += OnPause;
    }

    void OnDisable()
    {
        pauseAction.performed -= OnPause;
        pauseAction.Disable();
    }

    private void OnPause(InputAction.CallbackContext ctx)
    {
        if (IsPaused)
            Resume();
        else
            Pause();
    }

    public void Resume()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        IsPaused = false;
    }

    public void Pause()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
        IsPaused = true;
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
