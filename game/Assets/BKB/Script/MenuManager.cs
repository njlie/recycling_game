using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

 // ==============================================================================================================================
 // Menu manager used for various state of the menu for the game 
 // ==============================================================================================================================

public class MenuManager : MonoBehaviour {

    public  static  MenuManager     Instance;
	public          GameObject      Startmenu;
	public          GameObject      GUI;
	public          Transform       star_menu;

    // ============================================================================================================================== 
    // AWAKE
    // ==============================================================================================================================

    void Awake() {
		Instance = this;
		Startmenu.SetActive (true);
		GUI.SetActive (true);
	}

    // ==============================================================================================================================
    // RESTARTGAME 
    // ==============================================================================================================================

    public void RestartGame() {
		SoundManager.PlaySfx (SoundManager.Instance.soundClick);
		SceneManager.LoadSceneAsync (SceneManager.GetActiveScene ().buildIndex);
	}


    // ==============================================================================================================================
    // HOMESCENE
    // ==============================================================================================================================

    public void HomeScene() {
        SoundManager.PlaySfx (SoundManager.Instance.soundClick);
		Time.timeScale = 1;
		SceneManager.LoadSceneAsync ("MainMenu");

	}


    // ==============================================================================================================================
    // GAMEOVER
    // ==============================================================================================================================

    public void GameOver() {
		Startmenu.GetComponent<StartMenu> ().ShowMenu ();
	}


    // ==============================================================================================================================
    // STARTGAME
    // ==============================================================================================================================

    public void StartGame() {
		GUI.SetActive (true);
		GameManager.Instance.StartGame ();
	}


} // end of MenuManager
