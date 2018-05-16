using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {

    public GameObject pauseObjects;
    public float timeLeft = 0;

	// Use this for initialization
	void Start () {
        timeLeft = 10;
        hidePaused();
	}
	
	// Update is called once per frame
	void Update () {
        timeLeft -= Time.deltaTime;
        Debug.Log(timeLeft);
        if(timeLeft <= 0)
        {
            GameOver();
        }
	}

    void GameOver()
    {
        //pauseObjects = GameObject.FindGameObjectWithTag("ShowOnPause");
        //Time.timeScale = 0;
        showPaused();
        
        

    }

    public void showPaused()
    {
        pauseObjects.SetActive(true);
    }

    public void hidePaused()
    {
        pauseObjects.SetActive(false);
    }
}
