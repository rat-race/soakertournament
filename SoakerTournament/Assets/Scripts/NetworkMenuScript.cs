using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NetworkMenuScript : MonoBehaviour {

	public GUIStyle customButton;
	
	public int numberOfPlayers = 7;
	public string masterServerIPAddress = "72.52.207.14";
	public int connectionPort = 23466;
	
	private const string typeName = "SoakerTournament";
	private string gameName = "FIX THIS LATER";
	
	private List<HostData> hostList = new List<HostData>();

	// Use this for initialization
	void Start () {
		MasterServer.ipAddress = masterServerIPAddress;
		MasterServer.port = connectionPort;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnGUI () 
	{
		if (Network.peerType == NetworkPeerType.Disconnected)
		{
			if (GUI.Button (new Rect (20, 20, 180, 50), "Quick Join", customButton)) {
				Debug.Log ("Quick Join");
				MasterServer.RequestHostList();
				//Application.LoadLevel("Testbed");
			}
		}
		else if (Network.peerType == NetworkPeerType.Client)
		{
			if (GUI.Button (new Rect (20, 20, 180, 50), "Disconnect"))
				Network.Disconnect(200);		
		}
		else if (Network.peerType == NetworkPeerType.Server)
		{
			if (GUI.Button (new Rect(20,20,180,50), "Stop Hosting"))
				Network.Disconnect(200);
		}
	}
	
	void OnMasterServerEvent(MasterServerEvent msEvent)
	{
		bool trying_client = false;
		
		if (msEvent == MasterServerEvent.HostListReceived)
		{
			hostList.Clear();
			hostList.AddRange(MasterServer.PollHostList());
			
			foreach (HostData hd in hostList)
			{
				if (hd.connectedPlayers < hd.playerLimit)
				{
					Network.Connect(hd);
					trying_client = true;
				}			
			}
			
			if (Network.peerType == NetworkPeerType.Disconnected && !trying_client)
			{
				Network.InitializeServer(numberOfPlayers, connectionPort, !Network.HavePublicAddress());
				MasterServer.RegisterHost(typeName, gameName);
			}
		}
	}
}
