using UnityEngine;
using System.Collections;
using System;

public class SpawnScript : MonoBehaviour {

	public GameObject playerPrefab;
	public ArrayList playerScripts = new ArrayList();

	Hashtable players = new Hashtable();

	void OnServerInitialized()
	{
		SpawnPlayer(Network.player);
	}

	void OnPlayerConnected(NetworkPlayer player)
	{
		SpawnPlayer(player);
	}

	void OnPlayerDisconnected(NetworkPlayer player)
	{
		RemovePlayer(player);
	}

	void OnDisconnectedFromServer(NetworkDisconnection info)
	{
		GameObject[] gameobjects = GameObject.FindGameObjectsWithTag("Player");
		foreach (GameObject go in gameobjects)
			Destroy(go);
	}

	void SpawnPlayer(NetworkPlayer player)
	{
		string tempPlayerString = player.ToString();
		int playerNumber = Convert.ToInt32(tempPlayerString);

	 	GameObject playerObject = (GameObject)Network.Instantiate(playerPrefab, transform.position, transform.rotation, playerNumber);
		playerScripts.Add(playerObject.transform.GetComponent("PlayerMoveAuthoritative"));

		players.Add(playerNumber, playerObject);

		NetworkView theNetworkView = playerObject.networkView;
		theNetworkView.RPC("SetPlayer", RPCMode.AllBuffered, player);
	}

	void RemovePlayer(NetworkPlayer player)
	{
		string tempPlayerString = player.ToString();
		int playerNumber = Convert.ToInt32(tempPlayerString);

		GameObject go = (GameObject)players[playerNumber];
		Network.RemoveRPCs(go.networkView.viewID);
		Network.Destroy(go);
		players.Remove(player);
	}
}
