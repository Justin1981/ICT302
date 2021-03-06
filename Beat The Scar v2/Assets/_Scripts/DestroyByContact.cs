﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class DestroyByContact : MonoBehaviour
{
    public GameObject Explosion;
    public GameObject PlayerExplosion;
    public int ScoreValue = 1;

    private GameController gameController;


    public Text PlayerCollision;

    void Start()
    {
        GameObject gcObj = GameObject.FindGameObjectWithTag("GameController");
        if(gcObj != null)
        {
            gameController = gcObj.GetComponent<GameController>();
        }
        if(gameController == null)
        {
            Debug.Log("'GameController' script not found");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.name);
        if(other.CompareTag("Boundary") || other.CompareTag("Enemy"))
        {
            return;
        }

        if (Explosion != null)
        {
            Instantiate(Explosion, transform.position, transform.rotation);
        }

        //if (other.tag == "Player" || other.tag == "MainCamera")
        if (other.CompareTag("MainCamera"))
        {
            Instantiate(PlayerExplosion, other.transform.position, other.transform.rotation);
            gameController.GameOver();
        }

        gameController.AddScore(ScoreValue);
        Destroy(other.gameObject);
        Destroy(gameObject);
    }
/**
    private void OnCollisionEnter(Collision collision)
    {
        Collider other = collision.gameObject.GetComponent<Collider>();
        //Debug.Log(other.name);
        if (other.tag == "Boundary")
        {
            return;
        }
        Destroy(other.gameObject);
        Destroy(gameObject);
    }
    **/
}
