using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenAndCloseDoor : MonoBehaviour
{

    public Animator doorAnim;
    public GameObject closeTrigger;
    public AudioClip openSciDoor;

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            doorAnim.SetTrigger("OpenTheDoor");
            GetComponent<AudioSource>().clip = openSciDoor;
            GetComponent<AudioSource>().Play();
            closeTrigger.SetActive(true);
            Destroy(this.gameObject);
        }

    }
}
