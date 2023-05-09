using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseDoor : MonoBehaviour
{
    public Animator doorAnim;
    // Start is called before the first frame update
    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            doorAnim.SetTrigger("CloseTheDoor");
            Destroy(this.gameObject);
        }
    }
}
