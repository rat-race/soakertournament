using UnityEngine;
using System;
using System.Collections;
using System.IO;
using System.Text;

public class Gun : MonoBehaviour 
{

	//What needs to be done here
	//ParticleSystem needs to be created to be turned on when firing and turned off when not firing
	//Hitbox needs to be put in place which does the damage calculation
	//Pressure and water need to be stored here so that the gun can be dropped
	//Multiple guns?

	public static float water = 0 ;
	public static float pressure = 0;

	private Rigidbody waterBall ;
	public GameObject pPrefab ;

	System.Random rng ;



	// Use this for initialization
	void Start () 
	{
		pressure = 0 ;
		water = 100 ;

		pPrefab = (GameObject)(Resources.Load("Prefabs/Sphere")) ;
		rng = new System.Random(DateTime.Now.Millisecond) ;
	}

	void Awake()
	{
		pressure = 0 ;
		water = 100 ;
		
		pPrefab = (GameObject)(Resources.Load("Prefabs/Sphere")) ;
		rng = new System.Random(DateTime.Now.Millisecond) ;
	}

	public void SetParent(GameObject obj)
	{
		gameObject.transform.parent = obj.transform ;
		gameObject.transform.Translate(new Vector3(0, 4, 0)) ;
	}

	public void Pump(float amount)
	{
		pressure += 5 * amount * Time.deltaTime  ;
		
		if(pressure > 100.0f)
			pressure = 100.0f ;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Set the floor/ceiling for water

		if(water > 100.0f)
			water = 100.0f ;
		
		if(water < 0.0f)
			water = 0.0f ;
	}

	public void Fire()
	{
		//fireDistance = 0 ;
		water -= pressure / 15 ;
		pressure -= pressure / 7 * Time.deltaTime; 
		if(pressure < 0.1)
			pressure = 0 ;
		//fireDistance = (int)(pressure) ;
		
		GameObject clone ;
		//clone = Instantiate(pPrefab, transform.position, transform.rotation) as GameObject ;
		clone = Instantiate(pPrefab, transform.position, transform.rotation) as GameObject ; //To do: Move this to the gun
		clone.rigidbody.velocity = transform.TransformDirection(new Vector3((rng.Next(20)-10), (rng.Next (20)-10), 100)) ; //(Vector3.forward * 100);
	}
}
