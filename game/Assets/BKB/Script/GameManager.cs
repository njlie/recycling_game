using UnityEngine;
using System.Collections;

/*
 * GameManager is used to Manage the components of the game like the points. 
*/

public class GameManager: MonoBehaviour {
	public static GameManager Instance{ get; private set;}

	public enum GameState{Menu,Playing, Dead, Finish}; // what is the state of the game?
	public GameState State{ get; set; }

	public int starsDefault = 100; // star thing not needed 

	//[Header("Floating Text")]
	//public GameObject FloatingText;
	private MenuManager menuManager; // game object 

	SoundManager soundManager;
	Basket _Basket;

	[HideInInspector]
	public bool isNoLives = false;

	// // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // 
	// AWAKE //
	void Awake(){
		Instance = this;
		State = GameState.Menu;
	}

	// // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // 
	// POINT //
	public int Point{ get; set; }
	int savePointCheckPoint;

	public int SavedStars{ 
		get { return PlayerPrefs.GetInt (GlobalValue.Stars, 0); } 
		set { PlayerPrefs.SetInt (GlobalValue.Stars, value); } 
	}
	public int SavedPoints {
		get {
			string mode;
//			if (BasketManager.Instance.Mode == BasketManager.PlayMode.Endless)
//				mode = GlobalValue.ModeNormal;
//			else if (BasketManager.Instance.Mode == BasketManager.PlayMode.Sliding)
//				mode = GlobalValue.ModeSliding;
//			else
				mode = GlobalValue.ModeDual;
			
			return PlayerPrefs.GetInt (mode, 0); } 
		set { 
			string mode;
//			if (BasketManager.Instance.Mode == BasketManager.PlayMode.Endless)
//				mode = GlobalValue.ModeNormal;
//			else if (BasketManager.Instance.Mode == BasketManager.PlayMode.Sliding)
//				mode = GlobalValue.ModeSliding;
//			else
				mode = GlobalValue.ModeDual;

			PlayerPrefs.SetInt (mode, value); } 
	}

	// // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // 
	// START //
	void Start(){
		if (!PlayerPrefs.HasKey (GlobalValue.Stars))
			SavedStars = starsDefault;
		
		menuManager = FindObjectOfType<MenuManager> ();

		soundManager = FindObjectOfType<SoundManager> ();
		_Basket = FindObjectOfType<Basket> ();
	}
	/* CODE NOT NEEDED FOR THIS VERSION OF THE GAME
	public void ShowFloatingText(string text, Vector2 positon, Color color){
		GameObject floatingText = Instantiate (FloatingText) as GameObject;
		var _position = Camera.main.WorldToScreenPoint (positon);

		floatingText.transform.SetParent (menuManager.transform,false);
		floatingText.transform.position = _position;
			
		var _FloatingText = floatingText.GetComponent<FloatingText> ();
		_FloatingText.SetText (text, color);
	}*/
	// // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // 
	// START GAME //
	public void StartGame(){
		State = GameState.Playing;
	}
	// // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // 
	// GAME OVER //
	public void GameOver(){
//		State = GameState.Dead;
		if (Point > SavedPoints)
			SavedPoints = Point;
	
		MenuManager.Instance.GameOver ();
		SoundManager.PlaySfx (soundManager.soundGameover, 0.5f);

		StartCoroutine (ResetCo (0.1f));
//		AdsController.ShowAds ();
	}
	// // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // 
	// RESET CO //
	IEnumerator ResetCo(float time){
		yield return new WaitForSeconds (time);

		//reset all value and send the command to others
		Point = 0;
		TheBall.Instance.Reset ();
		//_Basket.Reset ();
		if (BasketTimeChallenge.Instance != null)
			BasketTimeChallenge.Instance.Reset ();
		GlobalValue.combo = 1;
	}
}//end of GameManager