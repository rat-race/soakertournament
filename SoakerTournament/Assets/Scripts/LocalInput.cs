using UnityEngine;
using System;
using System.Collections;
using System.IO;
using System.Text;

public class LocalInput : MonoBehaviour 
{

	private float speed = 10 ;
	private float rotSpeed = 600 ;
	private Vector3 velocity ;
	private float rotation = 0 ;
	public static int spawnpoint = 0 ;
	private Gun gun ;
	private Player player ;
	//public static int fireDistance = 0 ;

	float cSqueeze = 0 ; //Current squeeze
	float pSqueeze = 0 ; //Past squeeze

	public static float fireDistance = 500.0f ;



	System.Random rng ;

	// Use this for initialization
	public void Start () 
	{

		rng = new System.Random(DateTime.Now.Millisecond) ;

		gun = GetComponent<LocalPlayer>().gun ;


		//gun = player.gun ;
		//gun = GetComponent("Player").gun ;

		//pressure = 0 ;
	}

	// Update is called once per frame
	public void Update () 
	{
		velocity = new Vector3(Input.GetAxis("Horizontal") * speed , 0 , Input.GetAxis("Vertical") * speed) ;
		rotation = Input.GetAxis("Turn") * rotSpeed ;

		pSqueeze = cSqueeze ;
		cSqueeze = Input.GetAxis("Pressure") ;



		if(cSqueeze > pSqueeze)
			gun.Pump (cSqueeze - pSqueeze) ;

		//gun = GetComponent<LocalPlayer>().gun ;


		if(Input.GetAxis ("Fire") > 0.5f)
		{

			//(Player)GetComponent("Player").gun.Fire();
			GetComponent<LocalPlayer>().gun.Fire() ;

			/*
			//fireDistance = 0 ;
			water -= pressure / 15 ;
			pressure -= pressure / 7 * Time.deltaTime; 
			if(pressure < 0.1)
				pressure = 0 ;
			fireDistance = (int)(pressure) ;

			GameObject clone ;
			//clone = Instantiate(pPrefab, transform.position, transform.rotation) as GameObject ;
			clone = Instantiate(pPrefab, transform.TransformPoint(Vector3.forward * fireDistance) + new Vector3(0,3, 0), transform.rotation) as GameObject ; //To do: Move this to the gun
			clone.rigidbody.velocity = transform.TransformDirection(new Vector3((rng.Next(20)-10), (rng.Next (20)-10), 100)) ; //(Vector3.forward * 100);
			*/
		}


		//pressure += Input.GetAxis("Pressure") ;

		transform.Translate(velocity * Time.deltaTime);
		//transform.Rotate(0,rotation * Time.deltaTime,0) ;
		//transform.RotateAround (Vector3.zero, Vector3.up, 20 * Time.deltaTime);

		//RotateAround(point: Vector3, axis: Vector3, angle: float): void;


		transform.RotateAround( collider.bounds.center, Vector3.up, rotation * Time.deltaTime);
	}
}