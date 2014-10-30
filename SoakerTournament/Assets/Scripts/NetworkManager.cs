using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NetworkManager : MonoBehaviour {

	private List<HostData> hostList = new List<HostData>();
	
	public int connectionPort = 25001;

	// Use this for initialization
	void Awake () {
		MasterServer.ipAddress = "rogueski.dynamic-dns.net";
		MasterServer.port = connectionPort = 23466;
	}
	
	public void QuickJoin()	{
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
				}
			}
			
			if (Network.peerType == NetworkPeerType.Disconnected && !tryingClient)
			{
				Network.InitializeServer(GameManager.NumberOfPlayers, connectionPort, !Network.HavePublicAddress());
				MasterServer.RegisterHost(GameManager.GameTypeName, GameManager.GameName);			
			}
		}
	}
}
