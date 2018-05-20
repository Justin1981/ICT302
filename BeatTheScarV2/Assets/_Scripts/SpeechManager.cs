using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class SpeechManager : MonoBehaviour
{
    KeywordRecognizer keywordRecognizer = null;
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();

    private GameController gameController;

    // Use this for initialization
    void Start ()
    {
        GameObject gcObj = GameObject.FindGameObjectWithTag("GameController");
        if (gcObj != null)
        {
            gameController = gcObj.GetComponent<GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("'GameController' script not found");
        }


        keywords.Add("Restart", () =>
        {
            // call the OnReset method on every descendant object
            //this.BroadcastMessage("OnReset");

            gameController.Restart();
        });

        keywords.Add("Pause", () =>
        {
            // call the OnReset method on every descendant object
            //this.BroadcastMessage("OnReset");

            gameController.Pause();
        });

        //keywords.Add("Drop Sphere", () =>
        //{
        //    var focusObject = GazeGestureManager.Instance.FocusedObject;
        //    if(focusObject != null)
        //    {
        //        // call the OnDrop method on just the focused object
        //        focusObject.SendMessage("OnDrop", SendMessageOptions.DontRequireReceiver);
        //    }
        //});

        // tell the KeywordRecognizer about our keywords
        keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());

        // register a callback for the KeywordRecognizer and start recognizing
        keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
        keywordRecognizer.Start();
	}

    private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        System.Action keywordAction;
        if (keywords.TryGetValue(args.text, out keywordAction))
        {
            keywordAction.Invoke();
        }
    }
}
