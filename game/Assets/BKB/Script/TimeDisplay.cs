using UnityEngine;
using System.Collections;
using UnityEngine.UI;
/* 
TimeDisplay class is used to update the time  
*/ 
public class TimeDisplay : MonoBehaviour {
	public Transform displayPos;
	public GameObject Display;
	public Text time;
	public float minute, second;
	string timer;

	// // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // 
	// UPDATE //
	// Update is called once per frame
	void Update () {
		transform.position = Camera.main.WorldToScreenPoint (displayPos.position);
		//if (BasketManager.Instance.Mode == BasketManager.PlayMode.TimeChallenge) {
		if (BasketManager.Instance.Mode == BasketManager.PlayMode.Easy) {
			//Display.SetActive (true);
			if (BasketTimeChallenge.Instance.isRunning) {	// if The BasketTimeChallenge is Running 
				minute = (int)((int)BasketTimeChallenge.Instance.timeLeft / 60f);
				second = (int)((BasketTimeChallenge.Instance.timeLeft % 60f));
				timer = string.Format ("{0:00}:{1:00}", minute, second);
				time.text = timer;
			} /*else {

				// this part is not needed for this version of the game
				minute = (int)((int)BasketTimeChallenge.Instance.timeLeft / 60f);
				second = (int)((BasketTimeChallenge.Instance.timeLeft % 60f));
				timer = string.Format ("{0:00}:{1:00}", minute, second);
				time.text = timer;
				
			} */ 
		} else
			Display.SetActive (false);
	} // END OF UPDATE
	// // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // 
} // END OF TIME_DISPLAY CLASS 
