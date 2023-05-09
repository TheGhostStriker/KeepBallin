using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Text timeText;
    public Text bestTimeText;
    public Text personalBestText;
    public GameObject gameOverCanvas;
    public GameObject doorBlocker1;

    private float startTime;
    private float survivalTime;
    private float bestTime;
    private float personalBest;
    private bool isGameOver = false;

    public Animator doorAnim;
    public Animator sciDoorAnim;

    public AudioSource ohLook;
    public AudioClip ohLooky;


    void Start()
    {
        startTime = Time.time;
        bestTime = PlayerPrefs.GetFloat("BestTime", Mathf.Infinity);
        personalBest = PlayerPrefs.GetFloat("PersonalBest", Mathf.Infinity);
        Time.timeScale = 1f;
    }

    void Update()
    {
        if (!isGameOver)
        {
            // Update the survival time
            survivalTime = Time.time - startTime;
            timeText.text = "Survival Time: " + Mathf.Floor(survivalTime).ToString() + "s";

            // Update the best time
            if (survivalTime < bestTime)
            {
                bestTime = survivalTime;
                PlayerPrefs.SetFloat("BestTime", bestTime);
            }
            bestTimeText.text = "Best Time: " + Mathf.Floor(bestTime).ToString() + "s";

            // Update the personal best
            if (survivalTime < personalBest)
            {
                personalBest = survivalTime;
                PlayerPrefs.SetFloat("PersonalBest", personalBest);
            }
            personalBestText.text = "Personal Best: " + Mathf.Floor(personalBest).ToString() + "s";
        }

        if (survivalTime >= 30f && ohLook != null)
        {
            
            if(!ohLook.isPlaying)
            {
                ohLook.PlayOneShot(ohLooky);
                Destroy(ohLook, 6f);
            }
            //Animator animator = GetComponent<Animator>();
            doorAnim.SetTrigger("OpenDoor");

        }

        if(survivalTime >= 90f)
        {
            sciDoorAnim.SetTrigger("OpenSciDoor");
            doorBlocker1.SetActive(false);
        }
    }

    public void GameOver()
    {
        isGameOver = true;
        Time.timeScale = 0f;
        gameOverCanvas.SetActive(true);
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        SceneManager.LoadScene("HomeScreen");
    }
}




