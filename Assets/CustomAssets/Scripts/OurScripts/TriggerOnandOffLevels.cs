using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerOnandOffLevels : MonoBehaviour
{
    public GameObject prevLevel;
    public GameObject newLevel;
    public Animator doorAnim;
    public AudioSource congratsPlayer;
    


    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            congratsPlayer.Play();
            prevLevel.SetActive(false);
            newLevel.SetActive(true);
            doorAnim.SetTrigger("CloseDoor");

            Destroy(this.gameObject);
        }
    }
}
