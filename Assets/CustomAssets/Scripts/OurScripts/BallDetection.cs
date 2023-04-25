using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDetection : MonoBehaviour
{
    public GameObject gameOverCanvas;
    public GameObject timerText;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Ball"))
        {

            Time.timeScale = 0f;


            gameOverCanvas.SetActive(true);
            timerText.SetActive(false);
        }
    }
}
