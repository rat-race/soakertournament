using UnityEngine;
using System.Collections;

public class LocalInput : MonoBehaviour
{
	
	private float speed = 10 ;
	private float rotSpeed = 600 ;
	private Vector3 velocity ;
	private float rotation = 0 ;
	public static float water = 0 ;
	public static float pressure = 0;
	public static int spawnpoint = 0 ;
	public static int fireDistance = 0 ;
	
	float cSqueeze = 0 ; //Current squeeze
	float pSqueeze = 0 ; //Past squeeze
	
	private Rigidbody waterBall ;
	public GameObject pPrefab ;
	
	// Use this for initialization
	public void Start ()
	{
		pPrefab = (GameObject)(Resources.Load("Prefabs/Sphere")) ;
		
		water = 100 ;
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
			pressure += 5 * (cSqueeze - pSqueeze) * Time.deltaTime ;
		
		if(pressure > 100.0f)
			pressure = 100.0f ;
		
		//water++ ;
		
		//Set the floor/ceiling for water
		
		if(water > 100.0f)
			water = 100.0f ;
		
		if(water < 0.0f)
			water = 0.0f ;
		
		
		
		if(Input.GetAxis ("Fire") > 0.5f)
		{
			GameObject clone ;
			clone = Instantiate(pPrefab, transform.position, transform.rotation) as GameObject ;
			clone.rigidbody.velocity = transform.TransformDirection (Vector3.forward * 100);
			
			
			//fireDistance = 0 ;
			water -= pressure / 15 ;
			pressure -= pressure / 7 * Time.deltaTime;
			if(pressure < 0.1)
				pressure = 0 ;
			fireDistance = (int)(pressure) ;
			
			
			
		}
		
		
		//pressure += Input.GetAxis("Pressure") ;
		
		transform.Translate(velocity * Time.deltaTime);
		//transform.Rotate(0,rotation * Time.deltaTime,0) ;
		//transform.RotateAround (Vector3.zero, Vector3.up, 20 * Time.deltaTime);
		
		//RotateAround(point: Vector3, axis: Vector3, angle: float): void;
		
		
		transform.RotateAround( collider.bounds.center, Vector3.up, rotation * Time.deltaTime);
	}
}