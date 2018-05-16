using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: either the head radius is too small or the atroid radius for collision is too small

public class AstroidScript : MonoBehaviour {
    public float speed;
    
    //Transform target;
    Vector3 target;
	// Use this for initialization
	void Start () {
        target = new Vector3(transform.position.x, transform.position.y, -6);
	}


    void onTriggerEnter(Collision col)
    {
        //Physics.OverlapSphere(transform.position, 0.5f);
        //print("collision detected");
        Debug.Log("collision");
        Destroy(gameObject);
        
        //Destroy(col.gameObject);
    }


    // Update is called once per frame
    void Update () {
        //commented out to see if the atroids are actually spawning

        
        if(transform.position != target)
        {
            Vector3 pos = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            
            GetComponent<Rigidbody>().MovePosition(pos);
        }
        
        //iterate along current for different waypoints
		if(transform.position.z <= -5)
        {
            Destroy(gameObject);
        }

        

	}
}
