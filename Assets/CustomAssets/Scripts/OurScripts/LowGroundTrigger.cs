using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LowGroundTrigger : MonoBehaviour
{

    public GameObject floorObject;
    public PlayerMovementJoystick playerMovement;

    public AudioSource naughtySource;
    public AudioClip naughtyClip;


    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            floorObject.SetActive(true);
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
