using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] InputActionAsset inputActions;
    InputAction pauseAction;

    public static bool isPaused = false;
    public GameObject PauseMenuCanvas;

    void OnEnable()
    {
        pauseAction?.Enable();
    }

    void OnDisable()
    {
        pauseAction?.Disable();
    }

    void Awake()
    {
        pauseAction = inputActions.FindAction("Pause");
        pauseAction.performed += ctx => TogglePause();
    }

    void TogglePause()
    {
        if (PauseMenuCanvas != null)

        if (isPaused)
        {
            Resume();
        }
        else
        {
            Stop();
        }
    }
    
    public void Resume()
    {
        PauseMenuCanvas?.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    void Stop()
    {
        PauseMenuCanvas?.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void MainMenuButton()
    {
        Time.timeScale = 1f;
        GameManager.Instance.playerData.Fuel = 200;
        GameManager.Instance.playerData.Energy = 0;
        GameManager.Instance.playerData.Lives = 3;
        PauseMenuCanvas.SetActive(false);
        SceneManager.LoadScene(0);
    }
}

