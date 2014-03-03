using UnityEngine;
using System.Collections;

public class LocalPlayer : Player
{
	public LocalInput lInput ;

	
	public new void Start()
	{
		pPrefab = (GameObject)(Resources.Load("Prefabs/player/Penny")) ;
		pObject = (GameObject)(Instantiate (pPrefab, new Vector3(5, 10, 5), Quaternion.Euler(Vector3.zero))) ;
		Camera.main.transform.parent = pObject.transform ;
		Camera.main.transform.localPosition = new Vector3(0,20,-100) ;
		//lInput = gameObject.AddComponent("LocalInput") as LocalInput ;
		//gameObject.AddComponent("LocalInput") ;// as LocalInput ;
		//lInput = gameObject.AddComponent<LocalInput>() ;
		Spawn () ;
	}


	public new void Spawn()
	{
		//lInput = gameObject.AddComponent("LocalInput") as LocalInput ;
		pObject.transform.position = new Vector3(5,10,5) ;
	}
}