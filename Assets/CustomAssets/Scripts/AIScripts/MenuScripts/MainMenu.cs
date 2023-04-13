using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour, IPointerDownHandler
{
    // Functionality for the Play button
    public void PlayGame()
    {
        // Load the first level of the game
        SceneManager.LoadScene(1);
    }

    // Functionality for the Options button
    public void OpenOptions()
    {
        // Load the Options scene
        SceneManager.LoadScene("Options");
    }

    // Functionality for the Quit button
    public void QuitGame()
    {
        // Quit the application
        Application.Quit();
    }

    // Handle touch screen input
    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.pointerId == -1)
        {
            // Handle touch screen input for mobile devices
            if (EventSystem.current.currentSelectedGameObject != null)
            {
                ExecuteEvents.Execute(EventSystem.current.currentSelectedGameObject, eventData, ExecuteEvents.pointerClickHandler);
            }
        }
    }
}


