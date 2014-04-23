#pragma strict

var pWater : GUIText ;
var pPressure : GUIText ;
var pSpawnpoint : GUIText ;

function Start () 
{
	
}

function Update () 
{
	pWater.text = "Water: " + PlayerInput.water ;
	pPressure.text = "Pressure: " + PlayerInput.pressure ;
	//pSpawnpoint = "Spawn: " + PlayerInput.spawnpoint ;
	
}