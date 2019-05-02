using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionsManager : MonoBehaviour {

    public GameObject previousButton;
    public GameObject nextButton;
    public GameObject[] panels;
    int displayTrack = 0;

	// Use this for initialization
	void Start () {
        if (displayTrack == 0)
        {
            previousButton.SetActive(false);
        }
        if (displayTrack == panels.Length - 1)
        {
            nextButton.SetActive(false);
        }
        panels[displayTrack].SetActive(true);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Next() {
        if (!previousButton.activeSelf)
        {
            previousButton.SetActive(true);
        }
        panels[displayTrack].SetActive(false);
        displayTrack++;
        panels[displayTrack].SetActive(true);
        if (displayTrack == panels.Length - 1)
        {
            nextButton.SetActive(false);
        }
    }
    public void Previous() {
        if (!nextButton.activeSelf)
        {
            nextButton.SetActive(true);
        }
        panels[displayTrack].SetActive(false);
        displayTrack--;
        panels[displayTrack].SetActive(true);
        if (displayTrack == 0)
        {
            previousButton.SetActive(false);
        }
    }

    void Reset()
    {
        if (!previousButton.activeSelf || !nextButton.activeSelf) {
            previousButton.SetActive(true);
            nextButton.SetActive(true);
        }
    }

}
