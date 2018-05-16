using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

public class Bow : MonoBehaviour
{

    // The magintude of the force the arrow will be fired by
    public float fireForce = 1.0f;
    public GameObject arrowPrefab;

    private GestureRecognizer gestureRecognizer;

	// Use this for initialization
	void Start ()
    {
        // Set up GestureRecognizer to register the users finger taps
        gestureRecognizer = new GestureRecognizer();
        gestureRecognizer.TappedEvent += GestureRecognizerOnTappedEvent;
        gestureRecognizer.SetRecognizableGestures(GestureSettings.Tap);
        gestureRecognizer.StartCapturingGestures();
		
	}

    private void GestureRecognizerOnTappedEvent(InteractionSourceKind source, int tapCount, Ray headRay)
    {
        Shoot();
    }

    // This method will be publicly accessible to allow for voice-activated firing
    public void Shoot()
    {
        GetComponent<AudioSource>().Play();

        // instantiate arrow at current position and rotation of camera
        GameObject arrow = Instantiate(arrowPrefab, transform.position, transform.rotation) as GameObject;

        // Calculate the direction for firing the arrow 5 degrees upward
        Vector3 fireDirection = Quaternion.AngleAxis(-5, transform.right) * transform.forward;
        Rigidbody arrowBody = arrow.GetComponent<Rigidbody>();

        // Apply a force with desired magnitude in this direction to the arrow
        arrowBody.AddForce(fireDirection * fireForce);
    }

}
