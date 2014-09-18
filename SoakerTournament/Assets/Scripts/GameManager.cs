using UnityEngine;
using System.Collections;

public class GameManager
{
	public static GameManager instance;
	private GameManager() {} 

	static int currentScene ; // Tracks the current scene
	static int currentNetworkState ; // Disconnected/connecting/connected/host/client/disconnected/
	static string currentMap ;

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

}
