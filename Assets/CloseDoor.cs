using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseDoor : MonoBehaviour
{
    public Animator doorAnim;
    public AudioSource sciDoor;
    public AudioClip sciDoorClose;
    public GameObject pipesObject;

    public AudioSource welcomeToLab;
    public AudioClip welcomeToLabClip;
    // Start is called before the first frame update
    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && welcomeToLab != null)
        {
            if (!welcomeToLab.isPlaying)
            {
                welcomeToLab.PlayOneShot(welcomeToLabClip);
                Destroy(welcomeToLab, 4.2f);
            }
            doorAnim.SetTrigger("CloseTheDoor");
            sciDoor.PlayOneShot(sciDoorClose);
            
            Destroy(this.gameObject);
            pipesObject.SetActive(false);
            
            
        }
    }

   

   
}
