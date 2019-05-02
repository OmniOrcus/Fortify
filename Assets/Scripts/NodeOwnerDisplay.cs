using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeOwnerDisplay : MonoBehaviour, Observer {

    public GameNodeNetworked node;
    public Color[] colors;
    public SpriteRenderer rend;

    public void Look()
    {
        rend.color = colors[node.GetOwner()];
    }

    // Use this for initialization
    void Start () {
        node.AddObserver(this);
	}

    void Awake() {
        Look();
    }

}
