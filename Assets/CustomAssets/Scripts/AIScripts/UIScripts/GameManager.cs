using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

   


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
    public GameObject YouWinCanvas;
    public GameObject loungeAmbience;
    public GameObject labAmbience;
    public GameObject theatreAmbience;
    

    private float startTime;
    private float survivalTime;
    [SerializeField]private float bestTime;
    
    private bool isGameOver = false;

    public Animator doorAnim;
    public Animator sciDoorAnim;
    public Animator theatreDoorAnim;

    public AudioSource ohLook;
    public AudioClip ohLooky;

    public AudioClip sciFiDoor;
    public AudioClip sciFiDoor2;
    public AudioClip doorOpen;
    public AudioSource selfDestruct;
    public AudioSource selfDestruct2;
    public AudioSource selfDestruct3;
    public AudioSource cheatingYouMust;
    public AudioSource cantFindCheats;
    public AudioSource popcornNotEnough;
    public AudioSource annoyingMe;
    public AudioSource finalStageFine;
    public AudioSource cantBelieveThis;
    public AudioSource sciFiDoorOpen2;
    public AudioClip selfDestructClip;
    public AudioClip selfDestructClip2;
    public AudioClip selfDestructClip3;
    public AudioClip cheatingYouMustClip;
    public AudioClip cantFindCheatsClip;
    public AudioClip popcornNotEnoughClip;
    public AudioClip annoyingMeClip;
    public AudioClip finalStageFineClip;
    public AudioClip cantBelieveThisClip;


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

        if (survivalTime >= 25f && ohLook != null)
        {
            
            if(!ohLook.isPlaying)
            {
                ohLook.PlayOneShot(ohLooky);
                Destroy(ohLook, 6f);
            }
            
            doorAnim.SetTrigger("OpenDoor");

        }
        if(survivalTime >=25f)
        {
            
            secondLevel.SetActive(true);
        }

        if(survivalTime >= 35f && selfDestruct != null)
        {
            if(!selfDestruct.isPlaying)
            {
                selfDestruct.PlayOneShot(selfDestructClip);
                Destroy(selfDestruct, 5f);
            }
            
        }

        if(survivalTime >= 50f)
        {
            firstLevel.SetActive(false);
            loungeAmbience.SetActive(false);
            labAmbience.SetActive(true);
        }

        if(survivalTime >= 70f && annoyingMe != null) // 120
        {
            if(!annoyingMe.isPlaying)
            {
                annoyingMe.PlayOneShot(annoyingMeClip);
                Destroy(annoyingMe, 5f);
            }
            
        }

        if (survivalTime >= 95f && cheatingYouMust != null) //150
        {
            if(!cheatingYouMust.isPlaying)
            {
                cheatingYouMust.PlayOneShot(cheatingYouMustClip);
                Destroy(cheatingYouMust, 6f);
            }
            
        }

        if (survivalTime >= 105f && cantFindCheats != null)
        {
            if(!cantFindCheats.isPlaying)
            {
                cantFindCheats.PlayOneShot(cantFindCheatsClip);
                Destroy(cantFindCheats, 7f);
            }
            thirdLevel.SetActive(true);
        }

       
        if (survivalTime >= 120f && selfDestruct2 != null) //120
        {
            if(!selfDestruct2.isPlaying)
            {
                selfDestruct2.PlayOneShot(selfDestructClip2);
                Destroy(selfDestruct2, 5f);
            }

            
            if(!sciFiDoorOpen2.isPlaying)
            {
                sciFiDoorOpen2.PlayOneShot(sciFiDoor2);
                Destroy(sciFiDoor2, 1f);
            }
             
            

            sciDoorAnim.SetTrigger("OpenSciDoor");
            
            doorBlocker1.SetActive(false);
        }

        
        if(survivalTime >= 135f)
        {
            secondLevel.SetActive(false);
            labAmbience.SetActive(false);
            theatreAmbience.SetActive(true);
            sciDoorAnim.SetTrigger("CloseSciDoor");
        }

        

        if (survivalTime >= 170f && popcornNotEnough != null) //240
        {
            if(!popcornNotEnough.isPlaying)
            {
                popcornNotEnough.PlayOneShot(popcornNotEnoughClip);
                Destroy(popcornNotEnough, 8f);
            }
            
        }


        if (survivalTime >= 195f && finalStageFine != null) // 265
        {
            if(!finalStageFine.isPlaying)
            {
                finalStageFine.PlayOneShot(finalStageFineClip);
                
                Destroy(finalStageFine, 10f);
            }
            fourthLevel.SetActive(true);
            

        }

        if(survivalTime >= 210f && selfDestruct3 != null)
        {
            if (!selfDestruct3.isPlaying)
            {
                selfDestruct3.PlayOneShot(selfDestructClip3);
                Destroy(selfDestruct3, 5f);
            }
            theatreAmbience.SetActive(false);
            theatreDoorAnim.SetTrigger("FinalOpen");
            GetComponent<AudioSource>().clip = doorOpen;
            GetComponent<AudioSource>().Play();
        }
        if(survivalTime >= 225f)
        {
            thirdLevel.SetActive(false);
        }

        if(survivalTime >= 350f && cantBelieveThis != null)
        {
            if(!cantBelieveThis.isPlaying)
            {
                cantBelieveThis.PlayOneShot(cantBelieveThisClip);
                Destroy(cantBelieveThis, 7f);
            }
            
            Invoke("YouWinScreen", 7f);
        }

        //DEV CONTROLS - ALSO INCLUDES F AND G TO OPEN AND CLOSE FIRST LEVEL DOOR. P OPENS SECOND LEVEL DOORS
      
        if(Input.GetKeyDown(KeyCode.P))
        {
            sciDoorAnim.SetTrigger("OpenSciDoor");
            doorBlocker1.SetActive(false);
        }

        if(Input.GetKeyDown(KeyCode.L))
        {
            theatreDoorAnim.SetTrigger("FinalOpen");
        }
        if(Input.GetKeyDown(KeyCode.X))
        {
            sciDoorAnim.SetTrigger("CloseSciDoor");
        }
        
    }

    public void YouWinScreen()
    {
        YouWinCanvas.SetActive(true);

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
        SceneManager.LoadScene("MainMenu");
    }
}




