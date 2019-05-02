using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BackTrackManager : MonoBehaviour {

    public AudioClip[] songs;
    AudioSource source;
    uint track = 0;

	// Use this for initialization
	void Start () {
        source = GetComponent<AudioSource>();
        source.PlayOneShot(songs[track]);
	}
	
	// Update is called once per frame
	void Update () {
		if (!source.isPlaying)
        {
            track++;
            if (track >= songs.Length) { track = 0; }
            source.PlayOneShot(songs[track]);
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            source.mute = !source.mute;
        }
    }
}
