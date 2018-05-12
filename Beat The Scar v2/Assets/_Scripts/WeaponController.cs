using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject Shot;
    public Transform ShotSpawn;
    public float FireRate = 1.0f;
    public float Delay = 1.0f;

	// Use this for initialization
	void Start ()
    {
        InvokeRepeating("Fire", Delay, FireRate);
	}
	
	private void Fire()
    {
        GameObject shot = Instantiate(Shot, ShotSpawn.position, ShotSpawn.rotation) as GameObject;
        shot.GetComponent<AudioSource>().Play();
    }
}
