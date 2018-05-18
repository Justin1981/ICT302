using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public Text PlayerHealthText;

    private int score;
    private bool gameOver;
    private bool restart;
    private int playerHealth;

    private PlayerController player;

    void Start()
    {
        GameObject gcObj = GameObject.FindGameObjectWithTag("Player");
        if (gcObj != null)
        {
            player = gcObj.GetComponent<PlayerController>();
        }
        if (player == null)
        {
            Debug.Log("'PlayerController' script not found");
        }


        gameOver = false;
        restart = false;
        RestartText.text = "";
        GameOverText.text = "";
        score = 0;
        PrintScore();
        StartCoroutine(SpawnWaves());

        playerHealth = 100;
        PrintPlayerHealth();

    }

    void Update()
    {
        if(restart)
        {
            if(Input.GetKeyDown(KeyCode.R))
            {
                //Application.LoadLevel(Application.loadedLevel);
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
       
    }

    public void GameOver()
    {
        Instantiate(player.PlayerExplosion, player.transform.position, player.transform.rotation);

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
                RestartText.text = "Press 'R' for Restart";
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
        playerHealth -= 10;

        if(playerHealth <= 0)
        {
            GameOver();
        }

        PrintPlayerHealth();
    }

    private void PrintPlayerHealth()
    {
        PlayerHealthText.text = "Health: " + playerHealth.ToString();
    }
}
