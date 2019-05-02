using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour
{
    static Player self = null;

    public uint playerNumber;
    NetworkIdentity id;


	// Use this for initialization
	void Start () {
        Debug.Log("Player Created");
        if (isLocalPlayer)
        {
            self = this;
            Debug.Log("Setting player Singleton");
        }
        id = GetComponent<NetworkIdentity>();
        if (id == null)
        {
            Debug.LogError("ERR: Player Network Id null!");
            return;
        }
        if (isLocalPlayer)
        {
            Debug.Log("GMLinking");
            CmdLinkToGamemaster();
        }
    }

    void Awake()
    {
        
    }

    [Command]
    void CmdLinkToGamemaster() {
        if (Gamemaster.Access() == null)
        {
            Debug.Log("Waiting for Gamemaster");
            Invoke("LinkToGamemaster", 1);
        } else {
            playerNumber = Gamemaster.Access().RegisterPlayer(id.connectionToClient);
            RpcSetPlayerNumber(playerNumber);
        }
    }

    [ClientRpc]
    void RpcSetPlayerNumber(uint _player)
    {
        if (isLocalPlayer)
        {
            playerNumber = _player;
            gameObject.name = "Player: " + playerNumber.ToString();
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public uint GetPlayerNumber() {
        return playerNumber;
    }

    public void TakeControl()
    {
        self = this;
    }

    public static Player Access()
    {
        return self;
    }
}
