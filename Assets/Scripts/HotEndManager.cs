using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotEndManager : EndGameManager
{

    public override void VictoryText()
    {
        text.text = "Game Over!\nPlayer " + Player.Access().playerNumber + " Wins!";
    }
}
