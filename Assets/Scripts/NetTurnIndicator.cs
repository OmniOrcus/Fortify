using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetTurnIndicator : TurnPointer2
{
    public GameNodeNetworked monitor;

    void LateUpdate()
    {
        if (Gamemaster.Access().inPlay)
        {
            foreach (SpriteRenderer indicator in indicators)
            {
                indicator.enabled = false;
            }
            if (monitor.hasAuthority)
            {
                indicators[Player.Access().playerNumber - 1].enabled = true;
            } else {
                indicators[2 - Player.Access().playerNumber].enabled = true;
            }
        }
    }
}
