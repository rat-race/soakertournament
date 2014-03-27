using UnityEngine;
using System.Collections;

public class LocalInput : MonoBehaviour 
{

	private float speed = 10 ;
	private float rotSpeed = 600 ;
	private Vector3 velocity ;
	private float rotation = 0 ;
	public static int water = 0 ;
	public static float pressure = 0;
	
	float cSqueeze = 0 ; //Current squeeze
	float pSqueeze = 0 ; //Past squeeze

	// Use this for initialization
	public void Start () 
	{
		water = 0 ;
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
			pressure += cSqueeze - pSqueeze ;
		
		if(pressure > 100.0f)
			pressure = 100.0f ;
		
		//pressure += Input.GetAxis("Pressure") ;
		
		
		
		transform.Translate(velocity * Time.deltaTime);
		//transform.Rotate(0,rotation * Time.deltaTime,0) ;
		//transform.RotateAround (Vector3.zero, Vector3.up, 20 * Time.deltaTime);
		
		//RotateAround(point: Vector3, axis: Vector3, angle: float): void;


		transform.RotateAround( collider.bounds.center, Vector3.up, rotation * Time.deltaTime);
	}
}