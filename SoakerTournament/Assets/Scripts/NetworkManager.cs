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
					
					// Add code here to load level that the server is currently running
					// ????????????????????????????????????????????????????????????????
				}
			}
			
			if (Network.peerType == NetworkPeerType.Disconnected && !tryingClient)
			{
				Debug.Log("Initialising Server...");
				Network.InitializeServer(GameManager.NumberOfPlayers, connectionPort, !Network.HavePublicAddress());
				MasterServer.RegisterHost(GameManager.GameTypeName, GameManager.GameName);
				
						
				// Add code here to load GameManager.currentScene via Application.LoadLevel		
			}
		}
	}
}
