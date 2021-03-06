﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// ==============================================================================================================================
// FloatingText is used for adjusting text 
// ==============================================================================================================================

public class FloatingText : MonoBehaviour {
	public  Text        floatingText;
	public  float       timeToLive = 1;


    // ==============================================================================================================================
    // START
    // ==============================================================================================================================

    void Start(){
		Destroy (gameObject, timeToLive);
	}


    // ==============================================================================================================================
    // SETTEXT
    // ==============================================================================================================================

    public void SetText(string text, Color color){
		floatingText.color = color;
		floatingText.text = text;
	}


} // end of FloatingText