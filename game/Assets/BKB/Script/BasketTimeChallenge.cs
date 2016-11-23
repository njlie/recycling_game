using UnityEngine;
using System.Collections;

public class BasketTimeChallenge : MonoBehaviour {
	public static BasketTimeChallenge Instance;

	public Transform[] Destination;		//moving destinations
	public Transform centerDes;	

	int currentDestination;	
	Vector2 target;
	public float timer = 120f;
	public float speed = 1;

	public float timeStart,timeLeft;
	float _timer;

	[HideInInspector]
	public bool isRunning;

	void Awake(){
		Instance = this;
	}

	// Use this for initialization
	void Start () {
		if (Destination.Length < 3) {
			Debug.Log ("You must place 3 destination points to Destination");
		}

		currentDestination = 0;
		target = Destination [currentDestination].position;
		_timer = 120f;
		timeLeft = 120f;
	}

	// Update is called once per frame
	void Update () {
		if (!isRunning) {
			transform.position = Vector2.MoveTowards (transform.position, centerDes.position, speed * Time.deltaTime);
			return;
		}
		

		transform.position = Vector2.MoveTowards (transform.position, centerDes.position, speed * Time.deltaTime);
			
		if(!PauseMenu.isPaused){	// This is where you pause the time when the pause menu is up
		timeLeft -= Time.deltaTime;
		//timeLeft = _timer - (Time.realtimeSinceStartup - timeStart);
		}

		if (timeLeft <= 0) {
			GameManager.Instance.GameOver ();
			Reset ();
		}
	}

	public void StartRun(){
		if (isRunning)		//prevent the Ball send this event again
			return;
		
		isRunning = true;
		timeStart = Time.realtimeSinceStartup;
	}

	//called by GameManager when gameover
	public void Reset(){
		currentDestination = 0;
		target = Destination [currentDestination].position;
		_timer = timeLeft = 120f;
		//timeLeft = 120f;
		isRunning = false;
	}
}
