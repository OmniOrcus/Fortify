using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameManager : MonoBehaviour {

    static EndGameManager current;
    public Text text;

    public static EndGameManager Access() {
        return current;
    }

    public virtual void VictoryText()
    {
        text.text = "Congratuation! You Win!";
    }

    public virtual void LossText(uint winner)
    {
        text.text = "Commiserations. You Lose.\nPlayer " + winner + " Wins.";
    }

	// Use this for initialization
	void Start () {
        current = this;
        gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
