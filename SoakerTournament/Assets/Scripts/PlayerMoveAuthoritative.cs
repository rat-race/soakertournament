using UnityEngine;
using System.Collections;

public class PlayerMoveAuthoritative : MonoBehaviour
{
	public NetworkPlayer theOwner;
	float lastClientHInput = 0f;
	float lastClientVInput = 0f;
	float serverCurrentHInput = 0f;
	float serverCurrentVInput = 0f;

	void Awake()
	{
		if (Network.isClient)
			enabled = false;
	}

	[RPC]
	void SetPlayer(NetworkPlayer player)
	{
		theOwner = player;
		if (player == Network.player)
			enabled = true;
	}

	[RPC]
	void SendMovementInput(float HInput, float VInput)
	{
		serverCurrentHInput = HInput;
		serverCurrentVInput = VInput;
	}

	// Update is called once per frame
	void Update ()
	{
		if (theOwner != null && Network.player == theOwner)
		{
			float HInput = Input.GetAxis("Horizontal");
			float VInput = Input.GetAxis("Vertical");

			if (lastClientHInput != HInput || lastClientVInput != VInput)
			{
				lastClientHInput = HInput;
				lastClientVInput = VInput;

				if (Network.isServer)
					SendMovementInput(HInput, VInput);
				else if (Network.isClient)
					networkView.RPC ("SendMovementInput", RPCMode.Server, HInput, VInput);
			}
		}
		if (Network.isServer)
		{
			Vector3 moveDirection = new Vector3(serverCurrentHInput, 0, serverCurrentVInput);
			float speed = 5f;
			rigidbody.MovePosition(rigidbody.position + moveDirection * speed * Time.deltaTime);
		}
	}

	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
	{
		if (stream.isWriting)
		{
			Vector3 pos = rigidbody.position;
			stream.Serialize(ref pos);

			Vector3 vel = rigidbody.velocity;
			stream.Serialize(ref vel);
		}
		else
		{
			Vector3 receivedPosition = Vector3.zero;
			Vector3 receivedVelocity = Vector3.zero;

			stream.Serialize(ref receivedPosition);
			stream.Serialize(ref receivedVelocity);

			rigidbody.position = receivedPosition;
			rigidbody.velocity = receivedVelocity;
		}
	}
}

