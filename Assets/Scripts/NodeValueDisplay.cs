using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeValueDisplay : MonoBehaviour, Observer {

    public GameNodeNetworked node;
    public float UImapValue = 31.5f;
    public Text text;
    RectTransform rec;

    public void Look()
    {
        if (node.GetStrength() > 1000) {
            float large = node.GetStrength();
            large /= 1000;
            text.text = large.ToString("#.00");
        }
        else {
            text.text = node.GetStrength().ToString();
        }            
    }

    // Use this for initialization
    void Start () {
        node.AddObserver(this);
        float asp = ChooseMapScale();
        rec = GetComponent<RectTransform>();
        rec.localPosition = new Vector2(node.transform.position.x * asp, node.transform.position.y * asp);
        name += node.name;
        
    }

    void Awake()
    {
        //Debug.Log("Node Exists: " + (node != null));
        Look();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    float ChooseMapScale() {
        float asp = (Screen.currentResolution.width / Screen.currentResolution.height);
        if (asp == (4 / 3)) { }
        return UImapValue;
    }
}
