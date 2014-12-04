using UnityEngine;
using System.Collections;

public class GameManager
{
	public static GameManager instance;
	private GameManager() {} 
	
	public const string GameTypeName = "SoakerTournament";
	public static string GameName = "Test1";
	public static int NumberOfPlayers = 7;
	public static NetworkPeerType currentNetworkState;

	static int currentScene ; // Tracks the current scene
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
