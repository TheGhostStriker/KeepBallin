using UnityEngine;
using System.Collections;

public class door : MonoBehaviour {
	GameObject thedoor;

void OnTriggerEnter ( Collider other  )
	{
		if(other.CompareTag("Player"))
        {
			thedoor = GameObject.FindWithTag("SF_Door");
			thedoor.GetComponent<Animation>().Play("open");
		}
	
}

void OnTriggerExit ( Collider other  )
	{
		if(other.CompareTag("Player"))
        {
			thedoor = GameObject.FindWithTag("SF_Door");
			thedoor.GetComponent<Animation>().Play("close");
		}
	
}
}