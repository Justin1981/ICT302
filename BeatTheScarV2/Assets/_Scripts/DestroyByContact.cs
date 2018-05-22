using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    public GameObject Explosion;
    //public GameObject PlayerExplosion;
    public int ScoreValue = 1;

    private GameController gameController;

    void Start()
    {
        GameObject gcObj = GameObject.FindGameObjectWithTag("GameController");
        if(gcObj != null)
        {
            gameController = gcObj.GetComponent<GameController>();
        }
        if(gameController == null)
        {
            Debug.Log("'GameControlWarmup' script not found");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.name);
        if(other.CompareTag("Boundary") || other.CompareTag("Enemy") || other.CompareTag("HUD"))
        {
            return;
        }

        if (Explosion != null)
        {
            Instantiate(Explosion, transform.position, transform.rotation);
        }

        //if (other.tag == "Player" || other.tag == "MainCamera")
        if (other.CompareTag("Player"))
        {
            //Instantiate(PlayerExplosion, other.transform.position, other.transform.rotation);
            //gameController.GameOver();
            gameController.PlayerHit();
        }
        else
        {
            gameController.AddScore(ScoreValue);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
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
