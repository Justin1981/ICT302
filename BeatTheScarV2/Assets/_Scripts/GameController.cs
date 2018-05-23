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
    public Text HandText;

    private int score;
    private bool gameOver;
    private bool restart;
    private bool pause;


    private PlayerController player;

    private GestureRecognizer gestureRecognizer;

    private Vector3 handPosition;

    void Start()
    {
        // Setting up events for the interaction manager
        //InteractionManager.InteractionSourceDetected += SourceManager_SourceDetected;
        //InteractionManager.InteractionSourceLost += SourceManager_SourceLost;
        //InteractionManager.InteractionSourcePressed += SourceManager_SourcePressed;
        //InteractionManager.InteractionSourceReleased += SourceManager_SourceReleased;
        InteractionManager.InteractionSourceUpdated += SourceManager_SourceUpdated;
        InteractionManager.InteractionSourceDetected += SourceManager_SourceDetected;
        //InteractionManager.InteractionSourceLost += InteractionManager_InteractionSourceLost;

        InteractionManager.GetCurrentReading();

        HandText.text = "Nothing Detected";

        // Set up GestureRecognizer to register the users finger taps
        gestureRecognizer = new GestureRecognizer();
        gestureRecognizer.TappedEvent += GestureRecognizerOnTappedEvent;
        gestureRecognizer.SetRecognizableGestures(GestureSettings.Tap);
        gestureRecognizer.StartCapturingGestures();

        GameObject gcObj = GameObject.FindGameObjectWithTag("Player");
        //GameOverText.text += "Test for NULL ";
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
        RestartText.text = "";
        GameOverText.text = "";
        score = 0;
        PrintScore();
        StartCoroutine(SpawnWaves());

    }

    private void SourceManager_SourceDetected(InteractionSourceDetectedEventArgs obj)
    {
        if (obj.state.source.kind != InteractionSourceKind.Hand)
        {
            return;
        }
        //trackedHands.Add(state.source.id);

        //var obj = Instantiate(TrackingObject) as GameObject;
        //Vector3 pos;
        //if (state.properties.location.TryGetPosition(out pos))
        //{
        //    obj.transform.position = pos;
        //}
        //trackingObject.Add(state.source.id, obj);
    }



    private void SourceManager_SourceUpdated(InteractionSourceUpdatedEventArgs obj)
    {
        InteractionSourcePose statePose = obj.state.sourcePose;


        obj.state.sourcePose.TryGetPosition(out handPosition);


    }

    void OnDestroy()
    {
        InteractionManager.InteractionSourceUpdated -= SourceManager_SourceUpdated;
        gestureRecognizer.TappedEvent -= GestureRecognizerOnTappedEvent;
    }

    void Update()
    {

        InteractionManager.GetCurrentReading();

        HandText.text = handPosition.ToString();

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
        //togglePause.showPaused();
        //Ps.SetActive(true);
        GetComponent<TogglePause>().showPaused();
    }

    public void Unpause()
    {
        GetComponent<TogglePause>().hidePaused();
        Time.timeScale = 1;
        //togglePause.hidePaused();
        //Ps.SetActive(false);
    }



}
