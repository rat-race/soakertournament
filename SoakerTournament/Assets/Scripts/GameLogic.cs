using UnityEngine;
using System.Collections;

public class GameLogic : MonoBehaviour 
{

	private LocalPlayer lPlayer ;
	private DebugPlayer dPlayer ;
	private Map pMap ;
	private Spawning spawning ;

	// Use this for initialization
	void Start () 
	{
		pMap = gameObject.AddComponent("Map") as Map ;
		lPlayer = gameObject.AddComponent("LocalPlayer") as LocalPlayer ;
		dPlayer = gameObject.AddComponent("DebugPlayer") as DebugPlayer ;
		spawning = gameObject.AddComponent("Spawning") as Spawning ;

		//Load the spawn map
		spawning.Load("Assets\\testmap.txt");

		//lPlayer.Start() ;

		lPlayer.Init () ;
		dPlayer.Init () ;


		spawning.Spawn(dPlayer) ;
		spawning.Spawn(lPlayer) ;

		//dPlayer.Spawn(new Vector3(100,10,100)) ;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.J))
		   LocalInput.spawnpoint = spawning.Spawn (lPlayer) ;
	}
}
