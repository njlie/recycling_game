using UnityEngine;
using System.Collections;

public class newWindow : MonoBehaviour {

	public string game; 
	public string gameTest;
	public string SignIn; 
	public string Profile; 
	public string Main;
	public string TutorialScreen;
	public string OptionsScreen;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void changeToGame(){
		Application.LoadLevel (game);	// load the game Level
	}

	public void changeToGameTest(){
		Application.LoadLevel (gameTest);
	}
	
	public void changeSignIn(){
		Application.LoadLevel (SignIn);	// load the game Level
	}

	public void changeProfile(){
		Application.LoadLevel (Profile);	// load the game Level
	}

	public void changeMain(){
		Application.LoadLevel (Main);	// load the game Level
	}

	public void changeTutorialScreen(){
		Application.LoadLevel (TutorialScreen);	// load the Tutorial Screen
	}

	public void changeOptionsScreen(){
		Application.LoadLevel (OptionsScreen);	// load the Tutorial Screen
	}
		
		
	public void quit(){
		Application.Quit();	// quit the game 
	}
}
