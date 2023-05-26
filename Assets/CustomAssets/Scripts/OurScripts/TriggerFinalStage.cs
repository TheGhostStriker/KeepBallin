using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerFinalStage : MonoBehaviour
{
    public GameObject finalBoss;

    public AudioSource dunDunDun;
    public AudioClip bossBattle;

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            finalBoss.SetActive(true);
            dunDunDun.PlayOneShot(bossBattle);
            Destroy(this.gameObject);
        }
    }
}
