using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioSettings : MonoBehaviour {

    public AudioMixer mixer;

    // Use this for initialization
    void Start () {
        //AudioMixerGroup.
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetMasterVolume(float value)
    {
        mixer.SetFloat("MasterVol", value);
    }

    public void SetBackVolume(float value)
    {
        mixer.SetFloat("BackVol", value);
    }

    public void SetFXVolume(float value)
    {
        mixer.SetFloat("FXVol", value);
    }
}
