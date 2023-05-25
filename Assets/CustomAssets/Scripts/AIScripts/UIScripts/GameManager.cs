using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    //TODO: Move triggers to timers
    //TODO: Create a detonation sequence with funny voice lines haha
    //TODO: Fuck with player :)


    public Text timeText;
    public Text bestTimeText;
   
    public GameObject gameOverCanvas;
    public GameObject doorBlocker1;
    public GameObject secondLevel;
    public GameObject thirdLevel;
    public GameObject firstLevel;
    public GameObject fourthLevel;
    public GameObject pauseButton;
    public GameObject moveJoystick;
    public GameObject cameraJoystick;
    public GameObject dashButton;
    public GameObject stopButton;

    private float startTime;
    private float survivalTime;
    [SerializeField]private float bestTime;
    
    private bool isGameOver = false;

    public Animator doorAnim;
    public Animator sciDoorAnim;

    public AudioSource ohLook;
    public AudioClip ohLooky;


    void Start()
    {
        startTime = Time.time;
        bestTime = PlayerPrefs.GetFloat("BestTime", Mathf.Infinity);
        
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
            if (survivalTime > bestTime)
            {
                bestTime = survivalTime;
                PlayerPrefs.SetFloat("BestTime", bestTime);
            }
            bestTimeText.text = "Best Time: " + Mathf.Floor(bestTime).ToString() + "s";

            
        }

        if (survivalTime >= 50f && ohLook != null)
        {
            
            if(!ohLook.isPlaying)
            {
                ohLook.PlayOneShot(ohLooky);
                Destroy(ohLook, 6f);
            }
            //Animator animator = GetComponent<Animator>();
            doorAnim.SetTrigger("OpenDoor");

        }
        if(survivalTime >= 45f)
        {
            secondLevel.SetActive(true);
        }

        if(survivalTime >= 195f)
        {
            thirdLevel.SetActive(true);
        }
        if(survivalTime >= 200f)
        {
            sciDoorAnim.SetTrigger("OpenSciDoor");
            doorBlocker1.SetActive(false);
        }
        //TODO: Add FINAL Stage TRIGGER HERE

        if(survivalTime >= 255f)
        {
            fourthLevel.SetActive(true);
        }

        if(survivalTime >= 260f)
        {
            //ThirdDoorAnimHereOniChaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaan
        }

        //DEV CONTROLS - ALSO INCLUDES F AND G TO OPEN AND CLOSE FIRST LEVEL DOOR. P OPENS SECOND LEVEL DOORS
        //TODO: ADD THIRD DOOR CHEATCODE UWU
        if(Input.GetKeyDown(KeyCode.P))
        {
            sciDoorAnim.SetTrigger("OpenSciDoor");
            doorBlocker1.SetActive(false);
        }

        if(Input.GetKeyDown(KeyCode.L))
        {
            //ThirdDoorAnimHereSenpai.
        }
        
    }

    public void GameOver()
    {
        isGameOver = true;
        Time.timeScale = 0f;
        gameOverCanvas.SetActive(true);
        pauseButton.SetActive(false);
        moveJoystick.SetActive(false);
        cameraJoystick.SetActive(false);
        stopButton.SetActive(false);
        dashButton.SetActive(false);

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




