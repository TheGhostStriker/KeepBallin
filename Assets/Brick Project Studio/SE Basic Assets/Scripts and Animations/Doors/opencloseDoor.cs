using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SojaExiles

{
	public class opencloseDoor : MonoBehaviour
	{

		public Animator openandclose;
		public bool open;
		public Transform Player;

		public AudioClip openingDoor;
		public AudioClip closingDoor;

		void Start()
		{
			open = false;
		}

        public void Update()
        {
            if(Input.GetKeyDown(KeyCode.F))
            {
				StartCoroutine(opening());
            }

			if(Input.GetKeyDown(KeyCode.G))
            {
				StartCoroutine(closing());
            }
        }

        void OnMouseOver()
		{
			
				if (Player)
				{
					float dist = Vector3.Distance(Player.position, transform.position);
					if (dist < 15)
					{
						if (open == false)
						{
							if (Input.GetMouseButtonDown(0))
							{
								StartCoroutine(opening());
							}
						}
						else
						{
							if (open == true)
							{
								if (Input.GetMouseButtonDown(0))
								{
									StartCoroutine(closing());
								}
							}

						}

					}
				}

			

		}

		IEnumerator opening()
		{
			print("you are opening the door");
			openandclose.Play("Opening");
			GetComponent<AudioSource>().clip = openingDoor;
			GetComponent<AudioSource>().Play();
			open = true;
			yield return new WaitForSeconds(.5f);
		}

		IEnumerator closing()
		{
			print("you are closing the door");
			openandclose.Play("Closing");
			GetComponent<AudioSource>().clip = closingDoor;
			GetComponent<AudioSource>().Play();
			open = false;
			yield return new WaitForSeconds(.5f);
		}


	}
}