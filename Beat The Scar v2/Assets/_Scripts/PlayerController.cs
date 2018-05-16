using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //DEBUG Vars
    public Text posText;
    private Transform shotPos = null;

    public Text PlayerPos;

    //******

    //public Transform PlayerPosition;

    private Transform ShotSpawn;
    

    // The magintude of the force the shot will be fired by
    public float FireForce = 1.0f;
    public GameObject ShotPrefab;

    private GestureRecognizer gestureRecognizer;

    // Use this for initialization
    void Start()
    {
        // Set up GestureRecognizer to register the users finger taps
        gestureRecognizer = new GestureRecognizer();
        gestureRecognizer.TappedEvent += GestureRecognizerOnTappedEvent;
        gestureRecognizer.SetRecognizableGestures(GestureSettings.Tap);
        gestureRecognizer.StartCapturingGestures();

        
    }

    void Update()
    {
        PlayerPos.text = "Player Pos: " + GetComponent<Transform>().position.ToString();

        ShotSpawn = GetComponent<Transform>();

        if (shotPos != null)
        {
            posText.text = "Pos: " + ShotSpawn.position.ToString();
        }

        
    }

    private void GestureRecognizerOnTappedEvent(InteractionSourceKind source, int tapCount, Ray headRay)
    {
        Shoot();
    }

    // This method will be publicly accessible to allow for voice-activated firing
    public void Shoot()
    {
        //GetComponent<AudioSource>().Play();

        //Transform baseTransform = ShotPrefab.GetComponent<Transform>();
        //Quaternion baseRotate = baseTransform.rotation * Quaternion.LookRotation(ShotSpawn.forward);

        // instantiate shot at current position and rotation of camera
        GameObject shot = Instantiate(ShotPrefab, ShotSpawn.position, ShotSpawn.rotation) as GameObject;
        //////GameObject shot = Instantiate(ShotPrefab, GetComponent<Transform>().position, GetComponent<Transform>().rotation) as GameObject;

        // Calculate the direction for firing the shot 5 degrees upward
        //Vector3 fireDirection = Quaternion.AngleAxis(-5, ShotSpawn.right) * ShotSpawn.forward;
        //Rigidbody shotBody = shot.GetComponent<Rigidbody>();

        // Apply a force with desired magnitude in this direction to the shot
        //shotBody.AddForce(ShotSpawn.forward * FireForce);

        //shot.GetComponent<Rigidbody>().AddForce(ShotSpawn.forward * FireForce);

        //shotPos = shot.GetComponent<Rigidbody>().transform;

        shot.GetComponent<AudioSource>().Play();
    }
}
