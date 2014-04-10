using UnityEngine;
using System.Collections;

public class DebugPlayer : Player 
{

	// Use this for initialization
	public new void Awake () 
	{
		base.Awake ();
		pPrefab = (GameObject)(Resources.Load("Prefabs/player/Penny")) ;
		
		//WARNING: SUPER HACKY CODE
		if(pObject == null) 
			pObject = (GameObject)(Instantiate (pPrefab, new Vector3(5, 10, 5), Quaternion.Euler(Vector3.zero))) ;
		pObject.tag = "debug" ;
		//Spawn(new Vector3(50,50,50)) ;
	}
	
	// Update is called once per frame
	public new void Update () 
	{
	
	}



}
