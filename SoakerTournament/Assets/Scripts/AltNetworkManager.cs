using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AltNetworkManager : MonoBehaviour {

	//public string connectionIP = "127.0.0.1";
	public int numberofplayers = 7;
	public int connectionPort = 25001;

	private List<HostData> hostList = new List<HostData>();

	private const string typeName = "Network27Game";
	private string gameName = "Custom Game Name";

	private Vector2 scrollPostition = Vector2.zero;

	void Start() { 
		//MasterServer.ipAddress = "remote.stanto.com"; 
		//MasterServer.port = 23466;
	}

	void OnGUI() {

		if (Network.peerType == NetworkPeerType.Disconnected)
		{
			GUI.Label(new Rect(10,10,200,20), "Status: Disconnected");

			if (GUI.Button (new Rect(10, 30, 120, 20), "Quick Join"))
			{
				Debug.Log("Quick Join");
				MasterServer.RequestHostList(typeName);
			}
			/*
			if (GUI.Button (new Rect(10,30,120,20), "Find Server"))
			{
				//Network.Connect(connectionIP, connectionPort);
				MasterServer.RequestHostList(typeName);
			}
			if (GUI.Button (new Rect(10,60,120,20), "Initialize Server"))
			{
				Network.InitializeServer(numberofplayers, connectionPort, !Network.HavePublicAddress());
				MasterServer.RegisterHost(typeName, gameName);
			}
						
			if (hostList != null)
			{
				scrollPostition = GUI.BeginScrollView(new Rect(200,30,400,200), scrollPostition, new Rect(0,0,200,(hostList.Count * 30)));

				for(int i = 0; i < hostList.Count; i++)
				{
					if (GUI.Button(new Rect(0,(30 * i), 200,20), hostList[i].gameName))
						Network.Connect(hostList[i]);
				}

				GUI.EndScrollView();
			}
			*/
		}
		else if (Network.peerType == NetworkPeerType.Client)
		{
			GUI.Label (new Rect(10,10,300,20), "Status: Connected as Client");
			if (GUI.Button (new Rect(10,30,120,20), "Disconnect"))
				Network.Disconnect(200);
		}
		else if (Network.peerType == NetworkPeerType.Server)
		{
			GUI.Label (new Rect(10,10,300,20), "Status: Connected as Server");
			if (GUI.Button (new Rect(10,30,120,20), "Disconnect"))
				Network.Disconnect(200);
		}
	}

	void OnMasterServerEvent(MasterServerEvent msEvent)
	{
		Debug.Log("Master Server Event");

		bool trying_client = false;

		if (msEvent == MasterServerEvent.HostListReceived)
		{
			hostList.Clear();
			hostList.AddRange(MasterServer.PollHostList());

			Debug.Log("hostList loading: " + hostList.Count);

			// New Stuff
			foreach(HostData hd in hostList)
			{
				Debug.Log ("hostData players: " + hd.connectedPlayers + " of " + hd.playerLimit);
				if (hd.connectedPlayers < hd.playerLimit)
				{
					Debug.Log("Connecting to server...");
					Network.Connect(hd);
					trying_client = true;
				}
			}


			Debug.Log("peerType : " + Network.peerType.ToString());

			if (Network.peerType == NetworkPeerType.Disconnected && !trying_client)
			{
				Network.InitializeServer(numberofplayers, connectionPort, !Network.HavePublicAddress());
				MasterServer.RegisterHost(typeName, gameName);
			}

		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
