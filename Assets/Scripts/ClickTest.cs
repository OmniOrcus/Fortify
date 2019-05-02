using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickTest : MonoBehaviour {

    public Color player1;
    public Color player2;
    SpriteRenderer rend;

    // Use this for initialization
    void Start () {
        rend = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseDown() {
        rend.color = player1;
    }

}
