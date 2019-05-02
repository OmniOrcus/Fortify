using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotseatMaster : Gamemaster
{

    public int simedPlayers;

    public override void Begin()
    {
        Debug.Log("HS: Begining");
        for (uint i = 0; i < simedPlayers; i++)
        {
            Debug.Log("HS: Setting " + startNodes[i].name + " to " + (i + 1));
            startNodes[i].SetOwner(i + 1);
            startNodes[i].SetStrength(1);
        }
        base.Begin();
        Player.Access().playerNumber = 1;
    }

    override public void NextTurn()
    {
        Player.Access().playerNumber++;
        if(Player.Access().playerNumber > simedPlayers)
        {
            Player.Access().playerNumber = 1;
        }

    }

}
