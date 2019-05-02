using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionMapper : MonoBehaviour {

    public GameObject nodeHolder;
    public GameObject linkSprite;
    GameNodeNetworked[] nodes;

    List<NodeConnection> connections = new List<NodeConnection>();

	// Use this for initialization
	void Start () {
        nodes = nodeHolder.transform.GetComponentsInChildren<GameNodeNetworked>(true);
        Debug.Log("Mapper: " + nodes.Length + " Nodes detected");
        foreach (GameNodeNetworked node in nodes)
        {
            foreach (GameNodeNetworked connection in node.connentions)
            {
                connections.Add(new NodeConnection(node, connection));
            }
        }
        RemoveRepeats();
        foreach (NodeConnection connection in connections)
        {
            Debug.Log("Mapper: Drawing connection " + connection.name);
           Instantiate(linkSprite,
                ((connection.lower.transform.localPosition + connection.higher.transform.localPosition) / 2) + Vector3.forward,
                LinkRotate(connection),
                transform);

        }

	}

    Quaternion LinkRotate(NodeConnection connection)
    {
        return Quaternion.LookRotation(Vector3.forward, Vector3.Cross((connection.higher.transform.position - connection.lower.transform.position), Vector3.forward));
    }

    void RemoveRepeats() {
        Debug.Log("Mapper: Removing Repeats");
        for (int i = 0; i < connections.Count; i++)
        {
            for (int j = i+1; j < connections.Count; j++)
            {
                if(connections[i].name == connections[j].name)
                {
                    connections.Remove(connections[j]);
                    j--;
                }
            }
        }
    }

	// Update is called once per frame
	void Update () {
		
	}
}
