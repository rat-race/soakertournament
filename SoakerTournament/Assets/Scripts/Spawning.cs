using UnityEngine;
using System.Collections;

public struct SpawnPoint
{
	public Vector3 location ;
	public int affinity ;
}

public class Spawning : MonoBehaviour 
{
	private SpawnPoint[] Spawnpoints ;
	
	// Use this for initialization
	void Start () 
	{

	}

	// Update is called once per frame
	void Update () 
	{

	}

	int Load(string mapName)
	{
		TextReader tr = new StreamReader(mapName) ;
		
		String str = null ;


	}
}
