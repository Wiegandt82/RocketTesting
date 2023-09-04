using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        // Checks if the GameManager instance exists before accessing it
        GameManager.EnsureInstanceExists();

        GameManager.Instance.playerData.Reset();

        var pauseMenu = FindObjectOfType<PauseMenu>();
        if (pauseMenu != null)
        {
            Destroy(pauseMenu.gameObject);
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Application Closed");
    }
}
