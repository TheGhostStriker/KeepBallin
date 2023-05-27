using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerOnandOffLevels : MonoBehaviour
{
    
   
    public Animator doorAnim;
    
    public AudioSource slamDoor;
    public AudioClip doorSlam;
    


    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            
            
            
            doorAnim.SetTrigger("CloseDoor");
            slamDoor.PlayOneShot(doorSlam);

            Destroy(this.gameObject);
        }
    }
}
