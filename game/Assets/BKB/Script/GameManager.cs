using UnityEngine;
using System.Collections;

// ==============================================================================================================================
// GameManager is used to Manage the components of the game like the points. 
// ==============================================================================================================================

public class GameManager: MonoBehaviour {

	public  static  GameManager Instance{ get; private set;}
	public  enum    GameState   {Menu,Playing, Dead, Finish};       // what is the state of the game?
	public          GameState   State{ get; set; }
	public  int                 starsDefault = 100;                 // star thing not needed 
	private MenuManager         menuManager;        
	SoundManager soundManager;
	Basket _Basket;

	[HideInInspector]
	public  bool                isNoLives = false;
    private int                 savePointCheckPoint;

    // ==============================================================================================================================
    // AWAKE 
    // ==============================================================================================================================

    void Awake(){
		Instance = this;
		State = GameState.Menu;
	}


    // ==============================================================================================================================
    // POINT
    // ==============================================================================================================================

    public int Point{ get; set; }
	    

    // ==============================================================================================================================
    // SAVEDSTARS
    // ==============================================================================================================================

    public int SavedStars { 
		get { return PlayerPrefs.GetInt (GlobalValue.Stars, 0); } 
		set { PlayerPrefs.SetInt (GlobalValue.Stars, value); } 
	}


    // ==============================================================================================================================
    // SAVEDPOINTS
    // ==============================================================================================================================

    public int SavedPoints {
		get {
			string mode;
			mode = GlobalValue.ModeDual;
			
			return PlayerPrefs.GetInt (mode, 0); } 
		set { 
			string mode;
			mode = GlobalValue.ModeDual;

			PlayerPrefs.SetInt (mode, value); } 
	}


    // ==============================================================================================================================
    // START
    // ==============================================================================================================================

    void Start() {
		if (!PlayerPrefs.HasKey (GlobalValue.Stars))
			SavedStars = starsDefault;
		
		menuManager = FindObjectOfType<MenuManager> ();

		soundManager = FindObjectOfType<SoundManager> ();
		_Basket = FindObjectOfType<Basket> ();
	}


    // ==============================================================================================================================
    // STARTGAME
    // ==============================================================================================================================

    public void StartGame() {
		State = GameState.Playing;
	}


    // ==============================================================================================================================
    // GAMEOVER
    // ==============================================================================================================================

    public void GameOver() {
		if (Point > SavedPoints)
			SavedPoints = Point;
	
		MenuManager.Instance.GameOver ();
		SoundManager.PlaySfx (soundManager.soundGameover, 0.5f);

		StartCoroutine (ResetCo (0.1f));
	}


    // ============================================================================================================================== 
    // RESET CO
    // ==============================================================================================================================

    IEnumerator ResetCo(float time) {
		yield return new WaitForSeconds (time);
		//reset all value and send the command to others
		Point = 0;
		TheBall.Instance.Reset ();
		if (BasketTimeChallenge.Instance != null)
			BasketTimeChallenge.Instance.Reset ();
		GlobalValue.combo = 1;
	}


} // end of GameManager