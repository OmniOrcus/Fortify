using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuGraphicController : MonoBehaviour {

    public Color[] colors;
    public SpriteRenderer[] sprites;
    public float SwitchDelay = 1;
    public AudioSource source;

    int colorTrack = 0;
    int spriteTrack = 0;
    int spriteInc;
    float timePassed= 0;

	// Use this for initialization
	void Start () {
        spriteInc = Random.Range(1, 3);
	}
	
	// Update is called once per frame
	void Update () {
        timePassed += Time.deltaTime;
        if (timePassed >= SwitchDelay)
        {
            timePassed = 0;
            sprites[spriteTrack].color = colors[colorTrack];
            source.Play();
            spriteTrack += spriteInc;
            if (spriteTrack >= sprites.Length)
            {
                spriteTrack -= sprites.Length;
            }
            colorTrack++;
            if (colorTrack >= colors.Length)
            {
                colorTrack -= colors.Length;
            }
        }
	}
}
