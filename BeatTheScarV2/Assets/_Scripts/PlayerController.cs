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

    //******

    public Transform PlayerPosition;
    public GameObject PlayerExplosion;

    public int StartingHealth = 100;
    
    public Slider HealthSlider;
    public Text PlayerHealthText;


    // The magintude of the force the shot will be fired by
    ///public float FireForce = 1.0f;
    public GameObject ShotPrefab;

    //private GestureRecognizer gestureRecognizer;

    public int CurrentHealth;
    private bool damaged;
    private bool alive;

    // Use this for initialization
    void Start()
    {
        //// Set up GestureRecognizer to register the users finger taps
        //gestureRecognizer = new GestureRecognizer();
        //gestureRecognizer.TappedEvent += GestureRecognizerOnTappedEvent;
        //gestureRecognizer.SetRecognizableGestures(GestureSettings.Tap);
        //gestureRecognizer.StartCapturingGestures();

        CurrentHealth = StartingHealth;
        UpdateHealthBar();
        damaged = false;
        alive = true;
    }

    void Update()
    {
        //GetComponent<Transform>().SetPositionAndRotation(PlayerPosition.position, PlayerPosition.rotation);

        //if (shotPos != null)
        //{
        //    posText.text = "Pos: " + shotPos.position.ToString();
        //}

        posText.text = "Pos: " + PlayerPosition.position.ToString();
    }

    //private void GestureRecognizerOnTappedEvent(InteractionSourceKind source, int tapCount, Ray headRay)
    //{
    //    Shoot();
    //}

    // This method will be publicly accessible to allow for voice-activated firing
    public void Shoot()
    {
        //GetComponent<AudioSource>().Play();

        //Transform baseTransform = ShotPrefab.GetComponent<Transform>();
        //Quaternion baseRotate = baseTransform.rotation * Quaternion.LookRotation(PlayerPosition.forward);

        // instantiate shot at current position and rotation of camera
        GameObject shot = Instantiate(ShotPrefab, PlayerPosition.position, PlayerPosition.rotation) as GameObject;

        // Calculate the direction for firing the shot 5 degrees upward
        //Vector3 fireDirection = Quaternion.AngleAxis(-5, PlayerPosition.right) * PlayerPosition.forward;
        //Rigidbody shotBody = shot.GetComponent<Rigidbody>();

        // Apply a force with desired magnitude in this direction to the shot
        //shotBody.AddForce(PlayerPosition.forward * FireForce);

        //shot.GetComponent<Rigidbody>().AddForce(PlayerPosition.forward * FireForce);

        //shotPos = shot.GetComponent<Rigidbody>().transform;

        shot.GetComponent<AudioSource>().Play();
    }

    public void TakeDamage(int amount)
    {
        damaged = true;

        CurrentHealth -= amount;

        UpdateHealthBar();

        // If the player has lost all it's health and the death flag hasn't been set yet...
        if (CurrentHealth <= 0 && alive)
        {
            // ... it should die.
            Death();

        }

    }

    private void Death()
    {
        // Set the death flag so this function won't be called again.
        alive = false;

        Vector3 pos = PlayerPosition.position + PlayerPosition.forward * 3.0f;  // testing

        Instantiate(PlayerExplosion, pos, PlayerPosition.rotation);
    }

    public bool Alive()
    {
        return alive;
    }

    private void UpdateHealthBar()
    {
        HealthSlider.value = CurrentHealth;
        PlayerHealthText.text = "Health: " + CurrentHealth.ToString();
    }
}
