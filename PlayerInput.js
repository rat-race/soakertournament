#pragma strict

var speed:float = 10 ;
var rotSpeed:float = 10000 ;
var velocity:Vector3 ;
var rotation:float = 0 ;
static var water:int = 0 ;
static var pressure:float = 0;

var cSqueeze:float = 0 ; //Current squeeze
var pSqueeze:float = 0 ; //Past squeeze

function Start () 
{
	water = 0 ;
	//pressure = 0 ;
}

function Update () 
{

	velocity = Vector3(Input.GetAxis("Horizontal") * speed , 0 , Input.GetAxis("Vertical") * speed) ;
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
	transform.RotateAround( collider.bounds.center,Vector3.up,rotation * Time.deltaTime);
}