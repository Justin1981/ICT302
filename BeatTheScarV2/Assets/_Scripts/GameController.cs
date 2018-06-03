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
    public int TapLimitLeft = 5;
    public int TapLimitRight = 5;
    

    public Text ScoreText;
    public Text RestartText;
    public Text GameStateText;
    public Text AmmoCountText;
    public Text RelativePositionText;
    //public Text HandText;

    private int score;
    private bool gameOver;
    private bool restart;
    private bool pause;
    private bool tapCountOk;
    private int tapCount;
    private int tapSourceId;

    private PlayerController player;

    private enum Hand { LEFT = 1, RIGHT};
    private Hand activeHand;

    //private Vector3 handPosition;

    void Start()
    {
        GameObject gcObj = GameObject.FindGameObjectWithTag("Player");
        //GameStateText.text += "Test for NULL ";
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
        pause = false;
        tapCountOk = false;
        RestartText.text = "";
        GameStateText.text = "";
        AmmoCountText.text = "AMMO: RAISE YOUR HAND INTO THE TAP POSITION TO RELOAD";
        score = 0;
        tapCount = 0;
        tapSourceId = 0;


        UpdateScoreText();
        StartCoroutine(SpawnWaves());

    }

    void OnDestroy()
    {
        //InteractionManager.InteractionSourceUpdated -= SourceManager_SourceUpdated;
        //gestureRecognizer.TappedEvent -= GestureRecognizerOnTappedEvent;
    }

    void Update()
    {

        //////////////////InteractionManager.GetCurrentReading();

        //HandText.text = handPosition.ToString();

    }


    public void PlayerShoot()
    {
        if (!gameOver)
        {
            //if (tapCountOk)
            if(tapCount > 0)
            {
                player.Shoot();
                tapCount--;
                //tapCountOk = PlayerTapCountTest();
                //if(!tapCountOk)
                if(tapCount <= 0)
                {
                    AmmoCountText.text = "OUT OF AMMO - YOU MUST SWAP HANDS TO RELOAD";
                }
                else
                {
                    AmmoCountText.text = "AMMO: " + tapCount.ToString();
                }
            }

        }
    }



    //private bool PlayerTapCountTest()
    //{
    //    if (activeHand == Hand.LEFT && tapCount < TapLimitLeft)
    //    {
    //        return true;
    //    }
    //    else if (activeHand == Hand.RIGHT && tapCount < TapLimitRight)
    //    {
    //        return true;
    //    }
    //    return false;
    //}

    public void GameOver()
    {

        GameStateText.text = "Game Over";
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
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        ScoreText.text = "Score: " + score.ToString();
    }


    public void PlayerHit()
    {
        if (player.Alive())
        {
            player.TakeDamage(10);
        }

        if (!player.Alive() && !gameOver)
        {
            GameOver();
        }

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
        Time.timeScale = 0;
    }

    public void Unpause()
    {
        Time.timeScale = 1;
    }

    public void PlayerUpdateHand(int sourceId, Vector3 handPos)
    {
        //if (!gameOver)
        //{
        if (sourceId != tapSourceId)
        {
            tapSourceId = sourceId;

            Hand newHand = 0;

            Vector3 relativePos = player.PlayerPosition.InverseTransformPoint(handPos);
        RelativePositionText.text = "RELATIVE POS: " + relativePos.ToString();

            int tempCount = 0;

            if (relativePos.x <= 0.0f)
            {
                GameStateText.text = " LEFT ";
                newHand = Hand.LEFT;
                tempCount = TapLimitLeft;
            }
            else if (relativePos.x > 0.0f)
            {
                GameStateText.text = " RIGHT ";
                newHand = Hand.RIGHT;
                tempCount = TapLimitRight;
            }

            if (newHand != activeHand)
            {
                activeHand = newHand;
                tapCount = tempCount;
                //tapCountOk = true;
                AmmoCountText.text = "AMMO: " + tapCount.ToString();
            }
        }
        //}
    }

}
