using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
/*
 * Menu manager used for various state of the menu for the game 
 *
*/
public class MenuManager : MonoBehaviour {
	public static MenuManager Instance;

	public GameObject Startmenu;
	public GameObject GUI;
	public Transform star_menu;

	// // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // 
	// AWAKE // 
	void Awake(){
		Instance = this;
		Startmenu.SetActive (true);
		GUI.SetActive (true);
	}

	// // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // 
	// RESTART GAME // 
	public void RestartGame(){
		SoundManager.PlaySfx (SoundManager.Instance.soundClick);
		SceneManager.LoadSceneAsync (SceneManager.GetActiveScene ().buildIndex);
	}

	// // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // 
	// HOME SCENE // 
	public void HomeScene(){
		SoundManager.PlaySfx (SoundManager.Instance.soundClick);
		Time.timeScale = 1;
		SceneManager.LoadSceneAsync ("MainMenu");

	}
	// // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // 
	// GAME OVER // 
	public void GameOver(){
		Startmenu.GetComponent<StartMenu> ().ShowMenu ();
	}

	// using a diffent pause feature, check out the pause menu script 
//	public void Pause(){
//		SoundManager.PlaySfx (SoundManager.Instance.soundClick);
//		if (Time.timeScale == 0) {
//			GUI.SetActive (true);
//			Time.timeScale = 1;
//		} else {
//			GUI.SetActive (false);
//			Time.timeScale = 0;
//		}
//	}

	// // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // 
	// START GAME // 
	public void StartGame(){
		GUI.SetActive (true);
		GameManager.Instance.StartGame ();
	}
	/*
	// // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // 
	// GAME OVER // 
	IEnumerator GameOverCo(float time){
		yield return new WaitForSeconds (time);
		Startmenu.SetActive (true);
	}

	// // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // 
	// OPEN PLAY MODE // 
	public void OpenPlayMode(){
		SoundManager.PlaySfx (SoundManager.Instance.soundClick);
	}
	*/
}
