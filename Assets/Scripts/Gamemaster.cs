using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Gamemaster : NetworkBehaviour {

    static protected Gamemaster game;

    public NetworkIdentity[] controlledObjects;
    public List<NetworkConnection> playerCons = new List<NetworkConnection>();
    public GameNodeNetworked[] startNodes;

    [SyncVar]
    public int turn;

    public int maxPlayers;

    [SyncVar]
    public bool inPlay = false;

    // Use this for initialization
    void Start () {
        game = this;
        Debug.Log("Gamemaster Start!");
	}

    static public Gamemaster Access() {
        return game;
    }

    
    public uint RegisterPlayer(NetworkConnection player) {
        Debug.Log("Player connection Check: " + (player != null));
        playerCons.Add(player);
        Debug.Log("New Player recorded; Player Count: " + playerCons.Count);
        startNodes[playerCons.Count - 1].SetOwner((uint)playerCons.Count);
        startNodes[playerCons.Count - 1].SetStrength(1);
        if (playerCons.Count == maxPlayers) { Begin();  }
        return (uint)playerCons.Count;
    }

    public virtual void Begin()
    {
        foreach (GameNodeNetworked starter in startNodes)
        {
            starter.RpcObserverInform();
        }
        foreach (NetworkIdentity netOb in controlledObjects)
        {
            netOb.AssignClientAuthority(playerCons[turn]);
            Debug.Log(netOb.name + " now controlled by " + turn);
        }
        inPlay = true;
    }

    public virtual void NextTurn()
    {
        Debug.Log("Turn Change: Was " + turn);
        if (inPlay)
        {
            foreach (NetworkIdentity netOb in controlledObjects)
            {
                netOb.RemoveClientAuthority(playerCons[turn]);
            }
            turn++;
            if (turn >= playerCons.Count)
            {
                turn = 0;
            }
            Debug.Log("New Authority: " + turn + "; Is it null? " + (playerCons[turn] == null));
            foreach (NetworkIdentity netOb in controlledObjects)
            {
                netOb.AssignClientAuthority(playerCons[turn]);
                Debug.Log(netOb.name + " now controlled by " + turn);
            }
        }
    }

    public bool Wincheck() {
        bool win = true;
        for(uint i = 1; i < startNodes.Length; i++)
        {
            win = win && (startNodes[i].GetOwner() == startNodes[i - 1].GetOwner());
        }
        return win;
    }

    /*[Command]
    public void CmdAssignControl(int _turn)
    {
        foreach (NetworkIdentity netOb in controlledObjects)
        {
            netOb.AssignClientAuthority(playerCons[turn]);
            Debug.Log(netOb.name + " now controlled by " + turn);
        }
    }

    [ClientRpc]
    public void RpcAssignControl(int _turn)
    {
        foreach (NetworkIdentity netOb in controlledObjects)
        {
            netOb.AssignClientAuthority(playerCons[turn]);
            Debug.Log(netOb.name + " now controlled by " + turn);
        }
    }*/

}
