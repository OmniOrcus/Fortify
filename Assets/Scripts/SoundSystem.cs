using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundSystem : MonoBehaviour {

    static SoundSystem current;

    public static SoundSystem Access()
    {
        return current;
    }

    public AudioClip[] clips;
    AudioSource source;

	// Static assignment and source link
	void Start () {
        current = this;
        source = GetComponent<AudioSource>();
	}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M)){
            source.mute = !source.mute;
        }
    }

    public void PlayClip(uint index)
    {
        if (source.isPlaying)
        {
            source.Stop();
        }
        source.PlayOneShot(clips[index]);
    }

}
