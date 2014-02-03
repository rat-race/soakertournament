#pragma strict

var pWater : GUIText ;
var pPressure : GUIText ;

function Start () 
{

}

function Update () 
{
	pWater.text = "Water: " + PlayerInput.water ;
	pPressure.text = "Pressure: " + PlayerInput.pressure ;
}