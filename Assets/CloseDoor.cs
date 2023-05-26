using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseDoor : MonoBehaviour
{
    public Animator doorAnim;
    public AudioSource sciDoor;
    public AudioClip sciDoorClose;
    // Start is called before the first frame update
    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            doorAnim.SetTrigger("CloseTheDoor");
            sciDoor.PlayOneShot(sciDoorClose);
            Destroy(this.gameObject);
        }
    }
}
