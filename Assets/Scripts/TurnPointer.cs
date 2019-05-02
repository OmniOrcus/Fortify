using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnPointer : MonoBehaviour
{
    public Text text;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Player.Access().playerNumber == 1)
        {
            text.text = "Player 1's turn -->          ";
            text.alignment = TextAnchor.MiddleRight;
        }
        if (Player.Access().playerNumber == 2)
        {
            text.text = "          <-- Player 2's turn";
            text.alignment = TextAnchor.MiddleLeft;
        }
    }
}
