using UnityEngine;
using System.Collections;

// ==============================================================================================================================
// PauseMenu class used to manage the pause menu  
// ==============================================================================================================================

public class PauseMenu : MonoBehaviour {	

	public  string      game; 		            // Strings to hold level names 
	public  string      MainScreen; 

	public  static bool isPaused;	            // is paused is used to switch canvas on and off

	public  GameObject  pauseMenuCanvas;        // GameObject for the Canvas under pause menu 


    // ==============================================================================================================================
    // UPDATE
    // Update is called once per frame
    // ==============================================================================================================================

    void Update () {
		if (isPaused)
			pauseMenuCanvas.SetActive (true); 
		else
			pauseMenuCanvas.SetActive (false);
	}


    // ==============================================================================================================================
    // PAUSEBUTTON
    // ==============================================================================================================================

    public void pauseButtonPressed() {
        // play the click Sound
		SoundManager.PlaySfx (SoundManager.Instance.soundClick);    
		isPaused = !isPaused;
	}


    // ==============================================================================================================================
    // RESUMEBUTTON
    // ==============================================================================================================================

    public void resume() {
		isPaused = false; 
	}


    // ==============================================================================================================================
    // NEWGAMEBUTTON
    // ==============================================================================================================================

    public void newGAME() {
		isPaused = false; 
        // load the game Level 
		Application.LoadLevel (game);	
	}


    // ==============================================================================================================================
    // MAIN MENU BUTTON
    // ==============================================================================================================================

    public void mainMENU() {
		isPaused = false; 
        // load main screen 
		Application.LoadLevel (MainScreen);  
	}

		
} // end of PauseMenu