using UnityEngine;
using System.Collections;

public class DebugPlayer : Player 
{

	// Use this for initialization
	new void Start () 
	{
		base.Start ();
		Spawn(new Vector3(50,50,50)) ;
	}
	
	// Update is called once per frame
	new void Update () 
	{
	
	}



}
