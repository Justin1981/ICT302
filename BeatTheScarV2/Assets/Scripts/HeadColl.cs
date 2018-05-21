using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
//using System.Runtime.Serialization.Formatters.Binary;
/////////////////////////////////////////////////////////////////////////

using UnityEngine.SceneManagement;

public class HeadColl : MonoBehaviour {
    public int health;
    public TextMesh hp;
    // Use this for initialization
    void Start () {

    }
	
    // Update is called once per frame
	void Update () {
        //position of holo lens
        var headPosition = Camera.main.transform.position;
        //Collider[] hit = Physics.OverlapSphere(transform.position, 0.05f);
        //checking for hit boxes in the area
        Collider[] hit = Physics.OverlapBox(transform.position, transform.localScale/2);
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


        //SaveFile();
    }

    //public void SaveFile()
    //{
    //    string destination = Application.persistentDataPath + "/asteroidSave.dat";
    //    FileStream file;

    //    if (File.Exists(destination)) file = File.OpenWrite(destination);
    //    else file = File.Create(destination);

    //    int data = health;
    //    /////////////////////////////////////////////////////////////////////////
    //    //BinaryFormatter bf = new BinaryFormatter();
    //    //bf.Serialize(file, data);
    //    //file.Close();
    //}


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
