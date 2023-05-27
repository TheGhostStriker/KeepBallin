using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HightGroundTrigger : MonoBehaviour
{

    public PlayerMovementJoystick playerMovement;

    public AudioSource naughtySource;
    public AudioClip naughtyClip;


    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            naughtySource.PlayOneShot(naughtyClip);
            playerMovement.GetComponent<PlayerMovementJoystick>().enabled = false;
            Invoke("RestartTheLevel", 7f);
        }
    }

    public void RestartTheLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
