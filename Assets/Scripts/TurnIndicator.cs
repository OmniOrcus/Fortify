using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnIndicator : MonoBehaviour, Observer {

    public GameNodeNetworked monitoredNode;


    public void Look()
    {
        Debug.Log("Turn State: " + monitoredNode.hasAuthority);
        gameObject.SetActive(monitoredNode.hasAuthority);
    }

    // Use this for initialization
    void Start () {
        monitoredNode.AddObserver(this);
        Look();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
