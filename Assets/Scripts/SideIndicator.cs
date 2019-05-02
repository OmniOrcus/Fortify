using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideIndicator : MonoBehaviour {

    public GameObject[] indicators;

	// Use this for initialization
	void Awake () {
        Select();

    }

    void Select()
    {
        if (Player.Access() == null)
        {
            Debug.Log("Waiting for Player Assignment");
            Invoke("Select", 0.5f);
        }else
        {
            Debug.Log("Turning off " + (2 - Player.Access().GetPlayerNumber()));
            indicators[Player.Access().GetPlayerNumber() - 1].SetActive(true);
        }
    }
	
}
