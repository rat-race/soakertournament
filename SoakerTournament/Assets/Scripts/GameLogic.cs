using UnityEngine;
using System ;
using System.Collections;
using System.IO;
using System.Text;

public struct SpawnPoint
{
	public Vector3 location ;
	public Vector3 rotation ;
	public int affinity ;
	public int direction ;
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
		
		//Load the spawn map
		MapSetup(GameManager.Instance.GetMap()) ;
		
		AddPlayer(0) ;
		AddPlayer(1) ;
		
		//###Map Start###
		
		mapSizeX = 0 ;
		mapSizeY = 0 ;
		defaultTile = 1 ;
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

	void MapSetup(string mapName)
	{
		LoadSpawns("Assets\\Maps\\testmap.txt");
		
		fence = (GameObject)(Resources.Load("Tiles/Fence")) ;
		
		LoadMap("Assets\\Maps\\testmap.txt");
		GenerateTerrain() ;
		GenerateWalls () ;
	}


	int GenerateWalls()
	{
		//Set start position
		
		Vector3 dropPos = new Vector3(-TileSize, 0, -TileSize) ;
		
		//Top wall
		
		for(int i = 1; i < mapSizeX + 1; i++)
		{
			((GameObject)Instantiate(fence, (dropPos + new Vector3(i*TileSize, 0, 0)), Quaternion.Euler(Vector3.zero))).transform.parent = map.transform ;
		}
		
		dropPos = new Vector3(-TileSize, 0, mapSizeY * TileSize) ;
		
		//Bottom wall
		for(int i = 1; i < mapSizeX + 1; i++)
		{
			((GameObject)Instantiate(fence, (dropPos + new Vector3(i*TileSize, 0, 0)), Quaternion.Euler(Vector3.zero))).transform.parent = map.transform ;
		}
		
		dropPos = new Vector3(-TileSize, 0, -TileSize) ;
		
		//Left wall
		
		for(int i = 1; i < mapSizeY + 1; i++)
		{
			((GameObject)Instantiate(fence, (dropPos + new Vector3(0, 0, i*TileSize)), Quaternion.Euler(Vector3.zero))).transform.parent = map.transform ;
		}
		
		dropPos = new Vector3(mapSizeX * TileSize, 0, -TileSize) ;
		
		//Right wall
		
		for(int i = 1; i < mapSizeY + 1; i++)
		{
			((GameObject)Instantiate(fence, (dropPos + new Vector3(0, 0, i*TileSize)), Quaternion.Euler(Vector3.zero))).transform.parent = map.transform ;
		}
		
		return 0 ; //SUCCESS!
	}
	
	int BlankMap()
	{	
		mapSizeX = testbedsizeX ;
		mapSizeY = testbedsizeZ ;
		
		int mapArraySize = mapSizeX * mapSizeY ;
		
		tiles = new byte[mapArraySize] ;
		tData = new TileData[mapArraySize] ;
		
		for(int i = 0 ; i < mapArraySize ; i++ )
		{
			tiles[i] = 1 ;
			tData[i].TileID = 1 ;
			tData[i].Orientation = 0 ;
		}
		
		return 0 ;
	}
	
	//Returns:
	// -1 - No map loaded
	int GenerateTerrain()
	{
		map = new GameObject() ;
		map.name = "Map" ;

		for(int i = 0 ; i < mapArraySize; i++)
		{
			((GameObject)Instantiate(objTiles[tData[i].TileID], new Vector3((i%mapSizeX)*TileSize, 0, (int)(i/mapSizeX)*TileSize), Quaternion.Euler(new Vector3(0, tData[i].Orientation*90, 0)))).transform.parent = map.transform ;

		}
		
		return 0 ;
	}
	
