using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalDoorTrigger : MonoBehaviour
{
    public Animator finalDoorClose;

    public AudioSource doorSlamming;
    public AudioClip doorSlam;
    // Start is called before the first frame update
    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            finalDoorClose.SetTrigger("FinalClose");
            doorSlamming.PlayOneShot(doorSlam);
            Destroy(this.gameObject);
        }
    }
}
