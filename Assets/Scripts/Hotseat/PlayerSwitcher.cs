using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerSwitcher : NetworkBehaviour {

    public Player[] players;

    // Use this for initialization
    void Start () {
        Debug.Log("Player find " + Player.Access().name);
        players[0] = GameObject.Find("PlayerConnection").GetComponent<Player>();
        Debug.Log("Player find " + players[0].name);
        if (players[0] == null)
        {
            Debug.Log("Player find " + players[0].name);
            players[0] = GameObject.Find("Player: 1").GetComponent<Player>();
        }

    }

    override public void OnStartAuthority()
    {
        uint index = Player.Access().playerNumber;
        if(index >= players.Length)
        {
            index = 0;
        }
        players[index].TakeControl();

    }

    // Update is called once per frame
    void Update () {
		
	}
}
