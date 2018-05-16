using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverEnemy : MonoBehaviour
{
    public float Speed = 1.0f;


    public float Smoothing;
    public float Dodge;
    public Vector2 StartWait;
    public Vector2 ManeuverTime;
    public Vector2 ManeuverWait;
    public float Tilt;


    private float targetManeuver;
    private Rigidbody rb;
    private float currentSpeed;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * Speed;
        currentSpeed = rb.velocity.z;
        StartCoroutine(Evade());

    }

    IEnumerator Evade()
    {
        yield return new WaitForSeconds(Random.Range(StartWait.x, StartWait.y));

        while (true)
        {
            targetManeuver = Random.Range(1, Dodge) * -Mathf.Sign(transform.position.x);
            yield return new WaitForSeconds(Random.Range(ManeuverTime.x, ManeuverTime.y));
            targetManeuver = 0;
            yield return new WaitForSeconds(Random.Range(ManeuverWait.x, ManeuverWait.y));
        }
    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        float newManeuver = Mathf.MoveTowards(rb.velocity.x, targetManeuver, Time.deltaTime * Smoothing);
        rb.velocity = new Vector3(newManeuver, 0.0f, currentSpeed);

        //rb.position = new Vector3
        //(
        //    Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
        //    0.0f,
        //    Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
        //);

        rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -Tilt);

    }
}
