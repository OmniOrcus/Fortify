using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeShaker : MonoBehaviour {

    public float deltaY = 5;
    public float yFalloff = 1;
    public float timeScale = 1;
    public float maxTime = 1;
    Vector3 location;
    float timePassed = 0;
    float xTrack;

	// Use this for initialization
	void Start () {
        location = transform.position;
        enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        timePassed += Time.deltaTime;
        transform.position = location + new Vector3( 0, Mathf.Sin(timePassed * timeScale) * xTrack, 0);
        xTrack -= yFalloff * Time.deltaTime;
        if (timePassed > maxTime)
        {
            transform.position = location;
            enabled = false;
        }
    }

    public void Trigger()
    {
        timePassed = 0;
        xTrack = deltaY;
        enabled = true;
        Debug.Log("Shaking " + name);
    }
}
