using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    public Text timerText;
    public float startTime = 0f;
    public Animator doorAnim;
    private float currentTime;

    void Start()
    {
        currentTime = startTime;
    }

    void Update()
    {
        currentTime += Time.deltaTime;
        UpdateTimerText();

        if (currentTime >= 15f)
        {
            //Animator animator = GetComponent<Animator>();
            doorAnim.SetTrigger("OpenDoor");
        }
    }

    void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);
        int milliseconds = Mathf.FloorToInt((currentTime * 1000f) % 1000f);

        timerText.text = string.Format("{0:00}:{1:00}.{2:000}", minutes, seconds, milliseconds);
    }
}

