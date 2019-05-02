using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnPointer2 : MonoBehaviour {

    public SpriteRenderer[] indicators;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
        if (Gamemaster.Access().inPlay)
        {
            foreach (SpriteRenderer indicator in indicators)
            {
                indicator.enabled = false;
            }
            indicators[Player.Access().playerNumber-1].enabled = true;
        }
	}
}
