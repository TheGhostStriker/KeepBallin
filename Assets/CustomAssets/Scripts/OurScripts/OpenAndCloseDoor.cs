using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenAndCloseDoor : MonoBehaviour
{

    public Animator doorAnim;
    public GameObject closeTrigger;

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            doorAnim.SetTrigger("OpenTheDoor");
            closeTrigger.SetActive(true);
            Destroy(this.gameObject);
        }

    }
}
