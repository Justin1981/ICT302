using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMenuSound : MonoBehaviour {

    public AudioSource source;
    public AudioClip sound;
    public AudioClip hover;

	public void ClickSound()
    {
        source.PlayOneShot(sound);
    }

    public void HoverSound()
    {
        source.PlayOneShot(hover);
    }
	
}
