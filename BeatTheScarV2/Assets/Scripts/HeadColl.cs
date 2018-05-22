using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
//using System.Runtime.Serialization.Formatters.Binary;
/////////////////////////////////////////////////////////////////////////

using UnityEngine.SceneManagement;

public class HeadColl : MonoBehaviour {
    public int health;
    public Text hp;

    public Transform PlayerPosition;

    // Use this for initialization
    void Start () {

    }
	
    // Update is called once per frame
	void Update () {
        //position of holo lens
        //var headPosition = Camera.main.transform.position;

        var headPosition = PlayerPosition.position;

        //Collider[] hit = Physics.OverlapSphere(transform.position, 0.05f);
        //checking for hit boxes in the area
        //Collider[] hit = Physics.OverlapBox(transform.position, transform.localScale/2);
        Collider[] hit = Physics.OverlapBox(headPosition, transform.localScale / 2);
        if (hit.Length > 0)
        {
            Debug.Log("collision");
            //destroy all hit in thet frame
            for (int i = 0; i < hit.Length; i++)
            {
                Destroy(hit[i].gameObject);
            }
            health++;
        }
        //moves the box so it's always at the head
        gameObject.transform.position = new Vector3(headPosition.x, headPosition.y - transform.localScale.y/2, headPosition.z);
        //update the health, change to times hit maybe?
        hp.text = "Times hit:" + health;
        //hp.transform.position.x = 0;

    }


    //not used can delete, keeping around just in case
    void onTriggerEnter(Collision col)
    {
        health--;
        print("collision detected");
        //Destroy(col.gameObject);
        //col.gameObject.
        //delete the col object?
        //reduce hp by 1? do something to score?
        //col.gameObject.GetComponent(Script).jumptrue = false;
    }
}
