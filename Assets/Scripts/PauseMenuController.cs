using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    public Canvas pauseMenu;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pauseMenu.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.IsPaused)
            pauseMenu.enabled = true;
        else
            pauseMenu.enabled = false;
    }

    // ===== EXIT =====

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
