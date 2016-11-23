using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {	

	public string game; 		// Strings to hold level names 
	public string MainScreen; 

	public static bool isPaused;	// is paused is used to switch canvas on and off

	public GameObject pauseMenuCanvas; // GameObject for the Canvas under pause menu 

	// // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // 
	// UPDATE // 
	// Update is called once per frame
	void Update () {
	
		if (isPaused) {
			pauseMenuCanvas.SetActive (true); 
		} else {
			pauseMenuCanvas.SetActive (false);
		}

		// code that is used to test pause menu using the Escape key 
		/*
		if (Input.GetKeyDown (KeyCode.Escape)) 
		{
			isPaused = !isPaused; 
		}*/

	}

	// // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // 
	// PAUSE BUTTON // 
	public void pauseButtonPressed(){
		SoundManager.PlaySfx (SoundManager.Instance.soundClick); // play the click Sound
		isPaused = !isPaused;
	}

	// // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // 
	// RESUME BUTTON // 
	public void resume() {
		isPaused = false; 
	}
	//
	// // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // 
	// NEW GAME BUTTON // 
	public void newGAME(){
		isPaused = false; 
		Application.LoadLevel (game);	// load the game Level 
	}

	// // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // 
	// MAIN MENU BUTTON // 
	public void mainMENU(){
		isPaused = false; 
		Application.LoadLevel (MainScreen);  // load main screen 
	}
		
} // end of PauseMenu