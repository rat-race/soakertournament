using UnityEngine;
using System.Collections;

public class GameLogic : MonoBehaviour 
{

	private LocalPlayer lPlayer ;
	private DebugPlayer dPlayer ;
	private Map pMap ;

	// Use this for initialization
	void Start () 
	{
		pMap = gameObject.AddComponent("Map") as Map ;
		lPlayer = gameObject.AddComponent("LocalPlayer") as LocalPlayer ;
		dPlayer = gameObject.AddComponent("DebugPlayer") as DebugPlayer ;

		dPlayer.Spawn(new Vector3(100,10,100)) ;
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
