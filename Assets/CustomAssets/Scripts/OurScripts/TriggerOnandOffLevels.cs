using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerOnandOffLevels : MonoBehaviour
{
    public GameObject prevLevel;
   
    public Animator doorAnim;
    public AudioSource congratsPlayer;
    public AudioSource slamDoor;
    public AudioClip doorSlam;
    


    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            congratsPlayer.Play();
            prevLevel.SetActive(false);
            
            doorAnim.SetTrigger("CloseDoor");
            slamDoor.PlayOneShot(doorSlam);

            Destroy(this.gameObject);
        }
    }
}
