using UnityEngine;
using System.Collections;

public class GameManager
{
	private int currentScene ; // Tracks the current scene
	private int currentNetworkState ; // Disconnected/connecting/connected/host/client/disconnected/
	private string currentMap ;

	//=====================================================

	//Singleton stuff

	private static GameManager instance;
	private GameManager() 
	{
		currentMap = "Assets\\Maps\\testmap.txt" ;
	}

	public static GameManager Instance
	{
		get
		{
			if(instance == null)
			{
				instance = new GameManager() ;
			}
			return instance ;
		}
	}

	//=====================================================

	public void SetMap(string map)
	{
		currentMap = map ;
	}

	public string GetMap()
	{
		return currentMap;
	}
}
