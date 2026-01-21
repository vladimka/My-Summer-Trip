using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    private UIInputActions uiInputActions;
    public Canvas pauseMenu;
    public bool IsPaused { get; private set; }

    void OnEnable()
    {
        uiInputActions = new UIInputActions();
        uiInputActions.UI.Enable();
        uiInputActions.UI.Pause.performed += _ => TogglePause();
    }

    void Start()
    {
        ResumeGame();
    }
    void Update()
    {
        if(IsPaused)
            pauseMenu.enabled = true;
        else
            pauseMenu.enabled = false;
    }

    public void TogglePause()
    {
        Debug.Log($"Pause Toggled: {IsPaused}");
        if (IsPaused)
            ResumeGame();
        else
            PauseGame();
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        IsPaused = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        pauseMenu.enabled = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        IsPaused = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pauseMenu.enabled = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}