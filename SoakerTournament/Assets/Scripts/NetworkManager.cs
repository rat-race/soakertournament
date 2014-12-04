using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NetworkManager : MonoBehaviour {

	private List<HostData> hostList = new List<HostData>();
	
	public int connectionPort = 25001;

	// Use this for initialization
	void Start () {
		//MasterServer.ipAddress = "rogueski.dynamic-dns.net";
		//MasterServer.port = 23466;
		//Debug.Log("Master Server: " + MasterServer.ipAddress + ":" + MasterServer.port);
	}
	
	public void QuickJoin()	{
		Debug.Log ("Quick Join");
		MasterServer.RequestHostList(GameManager.GameTypeName);
	}

    void OnPlayerConnected(NetworkPlayer player)
    {
        Debug.Log("Player " + " connected from " + player.ipAddress + ":" + player.port);

        networkView.RPC("LoadServerLevel", player, GameManager.currentScene);
    }

    [RPC]
    void LoadServerLevel(int level)
    {
        Application.LoadLevel(level);
    }
	
	void OnMasterServerEvent(MasterServerEvent msEvent) {
		Debug.Log ("Master Server Event");
		
		bool tryingClient = false;
		
		if (msEvent == MasterServerEvent.HostListReceived)
		{
			hostList.Clear();
			hostList.AddRange(MasterServer.PollHostList());
			
			Debug.Log("Host list loading: " + hostList.Count);
			
			foreach(HostData hd in hostList)
			{
				Debug.Log ("Host data player: " + hd.connectedPlayers + " of " + hd.playerLimit);
				
				if (hd.connectedPlayers < hd.playerLimit)
				{
					Debug.Log("Connecting to server...");
					Network.Connect(hd);
					tryingClient = true;
				}
			}
			
			if (Network.peerType == NetworkPeerType.Disconnected && !tryingClient)
			{
				Debug.Log("Initialising Server...");
				Network.InitializeServer(GameManager.NumberOfPlayers, connectionPort, !Network.HavePublicAddress());
				MasterServer.RegisterHost(GameManager.GameTypeName, GameManager.GameName);
				
						
				// Add code here to load GameManager.currentScene via Application.LoadLevel
                Application.LoadLevel(GameManager.currentScene);
			}
		}
	}
}
