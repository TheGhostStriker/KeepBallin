using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockWall : MonoBehaviour
{
    public GameObject blockerWall;

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            blockerWall.SetActive(true);
            Destroy(this.gameObject);
        }
    }
}
