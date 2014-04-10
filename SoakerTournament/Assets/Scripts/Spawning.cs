using UnityEngine;
using System ;
using System.Collections;
using System.IO;
using System.Text;
public struct SpawnPoint
{
	public Vector3 location ;
	public int affinity ;
}

public class Spawning : MonoBehaviour 
{
	private SpawnPoint[] spawnPoints ;
	private int spawnTotal = 0 ;
	private System.Random rng ;
	static int TileSize = 10 ; //REMEMBER TO FIX THIS
	
	// Use this for initialization
	void Start () 
	{
		//Load the only current map

	}

	// Update is called once per frame
	void Update () 
	{

	}

	//Returns:
	// 1 - No spawn points found!

	public int Spawn(Player thePlayer)
	{
		//If there are no spawn points return 1
		if(spawnTotal <= 0)
			return 1 ;

		thePlayer.Spawn(spawnPoints[rng.Next(spawnTotal)].location) ;

		return 0 ; 
	}

	public int Load(string mapName)
	{

		//Set up the RNG seed for this component.
		rng = new System.Random(DateTime.Now.Millisecond) ;

		TextReader tr = new StreamReader(mapName) ;
		
		String str = null ;

		//PASS 1 - Count the spawnpoints, set the array size

		spawnTotal = 0 ;
		
		while ((str = tr.ReadLine())!= null)
		{
			string[] strings = str.Split (' ') ;
			
			if(strings[0] == "S")
				spawnTotal++ ;
		}

		spawnPoints = new SpawnPoint[spawnTotal] ;

		tr = new StreamReader(mapName) ;
		int count = 0 ;
		
		while ((str = tr.ReadLine())!= null)
		{
			string[] strings = str.Split (' ') ;
			
			if(strings[0] == "S")
			{
				try
				{
					//Set the position and the affinity
					spawnPoints[count].location = new Vector3(System.Convert.ToInt32(strings[1])*TileSize,30, System.Convert.ToInt32(strings[2])*TileSize) ;
					spawnPoints[count].affinity = System.Convert.ToInt32(strings[3]) ;
				}
				catch(FormatException e)
				{
					break ;
				}

				count++ ;
			}
		}

		return 0 ;
	}
}