	int LoadMap(string mapName)
	{
		//First pass of file - Find the initialisation
		
		TextReader tr = new StreamReader(mapName) ;
		
		String str = null ;
		
		while ((str = tr.ReadLine())!= null)
		{
			string[] strings = str.Split (' ') ;
			
			if(strings[0] == "I")
			{
				mapSizeX = System.Convert.ToInt32(strings[1]) ;
				mapSizeY = System.Convert.ToInt32(strings[2]) ;
				defaultTile = System.Convert.ToByte(strings[3]) ;
			}
		}
		
		//Set the default tile
		
		mapArraySize = mapSizeX * mapSizeY ;
		
		tiles = new byte[mapArraySize] ;
		tData = new TileData[mapArraySize] ;
		
		for(int i = 0 ; i < mapArraySize ; i++ )
		{
			//tiles[i] = defaultTile ;
			tData[i].TileID = defaultTile ;
			tData[i].Orientation = 0 ;
		}
		
		//Second pass of file - Count the assets
		
		tr = new StreamReader(mapName) ;
		
		assetTotal = 0 ;
		
		while ((str = tr.ReadLine())!= null)
		{
			string[] strings = str.Split (' ') ;
			
			if(strings[0] == "L")
				assetTotal++ ;
		}
		
		objTiles = new GameObject[assetTotal] ;
		
		//Third pass of file - Load the assets
		
		tr = new StreamReader(mapName) ;
		
		assetCount = 0 ;
		
		while ((str = tr.ReadLine())!= null)
		{
			string[] strings = str.Split (' ') ;
			
			if(strings[0] == "L")
			{
				try
				{
					//objTiles[assetCount] = (GameObject)Instantiate(Resources.Load(strings[1])) ;
					
					objTiles[assetCount] = (GameObject)(Resources.Load(strings[1])) ;
					assetCount++ ;
				}
				catch(FormatException e)
				{
					break ;
				}
			}
		}	
		
		//Fourth pass of file - Extract the tile IDs
		
		tr = new StreamReader(mapName) ;
		
		while ((str = tr.ReadLine())!= null)
		{
			string[] strings = str.Split (' ') ;
			
			if(strings[0] == "T")
			{
				try
				{
					//Find the position in the array
					int arrayLocation = System.Convert.ToInt32(strings[2]) + System.Convert.ToInt32(strings[3]) * mapSizeX ;
					//Apply the tile
					tData[arrayLocation].TileID = System.Convert.ToByte(strings[1]) ;
					
					if(strings.GetLength(0) > 4)
						tData[arrayLocation].Orientation = System.Convert.ToByte(strings[4]) ;
				}
				catch(FormatException e)
				{
					break ;
				}
			}
		}
		
		return 0 ;
	}
	
	int ExportMap()
	{
		TextWriter tw = File.CreateText("export.txt") ;
		
		//Export the map size
		
		tw.WriteLine("# Map Size") ;
		tw.WriteLine("S " + System.Convert.ToString(mapSizeX) + " " + System.Convert.ToString(mapSizeY) + " " + System.Convert.ToString(defaultTile)) ;
		
		//Export the asset locations
		
		tw.WriteLine("# Assets") ;
		
		tw.WriteLine(objTiles[0]) ;
		
		//Export the tile IDs
		
		tw.WriteLine("# Tiles") ;
		
		for(int i = 0 ; i < mapArraySize ; i++)
		{
			
		}
		
		tw.Close() ;
		
		return 0 ;
	}
	
	int LoadMap()
	{
		
		
		return 0 ;
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

	int LoadSpawns(string mapName)
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
					//Syntax: S <X> <Y> <Rotation> <Affinity>
					spawnPoints[count].location = new Vector3(System.Convert.ToInt32(strings[1])*TileSize - TileSize/2,5, System.Convert.ToInt32(strings[2])*TileSize - TileSize/2) ;
					spawnPoints[count].rotation = new Vector3 (0, System.Convert.ToInt32(strings[3])*45,0) ;
					spawnPoints[count].affinity = System.Convert.ToInt32(strings[4]) ;
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

	//----------------------------------------------------------------------------------
	// Player handling
	//----------------------------------------------------------------------------------
}