using UnityEngine;
using System.Collections;

public class DebugPlayer : Player 
{

	// Use this for initialization
	public new void Awake () 
	{
		base.Awake ();

		gameObject.tag = "debug" ;
	}
	
	// Update is called once per frame
	public new void Update () 
	{
	
	}



}
