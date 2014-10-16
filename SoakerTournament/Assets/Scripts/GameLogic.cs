using UnityEngine;
using System ;
using System.Collections;
using System.IO;
using System.Text;

public struct SpawnPoint
{
	public Vector3 location ;
	public Quaternion rotation ;
	public int affinity ;
	//public int direction ;
}

public class GameLogic : MonoBehaviour
{
	
	//private LocalPlayer lPlayer ;
	//private DebugPlayer dPlayer ;
	//private Map pMap ;
	//private Spawning spawning ;

	//Spawning stuff

	private SpawnPoint[] spawnPoints ;
	private int spawnTotal = 0 ;
	private System.Random rng ;
	static int TileSize = 10 ; //FIXED!

	//Maps

	public GameObject map ;

	public int testbedsizeX ;
	public int testbedsizeZ ;
	private byte[] tiles ;
	private TileData[] tData ;
	private int mapSizeX ;
	private int mapSizeY ;
	private byte defaultTile ;
	private int mapArraySize ;

	private GameObject[] objTiles ;	
	private GameObject fence ;
	
	// Asset manager stuff
	
	private int assetTotal ;
	private int assetCount ;
	
	//Player handling stuff

	private GameObject playerPrefab ;
	private const int MAXPLAYERS = 8 ;
	private GameObject[] goPlayers ;

	void Start()
	{
		playerPrefab = (GameObject)(Resources.Load("Prefabs/player/Penny")) ;
		
		//Set the number of players
		
		goPlayers = new GameObject[MAXPLAYERS] ;
		
		for(int i = 0 ; i < MAXPLAYERS; i++)
		{
			goPlayers[i] = null ;
		}
		
		//Generate the spawn list from the map

		GenerateSpawnList() ;


		//Add the players - Currently just adds two players - One player and one debug character
		
		AddPlayer(0) ;
		AddPlayer(1) ;
	
	}
		
	// Update is called once per frame
	void Update ()
	{

	}


	// Inputs:
	// 0 - Local player
	// 1 - Debug Player
	// 
	//
	//

	int AddPlayer(int type)
	{
		for(int i = 0 ; i < MAXPLAYERS; i++)
		{
			if(goPlayers[i] == null)
			{
				//Create the player
				if(type == 0)
				{
					goPlayers[i] = (GameObject)(Instantiate (playerPrefab, new Vector3(5, 10, 5), Quaternion.Euler(Vector3.zero))) ;
					goPlayers[i].AddComponent("LocalPlayer");

					Spawn(goPlayers[i].GetComponent<Player>()) ;
					return 0;
				}
				else if(type == 1)
				{
					goPlayers[i] = (GameObject)(Instantiate (playerPrefab, new Vector3(5, 10, 5), Quaternion.Euler(Vector3.zero))) ;
					goPlayers[i].AddComponent("DebugPlayer");

					Spawn(goPlayers[i].GetComponent<Player>()) ;
					return 0;
				}
			}
		}

		return 1 ;
	}

	//----------------------------------------------------------------------------------
	// Map
	//----------------------------------------------------------------------------------

	void GenerateSpawnList()
	{
		//Set up the RNG seed for this component.
		rng = new System.Random(DateTime.Now.Millisecond) ;

		//Find out how many spawns are on the map

		GameObject[] objArray = GameObject.FindGameObjectsWithTag("spawn");

		spawnTotal = objArray.GetLength(0) ;
		spawnPoints = new SpawnPoint[spawnTotal] ;

		//Load all of the spawns into a nicer format than gameobjects

		for(int i = 0; i < spawnTotal; i++)
		{
			spawnPoints[i].location = objArray[i].transform.position ;
			spawnPoints[i].rotation = objArray[i].transform.rotation ;

			//Get rid of the GameObject
		}

	}


	//----------------------------------------------------------------------------------
	// Spawning
	//----------------------------------------------------------------------------------

	public int Spawn(Player thePlayer)
	{
		//If there are no spawn points return 1
		if(spawnTotal <= 0)
			return -1 ;
		
		int selected = rng.Next(spawnTotal) ;
		
		thePlayer.Spawn(spawnPoints[selected].location, spawnPoints[selected].rotation ) ;
		
		return selected ;
	}



	//----------------------------------------------------------------------------------
	// Player handling
	//----------------------------------------------------------------------------------
}