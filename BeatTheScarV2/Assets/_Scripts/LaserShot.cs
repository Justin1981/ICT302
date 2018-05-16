using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserShot : MonoBehaviour {

    // On collision method
    private void OnCollisionEnter(Collision collision)
    {
        // Play audio for collision
        //GetComponent<AudioSource>().Play();

        // make the arrows Rigidbody "kinematic", to freeze its movement when it collides with something
        GetComponent<Rigidbody>().isKinematic = true;

        // Child its trasnform component to the game object it collided with, so the arrow moves with the object
        transform.parent = collision.transform;
    }
}
