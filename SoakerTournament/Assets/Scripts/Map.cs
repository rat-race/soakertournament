using UnityEngine;
using System ;
using System.Collections;
using System.IO;
using System.Text;

public struct TileData
{
	public byte TileID ;
	public byte Orientation ;
}

public class Map : MonoBehaviour 
{
	public int testbedsizeX ;
	public int testbedsizeZ ;
	private byte[] tiles ;
	private TileData[] tData ;
	private int mapSizeX ;
	private int mapSizeY ;
	private byte defaultTile ;
	private int mapArraySize ;
	static int TileSize = 10 ;
	
	public GameObject obj ;
	public GameObject obj2 ;
	private GameObject[] objTiles ;	
	private GameObject fence ;
	
	// Asset manager stuff

	private int assetTotal ;
	private int assetCount ;
	//private string assetLocations[] ;
	
	
	// Use this for initialization
	void Start () 
	{		
		mapSizeX = 0 ;
		mapSizeY = 0 ;
		defaultTile = 1 ;
		
		//Load prefab library
		//objTiles = new GameObject[4] ;
		
//		objTiles[0] = (GameObject)Instantiate(Resources.Load("Tiles/Tile1")) ;
//		objTiles[1] = (GameObject)Instantiate(Resources.Load("Tiles/Tile1")) ;
//		objTiles[2] = (GameObject)Instantiate(Resources.Load("Tiles/Tile2")) ;
		
		fence = (GameObject)(Resources.Load("Tiles/Fence")) ;
		
		//BlankMap() ;
		LoadMap("Assets\\testmap.txt");
		GenerateTerrain() ;
		GenerateWalls () ;
		ExportMap() ;
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	int ClearMap()
	{
		return 0 ;
	}

	int GenerateWalls()
	{
		//Set start position

		Vector3 dropPos = new Vector3(-TileSize, 0, -TileSize) ;

		//Top wall

		for(int i = 1; i < mapSizeX + 1; i++)
		{
			Instantiate(fence, (dropPos + new Vector3(i*TileSize, 0, 0)), Quaternion.Euler(Vector3.zero)) ;
		}

		dropPos = new Vector3(-TileSize, 0, mapSizeY * TileSize) ;

		//Bottom wall
		for(int i = 1; i < mapSizeX + 1; i++)
		{
			Instantiate(fence, (dropPos + new Vector3(i*TileSize, 0, 0)), Quaternion.Euler(Vector3.zero)) ;
		}

		dropPos = new Vector3(-TileSize, 0, -TileSize) ;

		//Left wall

		for(int i = 1; i < mapSizeY + 1; i++)
		{
			Instantiate(fence, (dropPos + new Vector3(0, 0, i*TileSize)), Quaternion.Euler(Vector3.zero)) ;
		}

		dropPos = new Vector3(mapSizeX * TileSize, 0, -TileSize) ;

		//Right wall

		for(int i = 1; i < mapSizeY + 1; i++)
		{
			Instantiate(fence, (dropPos + new Vector3(0, 0, i*TileSize)), Quaternion.Euler(Vector3.zero)) ;
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
		
		for(int i = 0 ; i < mapArraySize; i++)
		{
			Instantiate(objTiles[tData[i].TileID], new Vector3((i%mapSizeX)*TileSize, 0, (int)(i/mapSizeX)*TileSize), Quaternion.Euler(new Vector3(0, tData[i].Orientation*90, 0))) ;
			
//			// Ugly switch statement
//			switch(tData[i].Orientation)
//			{
//				case 0:
//					Instantiate(objTiles[tData[i].TileID], new Vector3((i%mapSizeX)*TileSize, 0, (int)(i/mapSizeX)*TileSize), Quaternion.Euler(Vector3.zero)) ;
//				break ;
//				case 1:
//					Instantiate(objTiles[tData[i].TileID], new Vector3((i%mapSizeX)*TileSize, 0, (int)(i/mapSizeX)*TileSize), Quaternion.Euler(new Vector3(0, 90, 0))) ;
//				break ;
//				case 2:
//					Instantiate(objTiles[tData[i].TileID], new Vector3((i%mapSizeX)*TileSize, 0, (int)(i/mapSizeX)*TileSize), Quaternion.Euler(new Vector3(0, 180, 0))) ;
//				break ;
//				case 3:
//					Instantiate(objTiles[tData[i].TileID], new Vector3((i%mapSizeX)*TileSize, 0, (int)(i/mapSizeX)*TileSize), Quaternion.Euler(new Vector3(0, 270, 0))) ;
//				break ;
//				default:
//					Instantiate(objTiles[tData[i].TileID], new Vector3((i%mapSizeX)*TileSize, 0, (int)(i/mapSizeX)*TileSize), Quaternion.Euler(Vector3.zero)) ;
//				break ;
//			}
			//Instantiate(objTiles[tiles[i]], new Vector3((i%mapSizeX)*TileSize, 0, (int)(i/mapSizeX)*TileSize), Quaternion.Euler(Vector3.zero)) ;
//			if(tiles[i] == 2)
//				Instantiate(obj2, new Vector3((i%mapSizeX)*TileSize, 0, (int)(i/mapSizeX)*TileSize), Quaternion.Euler(Vector3.zero)) ;
//			else if(tiles[i] == 1)
//				Instantiate(obj, new Vector3((i%mapSizeX)*TileSize, 0, (int)(i/mapSizeX)*TileSize), Quaternion.Euler(Vector3.zero)) ;
//			else Instantiate(obj, new Vector3((i%mapSizeX)*TileSize, 0, (int)(i/mapSizeX)*TileSize), Quaternion.Euler(Vector3.zero)) ;
				
		}
		
//		for(int i = 0 ; i < mapSizeX ; i++)
//		{
//			for(int j = 0 ; j < mapSizeY ; j++)
//			{
//				Instantiate(obj, new Vector3(i*TileSize, 0, j*TileSize), Quaternion.Euler(Vector3.zero)) ;
//			}
//		}

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
		tw.WriteLine("S " + System.Convert.ToString(mapSizeX) + " " + System.Convert.ToString(mapSizeY) + " " + System.Convert.ToString(defaultTile))  ;

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
}
