using UnityEngine;
using System.Collections;

public class LocalPlayer : Player
{
	public LocalInput lInput ;

	public new void Start()
	{
		base.Start() ;
	}

	public new void Awake()
	{
		pPrefab = (GameObject)(Resources.Load("Prefabs/player/Penny")) ;
		pObject = (GameObject)(Instantiate (pPrefab, new Vector3(5, 10, 5), Quaternion.Euler(Vector3.zero))) ;
		pObject.tag = "local" ;
		Camera.main.transform.parent = pObject.transform ;
		Camera.main.transform.localPosition = new Vector3(0,20,-100) ;
		
		//lInput = (LocalInput)gameObject.AddComponent("LocalInput");
		
		//lInput = (GameObject)Instantiate
		
		lInput = pObject.AddComponent("LocalInput") as LocalInput ;
		//Instantiate(lInput) ;
		
		
		
		//gameObject.AddComponent("LocalInput") ;// as LocalInput ;
		//lInput = gameObject.AddComponent<LocalInput>() ;
		//Spawn () ;
	}
	
	
	// public new void Spawn()
	// {
	// //lInput = gameObject.AddComponent("LocalInput") as LocalInput ;
	// pObject.transform.position = new Vector3(5,10,5) ;
	// }
}