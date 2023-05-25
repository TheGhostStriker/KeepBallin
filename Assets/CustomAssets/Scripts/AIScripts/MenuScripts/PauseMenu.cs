using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public Button pauseButton;
    public Button resumeButton;
    public Button mainMenuButton;

    private bool isPaused = false;
    private float previousTimeScale;

    private void Start()
    {
        // Add button click listeners
        pauseButton.onClick.AddListener(PauseGame);
        resumeButton.onClick.AddListener(ResumeGame);
        mainMenuButton.onClick.AddListener(ReturnToMainMenu);

        // Hide the pause menu UI initially
        pauseMenuUI.SetActive(false);
    }

    private void Update()
    {
        // Listen for touch input to toggle pause
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                if (isPaused)
                    ResumeGame();
                else
                    PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        if (isPaused)
            return;

        isPaused = true;
        previousTimeScale = Time.timeScale;
        Time.timeScale = 0f;  // Pause the game
        pauseMenuUI.SetActive(true);
    }

    public void ResumeGame()
    {
        if (!isPaused)
            return;

        isPaused = false;
        Time.timeScale = previousTimeScale;  // Resume the game
        pauseMenuUI.SetActive(false);
    }

    public void ReturnToMainMenu()
    {
        // Add code here to handle returning to the main menu screen
        // For example, you can load a new scene using SceneManager.LoadScene()
        SceneManager.LoadScene("MainMenu");
        // Resume the game to prevent any lingering pause effects
        ResumeGame();
    }
}

