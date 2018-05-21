using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using System.Collections.Generic;
//NOTES FOR OBJECT SPAWNING
//left and right is x axis, forward and back is z up and down is y
//thing seem to spawn around the holo lens
//TODO: add more waves in, fix up the current waves
public class newCubeScript : MonoBehaviour {
    public Transform Astroid;
    //Text position;
    TextMesh position;
    public float spawnRate;
    public float startTime;
    public float xmin, xmax, ymin, ymax;
    // Use this for initialization
    void Start()
    {
       // startTime = Time.time;
        InvokeRepeating("CreateSpheres", startTime, spawnRate);
    }

    //left is negative, right is positive for x
    //lets assume I have 1.5m to work with, each sphere is 30cm
    //so 6 spheres across - 5 will work, 10 cm gaps max
    //0 is always head height
    //have them duck 2 spheres at most?
    //head *should* be 10cm
    //-0.75 to 1.75

    
    void CreateSpheres()
    {


        //Random.RandomRange(-2, 2);


        //Instantiate(Astroid, new Vector3(Random.Range(-2, 2), Random.Range(-5, 1), 3), Quaternion.identity);
        //uncomment out below when ready to use unity for setting values
        //Vector3 position = new Vector3(Random.Range(xmin, xmax), Random.Range(ymin, ymax), 3);
        Vector3 position = new Vector3(Random.Range(-1f, 1f), Random.Range(-0.5f, 1f), 6);
        Instantiate(Astroid, position, Quaternion.identity);
        
    }

    // Update is called once per frame
    void Update ()
    {
        var headPosition = Camera.main.transform.position;

        //CreateSpheres();
        //position.text = "positionX: " + headPosition.x + "positionY" + headPosition.y + "positionZ" + headPosition.z;
        //GameObject.Find("posText").GetComponentInChildren<TextMesh>().text = "positionX: " + headPosition.x + "positionY" + headPosition.y + "positionZ" + headPosition.z;
    }
}
