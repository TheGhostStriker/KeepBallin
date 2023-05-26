using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenAndCloseDoor : MonoBehaviour
{

    public Animator doorAnim;
    public GameObject closeTrigger;
    public AudioSource doorSlam;
    public AudioClip doorSlamClip;

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            doorAnim.SetTrigger("OpenTheDoor");
            doorSlam.PlayOneShot(doorSlamClip);
            closeTrigger.SetActive(true);
            Destroy(this.gameObject);
        }

    }
}
