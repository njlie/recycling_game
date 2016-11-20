using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour {
	public GameObject PlayBut;
	Animator anim;
	public Text score1;
	public Text score2;
	public Text best;
	public GameObject ScorePanel;
	public GameObject AddCoins;
	public Image SoundImage;
	public Sprite soundOn;
	public Sprite soundOff;
	//public string facebookLink = "Your facebook link";
	//public string yourAppLink = "Your app link";

	// // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // 
	// START // 
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		PlayBut.SetActive (true);
		ScorePanel.SetActive (false);
		score1.gameObject.SetActive (false);
		score2.text = "";
		best.text = "";
	} 
	// // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // 
	// UPDATE //
	// Update is called once per frame
	void Update () {
		score1.text = GameManager.Instance.Point.ToString ();
	}

	// // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // 
	// PLAY // 
	// called when the play button is pressed 
	public void Play(){
		PlayBut.SetActive (false);
		GameManager.Instance.StartGame ();
		MenuManager.Instance.StartGame ();
		HideMenu ();
		SoundManager.PlaySfx (SoundManager.Instance.soundClick);
	}

	// // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // 
	//SHOW MENU// 
	// shows the play button befor game starts 
	public void ShowMenu(){
		anim.SetBool ("isHide",false);
		score1.gameObject.SetActive (false);
		ScorePanel.SetActive (true);
		score2.text = GameManager.Instance.Point + "";
		best.text = GameManager.Instance.SavedPoints + "";
	} 
	// // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // //
	// HIDE MENU // 
	// do not comment this out, or else score will not show up on game screen 
	public void HideMenu (){
		anim.SetBool ("isHide",true);
		score1.gameObject.SetActive (true); // show the score of the game 
		ScorePanel.SetActive (false);
	}
	/*
	public void Sound(){
		if (AudioListener.volume == 1) {
			AudioListener.volume = 0;
			SoundImage.sprite = soundOff;
		} else {
			AudioListener.volume = 1;
			SoundImage.sprite = soundOn;
			SoundManager.PlaySfx (SoundManager.Instance.soundClick);
		}
	}
	*/
	/*
	public void Facebook(){
		Application.OpenURL (facebookLink);
	}

	*/
	/*
	public void Like(){
		Application.OpenURL (yourAppLink);
	}
	*/
}
