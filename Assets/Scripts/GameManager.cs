using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private UIInputActions inputActions;

    public static GameManager Instance { get; private set; }
    public bool IsPaused { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        inputActions = new UIInputActions();
    }

    private void OnEnable(){
        inputActions.UI.Enable();

        inputActions.UI.Pause.performed += ctx => TogglePause();
    }

    // private void OnDisable(){
    //     inputActions.UI.Disable();
    // }

    private void Start()
    {
        ResumeGame();
    }

    // ===== PAUSE =====

    public void TogglePause()
    {
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
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        IsPaused = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // ===== SCENE =====

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadLevel(int index)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(index);
    }

    // ===== EXIT =====

    public void QuitGame()
    {
        Application.Quit();
    }
}