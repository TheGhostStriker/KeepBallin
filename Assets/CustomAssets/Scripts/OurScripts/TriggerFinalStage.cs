using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerFinalStage : MonoBehaviour
{
    public GameObject finalBoss;

    
    public GameObject bossBattle;

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            finalBoss.SetActive(true);
            bossBattle.SetActive(true);
            Destroy(this.gameObject);
        }
    }
}
