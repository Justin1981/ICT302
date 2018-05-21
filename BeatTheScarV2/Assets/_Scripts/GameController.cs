using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEngine.XR.WSA.Input;

using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject[] Hazards;
    public Vector3 SpawnValues;
    public int HazardCount;
    public float SpawnWait;
    public float StartWait;
    public float WaveWait;

    public Text ScoreText;
    public Text RestartText;
    public Text GameOverText;
    //public Text PlayerHealthText;

    private int score;
    private bool gameOver;
    private bool restart;
    private bool pause;
    //private int playerHealth;

    private PlayerController player;

    private GestureRecognizer gestureRecognizer;

    private GameObject playerHitPlane;
    private bool playerHitPlaneOn;
    private int playerHitPlaneCount;

    void Start()
    {
        // Set up GestureRecognizer to register the users finger taps
        gestureRecognizer = new GestureRecognizer();
        gestureRecognizer.TappedEvent += GestureRecognizerOnTappedEvent;
        gestureRecognizer.SetRecognizableGestures(GestureSettings.Tap);
        gestureRecognizer.StartCapturingGestures();

        GameObject gcObj = GameObject.FindGameObjectWithTag("Player");
        if (gcObj != null)
        {
            player = gcObj.GetComponent<PlayerController>();
        }
        if (player == null)
        {
            Debug.Log("'PlayerController' script not found");
        }

        playerHitPlane = GameObject.FindGameObjectWithTag("PlayerHitPlane");
        playerHitPlaneOn = false;
        playerHitPlaneCount = 0;


        gameOver = false;
        restart = false;
        pause = false;
        RestartText.text = "";
        GameOverText.text = "";
        score = 0;
        PrintScore();
        StartCoroutine(SpawnWaves());

        //playerHealth = 100;
        //PrintPlayerHealth();

    }

    void Update()
    {
        //if(restart)
        //{
        //    if(Input.GetKeyDown(KeyCode.R))
        //    {
        //        //Application.LoadLevel(Application.loadedLevel);
        //        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //    }
        //}

        //if (!player.Alive() && !gameOver)
        //{
        //    GameOver();
        //}

        //if(playerHitPlaneOn && !playerHitPlane.activeSelf)
        //{
        //    playerHitPlane.SetActive(true);
        //}

        //if(playerHitPlane.activeSelf)
        //{
        //    playerHitPlaneCount++;
        //}

        //if(playerHitPlaneCount > 500)
        //{
        //    playerHitPlaneOn = false;
        //    playerHitPlane.SetActive(false);
        //    playerHitPlaneCount = 0;
        //}

        

    }


    private void GestureRecognizerOnTappedEvent(InteractionSourceKind source, int tapCount, Ray headRay)
    {
        if (!gameOver)
        {
            player.Shoot();
        }
    }

    public void GameOver()
    {
        //Instantiate(player.PlayerExplosion, player.transform.position, player.transform.rotation);

        GameOverText.text = "Game Over";
        gameOver = true;
    }

    private IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(StartWait);

        while (true)
        {
            for (int i = 0; i < HazardCount; i++)
            {
                GameObject hazard = Hazards[Random.Range(0, Hazards.Length)];
                float x = Random.Range(-SpawnValues.x, SpawnValues.x);
                Vector3 spawnPosition = new Vector3(x, SpawnValues.y, SpawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;

                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(SpawnWait);
            }
            yield return new WaitForSeconds(WaveWait);

            if(gameOver)
            {
                RestartText.text = "Say 'Restart' to Restart Game";
                restart = true;
                break;
            }
        }
    }

    public void AddScore(int points)
    {
        score += points;
        PrintScore();
    }

    private void PrintScore()
    {
        ScoreText.text = "Score: " + score.ToString();
    }

    public void PlayerHit()
    {

        if(player.Alive())
        {
            player.TakeDamage(10);
            //playerHitPlaneOn = true;
            //playerHitPlane.SetActive(true);
            StartCoroutine(FlashPlayerHitPlane());

        }

        if (!player.Alive() && !gameOver)
        {
            GameOver();
        }

    }

    private IEnumerator FlashPlayerHitPlane()
    {
        playerHitPlane.SetActive(true);
        yield return new WaitForSeconds(5);
        playerHitPlane.SetActive(false);
    }


    public void Restart()
    {
        if(restart)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void Pause()
    {
        if (Time.timeScale == 1)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }
}
