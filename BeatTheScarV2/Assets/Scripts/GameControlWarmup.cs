using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;
using HoloToolkit.Unity.InputModule;

using UnityEngine.SceneManagement;
using UnityEngine.XR.WSA.Input;

public class GameControlWarmup : MonoBehaviour {
    [SerializeField]
    private int currentStretch;
    [SerializeField]
    private float chargeLeft;
    [SerializeField]
    private List<string> listOfEx = new List<string>();
    [SerializeField]
    private int stretchCount;
    private static GameControlWarmup instance;
    [SerializeField]
    private float timer;
    public List<GameObject> stretchObjects = new List<GameObject>();
    private TextMesh nameStretchText;
    private TextMesh timerText;
    private TextMesh stretchCountText;

    public string SceneName;



    /////////////////////////////////////////////////////////////////


    private GestureRecognizer gestureRecognizer;


    ////////////////////////////////////////////////////////////////



    public static GameControlWarmup Instance
    {
        get
        {
            if (instance != null)
            {
                return instance;
            }
            else
            {
                GameObject go = GameObject.FindGameObjectWithTag("GameControlWarmup");
                instance = go.GetComponent<GameControlWarmup>();

                return instance;
            }
        }
    }

	void Start () {

        // Set up GestureRecognizer to register the users finger taps
        gestureRecognizer = new GestureRecognizer();
        gestureRecognizer.TappedEvent += GestureRecognizerOnTappedEvent;
        gestureRecognizer.SetRecognizableGestures(GestureSettings.Tap);
        gestureRecognizer.StartCapturingGestures();

        listOfEx.Add("Hamstring Stretch");
        listOfEx.Add("Back Stretch");
        listOfEx.Add("Arm Stretch");
        chargeLeft = 100.0f;
        currentStretch = 0;
        stretchCount = 0;
        timer = 0.0f;
        foreach (GameObject fooObj in GameObject.FindGameObjectsWithTag("Stretch"))
        {
            stretchObjects.Add(fooObj);
        }
        //if (stretchObjects[0].GetComponent<MeshRenderer>().enabled == false)
        if (stretchObjects[0].transform.GetChild(0).GetComponent<MeshRenderer>().enabled == false)
        {
            stretchObjects[0].transform.GetChild(0).GetComponent<MeshRenderer>().enabled = true;
            //stretchObjects[0].GetComponentInChildren<MeshRenderer>().enabled = true;
            //stretchObjects[0].GetComponent<MeshRenderer>().enabled = true;
        }
        FindTextObjects();
        UpdateUI();
	}

    private void FindTextObjects()
    {
        nameStretchText = GameObject.FindGameObjectWithTag("StretchText").GetComponent<TextMesh>();
        timerText = GameObject.FindGameObjectWithTag("TimerText").GetComponent<TextMesh>();
        stretchCountText = GameObject.FindGameObjectWithTag("CountText").GetComponent<TextMesh>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        UpdateTimer(timer);
    }

    private void UpdateTimer(float timer)
    {
        timerText.text = "Time: " + (int)this.timer;
    }

    public void stretchComplete()
    {    
        if (currentStretch != listOfEx.Count)
        {
            stretchObjects[stretchCount].transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
            currentStretch++;
            stretchCount++;
        }

        InputManager.Instance.PushInputDisable();
        if (stretchCount == listOfEx.Count)
        {
            WarmUpComplete();
            if (!InputManager.Instance.IsInputEnabled)
            {
                InputManager.Instance.PopInputDisable();
            }
        }
        else
        {
            stretchObjects[stretchCount].transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;

            stretchObjects[stretchCount].transform.GetChild(0).GetComponent<MeshRenderer>().enabled = enabled;
            if (!InputManager.Instance.IsInputEnabled)
            {
                InputManager.Instance.PopInputDisable();
            }
        }
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (currentStretch != listOfEx.Count)
        {
            stretchCountText.text = stretchCount + " stretches complete";
            nameStretchText.text = "Current Stretch: " + listOfEx[currentStretch];
        }
    }

    public void WarmUpComplete()
    {
        Debug.Log("Warm Up is Completed");
        //SaveStats(listOfEx);//////////////////////////////////////////////////

        SceneManager.LoadScene(SceneName);


        //Application.Quit(); // CHANGE THIS TO LOAD NEXT SCENE OR CONTINUE SCREEN
        //UnityEditor.EditorApplication.isPlaying = false;


    }

    //private void SaveStats(List<string> listOfEx)
    //{
    //    string path = "Assets/saveData.txt";
    //    if (File.Exists(path))
    //    {
    //        StreamWriter writer = new StreamWriter(path, true);
    //    }
    //    else
    //    {
    //        Debug.Log("File Does not exist");
    //        using (StreamWriter sw = File.AppendText(path))
    //        {
    //            foreach (string value in listOfEx)
    //            {
    //                sw.WriteLine(value);
    //            }
    //            sw.WriteLine((int)timer);
    //            sw.WriteLine(listOfEx.Count);
    //        }
    //    }
    //}

    public void updateCharge()
    {
        chargeLeft -= 100 / listOfEx.Count;
    }

    private void GestureRecognizerOnTappedEvent(InteractionSourceKind source, int tapCount, Ray headRay)
    {
        stretchComplete();
    }
}
