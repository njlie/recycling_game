﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// ==============================================================================================================================
// TimeDisplay class is used to update the time  
// ==============================================================================================================================

public class TimeDisplay : MonoBehaviour {

	public  Transform       displayPos;
	public  GameObject      Display;
	public  Text            time;
	public  float           minute, second;
	private string          timer;


    // ==============================================================================================================================
    // UPDATE
    // Update is called once per frame
    // ==============================================================================================================================

    void Update () {
		transform.position = Camera.main.WorldToScreenPoint (displayPos.position);
		if (BasketManager.Instance.Mode == BasketManager.PlayMode.Easy) {
            // if The BasketTimeChallenge is Running 
			if (BasketTimeChallenge.Instance.isRunning) {	
				minute = (int)((int)BasketTimeChallenge.Instance.timeLeft / 60f);
				second = (int)((BasketTimeChallenge.Instance.timeLeft % 60f));
				timer = string.Format ("{0:00}:{1:00}", minute, second);
				time.text = timer;
			} 
		} else
			Display.SetActive (false);
	}

	
} // END OF TIME_DISPLAY CLASS 
