using UnityEngine;
using System.Collections;

// ==============================================================================================================================
// BasketTimeChallenge is the game itself 
// ==============================================================================================================================

public class BasketTimeChallenge : MonoBehaviour {

	public  static BasketTimeChallenge Instance;
    public  Transform[]     Destination;		        //moving destinations
	public  Transform       centerDes;	
    public  float           timer = 120f;
	public  float           speed = 1;
    public  float           timeStart,timeLeft;
    private	int             currentDestination;	
	private Vector2         target;
	private float           _timer;

	[HideInInspector]
	public bool             isRunning;


    // ==============================================================================================================================
    // AWAKE
    // ==============================================================================================================================

    void Awake() {
		Instance = this;
	}


    // ==============================================================================================================================
    // START
    // Use this for initialization
    // ==============================================================================================================================

    void Start () {
		currentDestination = 0;
		target = Destination [currentDestination].position;
		_timer = 120f;
		timeLeft = 120f;
	}


    // ============================================================================================================================== 
    // UPDATE
    // Update is called once per frame
    // ==============================================================================================================================

    void Update () {
		if (!isRunning) {
			transform.position = Vector2.MoveTowards (transform.position, centerDes.position, speed * Time.deltaTime);
			return;
		}
		transform.position = Vector2.MoveTowards (transform.position, centerDes.position, speed * Time.deltaTime);
		// This is where you pause the time when the pause menu is up
		if(!PauseMenu.isPaused)	
		timeLeft -= Time.deltaTime;

		if (timeLeft <= 0) {
			GameManager.Instance.GameOver ();
			Reset ();
		}
	}


    // ==============================================================================================================================
    // START RUN
    // ==============================================================================================================================

    public void StartRun() {
        //prevent the Ball send this event again
		if (isRunning)		
			return;
		
		isRunning = true;
		timeStart = Time.realtimeSinceStartup;
	}


    // ==============================================================================================================================
    // RESET
    // called by GameManager when gameover
    // ==============================================================================================================================

    public void Reset() {
		currentDestination = 0;
		target = Destination [currentDestination].position;
		_timer = timeLeft = 120f;
		isRunning = false;
	}


} // end of BasketTimeChallenge
