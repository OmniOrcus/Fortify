using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttDisplay : MonoBehaviour {

    public static AttDisplay current;

    public Text display;
    public Vector3 offset;

    public static AttDisplay Access()
    {
        return current;
    }

    public void SetDisplay(int value)
    {
        if (value > 1000)
        {
            float large = value;
            large /= 1000;
            display.text = large.ToString("#.00");
        }
        else
        {
            display.text = value.ToString();
        }
    }

	// Use this for initialization
	void Start () {
        current = this;
	}
	
	// Update is called once per frame
	void Update () {
        gameObject.transform.position = Input.mousePosition + offset;
	}
}
