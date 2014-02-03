#pragma strict

var speed:float = 10 ;
var rotSpeed:float = 10000 ;
var velocity:Vector3 ;
var rotation:float = 0 ;

function Start () 
{

}

function Update () 
{

	velocity = Vector3(0 , 0 , Input.GetAxis("Vertical") * speed) ;
	rotation = Input.GetAxis("Turn") * rotSpeed ;
	
	transform.Translate(velocity * Time.deltaTime);
	//transform.Rotate(0,rotation * Time.deltaTime,0) ;
	//transform.RotateAround (Vector3.zero, Vector3.up, 20 * Time.deltaTime);
	
	//RotateAround(point: Vector3, axis: Vector3, angle: float): void;
	transform.RotateAround( collider.bounds.center,Vector3.up,rotation * Time.deltaTime);
}