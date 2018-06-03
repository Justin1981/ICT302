// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.WSA.Input;
using UnityEngine.UI;

/// <summary>
/// HandsManager determines if the hand is currently detected or not.
/// </summary>
public class HandsManager : MonoBehaviour
{
    /// <summary>
    /// HandDetected tracks the hand detected state.
    /// Returns true if the list of tracked hands is not empty.
    /// </summary>
    public bool HandDetected
    {
        get { return trackedHands.Count > 0; }
    }

    //public GameObject TrackingObject;
    public Text StatusText;
    public Text HandPosText;

    private HashSet<uint> trackedHands = new HashSet<uint>();
    //private Dictionary<uint, GameObject> trackingObject = new Dictionary<uint, GameObject>();
    private GestureRecognizer gestureRecognizer;
    private uint activeId;

    private Vector3 handPosition;

    private GameController gameController;

    void Start()
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

        InteractionManager.InteractionSourceDetected += InteractionManager_InteractionSourceDetected;
        InteractionManager.InteractionSourceUpdated += InteractionManager_InteractionSourceUpdated;
        InteractionManager.InteractionSourceLost += InteractionManager_InteractionSourceLost;

        InteractionManager.GetCurrentReading();

        gestureRecognizer = new GestureRecognizer();
        gestureRecognizer.SetRecognizableGestures(GestureSettings.Tap | GestureSettings.Hold);
        gestureRecognizer.Tapped += GestureRecognizer_Tapped;
        //gestureRecognizer.HoldStarted += GestureRecognizer_HoldStarted;
        //gestureRecognizer.HoldCompleted += GestureRecognizer_HoldCompleted;
        //gestureRecognizer.HoldCanceled += GestureRecognizer_HoldCanceled;            
        gestureRecognizer.StartCapturingGestures();
        StatusText.text = "READY";
        HandPosText.text = "Nothing Detected";

    }

    void Update()
    {
        InteractionManager.GetCurrentReading();

        HandPosText.text = handPosition.ToString();
    }

    private void GestureRecognizer_HoldStarted(HoldStartedEventArgs args)
    {
        uint id = args.source.id;            
        StatusText.text = "HoldStarted - Kind: " + args.source.kind.ToString() + " - Id: " +  id.ToString();

        if(activeId == id)
            StatusText.text += "-TRACKED";
    }

    private void GestureRecognizer_HoldCompleted(HoldCompletedEventArgs args)
    {
        uint id = args.source.id;            
        StatusText.text = "HoldCompleted - Kind:" + args.source.kind.ToString() + " - Id: " + id.ToString();

        if (activeId == id)
            StatusText.text += "-TRACKED";
    }

    private void GestureRecognizer_HoldCanceled(HoldCanceledEventArgs args)
    {
        uint id = args.source.id;            
        StatusText.text = "HoldCanceled - Kind: " + args.source.kind.ToString() + " - Id: " + id.ToString();

        if (activeId == id)
            StatusText.text += "-TRACKED";
    }

    private void GestureRecognizer_Tapped(TappedEventArgs args)
    {
        uint id = args.source.id;
        StatusText.text = "Tapped - Kind: " + args.source.kind.ToString() + " - Id: " + id.ToString();  

        if (activeId == id)
            StatusText.text += "-TRACKED";

        //if (activeId != id)
        //{
            //gameController.PlayerUpdateHand((int)activeId, handPosition);
        //}

        gameController.PlayerShoot();
    }


    private void InteractionManager_InteractionSourceDetected(InteractionSourceDetectedEventArgs args)
    {
        uint id = args.state.source.id;
        // Check to see that the source is a hand.
        if (args.state.source.kind != InteractionSourceKind.Hand)
        {
            return;
        }
        args.state.sourcePose.TryGetPosition(out handPosition);
        trackedHands.Add(id);
        activeId = id;
        //gameController.PlayerUpdateHand((int)activeId, handPosition);
    }

    private void InteractionManager_InteractionSourceUpdated(InteractionSourceUpdatedEventArgs args)
    {
        if (args.state.source.kind == InteractionSourceKind.Hand)
        {
            args.state.sourcePose.TryGetPosition(out handPosition);
        }
        gameController.PlayerUpdateHand((int)activeId, handPosition);
    }

    private void InteractionManager_InteractionSourceLost(InteractionSourceLostEventArgs args)
    {
        uint id = args.state.source.id;
        // Check to see that the source is a hand.
        if (args.state.source.kind != InteractionSourceKind.Hand)
        {
            return;
        }

        if (trackedHands.Contains(id))
        {
            trackedHands.Remove(id);
        }

        if (trackedHands.Count > 0)
        {
            activeId = trackedHands.First();
        }
        //else
        //{
        //    handPosition.Set(0.0f, 0.0f, 0.0f);
        //}
    }

    void OnDestroy()
    {                        
        InteractionManager.InteractionSourceDetected -= InteractionManager_InteractionSourceDetected;
        InteractionManager.InteractionSourceUpdated -= InteractionManager_InteractionSourceUpdated;
        InteractionManager.InteractionSourceLost -= InteractionManager_InteractionSourceLost;

        // Not required for current Unity project
        //gestureRecognizer.HoldStarted -= GestureRecognizer_HoldStarted;
        //gestureRecognizer.HoldCompleted -= GestureRecognizer_HoldCompleted;
        //gestureRecognizer.HoldCanceled -= GestureRecognizer_HoldCanceled;

        gestureRecognizer.Tapped -= GestureRecognizer_Tapped;

        gestureRecognizer.StopCapturingGestures();
    }

}
