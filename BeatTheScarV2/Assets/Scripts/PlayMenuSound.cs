using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMenuSound : MonoBehaviour {

    public AudioSource source;

	void Start()
    {
        source = GetComponent<AudioSource>();
    }
	
	public void OnHover()
    {
        source.Play();
    }
}
