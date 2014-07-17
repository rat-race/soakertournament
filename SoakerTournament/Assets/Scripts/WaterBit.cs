using UnityEngine;
using System.Collections;

public class WaterBit : MonoBehaviour 
{

	private bool isActive = false ;

	// Use this for initialization
	void Start () 
	{
		isActive = true ;
		Destroy(gameObject, 5.0f) ; //Destroys the object after 5 seconds if it isn't already destroyed
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(isActive == false)
		{
			Destroy(gameObject) ;
		}

	}

	void OnTriggerEnter(Collider other)
	{
		if(isActive)
		{
			if(other.tag == "Player")
			{
				//Make them wet (AWWWWW YEAH)

			}

			//Reduce the item to inactive and move it out of the area


			//transform.position = new Vector3(0,-5,0) ;
			isActive = false ;
		}
	}
}
