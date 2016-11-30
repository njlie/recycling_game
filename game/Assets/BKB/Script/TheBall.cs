using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/* 
 * Ball is used to refer to trash objects for clarity
 * class is used for the trash objects themself 
 * TheBall calls ball class 
*/ 

public class TheBall : MonoBehaviour {
	public static TheBall Instance; 
	public float force = 0f;		    //the push force
	public float mulDistaceDrag = 1.5f;	//scale the distance of the begin touch and the current touch	
	public GameObject theBall;
	public float allowFireDistance = 2f; //when the distance of the begin and the current touch over this value then fire the ball
	public Transform[] SpawnPoint;	   //random spawn the ball with this postion list
	public Sprite[] itemObj;		// Item object is an array that is used to hold trash objects 
	public int count = 0;			

	public Queue<int> randInd = new Queue<int> ();

	[HideInInspector]
	public Sprite BallSprite;	//change every Ball sprite with this BallSprite 
	private int randIndex;		// Used for picking a random Index in item Object. 

	/*
	public GameObject Star;		// CODE NOT NEEDED FOR THIS VERSION OF THE GAME 
	[Range(10,100)]
	public float percentShowStar = 20;
	GameObject _Star;
	*/

	private Vector2 pos2;	//the current or end touch position
	private bool isDragging = false;	// is the ball being fired 
	public float distance;
	Rigidbody2D rigBall;

	public AudioClip fireSound;	// Audio Clip for when the ball is fired  
	[Range(0,1)]
	public float fireSoundVolume = 0.5f;	// volume 

	public Vector2 direction; // direction that the trash object was fired 

	GameObject Ball;	// game objects 
	Camera camera;
	Basket _Basket;
	StartMenu _StartMenu;

	void Awake(){
		Instance = this; // refer to TheBAll itself 
	}

	// // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // 
	// START //
	// Use this for initialization
	void Start () {
		Debug.Log ("TheBall/Start");
		StartCoroutine (SpawnBallCo (0, transform.position));
		_Basket = FindObjectOfType<Basket> ();
		_StartMenu = FindObjectOfType<StartMenu> ();

		camera = Camera.main;

		//get the choosen ball sprite
		BallSprite = ItemManager.Instance.GetItemImage (PlayerPrefs.GetInt (GlobalValue.ChoosenBall, 0));
		randInd.Clear();
		//randInd.Enqueue (0);
		randInd.Enqueue(Random.Range (0, 12));
		//randInd.Enqueue(Random.Range (0, 7));
	}
	// // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // 
	// UPDATE //
	void Update () {
		// keep the object from Firing when the play button is pressed 
		if (Ball == null || !Ball.GetComponent<Ball> ().touchTheBall || GameManager.Instance.State != GameManager.GameState.Playing)
			return;

		if (Input.GetButtonDown ("Fire1")) {
			isDragging = true;
		}

		if (Input.GetButtonUp ("Fire1") && isDragging) {
			isDragging = false;
			if (distance > allowFireDistance)
				Fire ();
		}

		if (isDragging) {
			pos2 = camera.ScreenToWorldPoint(Input.mousePosition);
			distance = Vector2.Distance (Ball.transform.position, pos2) * mulDistaceDrag;	//mul 1.5 time drag touch screen
			if (distance > allowFireDistance) {
				Fire ();		//fire the ball when the distance is greater than the allowfiredistance
				isDragging = false;
			}
		}
	}

	public float a; // used in Fire for calculation of direction 

	// // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // 
	// FIRE //
	void Fire(){
		//_StartMenu.HideMenu ();

		//if (BasketManager.Instance.Mode == BasketManager.PlayMode.TimeChallenge)
		if (BasketManager.Instance.Mode == BasketManager.PlayMode.Easy) // where using the BasketTimeChallenge Version of the game 
			BasketTimeChallenge.Instance.StartRun ();		// Create an instance of BasketTimeChallenge 

		direction = (pos2 - (Vector2)Ball.transform.position).normalized; // The DIRECTION the object has been fired 

		a = Mathf.Atan2 (direction.y, direction.x) * Mathf.Rad2Deg; // math stuff for determining the direction 
		if (a > 115) {
			direction.x = -Mathf.Sin (10*Mathf.Deg2Rad);
			direction.y = Mathf.Cos (10*Mathf.Deg2Rad);
		} else if (a < 75) {
			direction.x = Mathf.Sin (10*Mathf.Deg2Rad);
			direction.y = Mathf.Cos (10*Mathf.Deg2Rad);
		}

		SoundManager.PlaySfx (fireSound, fireSoundVolume);	// play the fireSound when the object is fired 

		rigBall.isKinematic = false;
		rigBall.AddForce (direction * force);
		rigBall.gameObject.GetComponent<Ball> ().Fire ();

		//make the ball rotate a little bit
		rigBall.AddTorque (direction.x > 0 ? -100 : 100);

		Ball = null;

		//var spawnPoint = SpawnPoint.Length > 0 ? SpawnPoint [Random.Range (0, SpawnPoint.Length)].position : transform.position;
		//StartCoroutine (SpawnBallCo (.5f, spawnPoint));
	}

	// // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // 
	// SPAWN //
	//spawn the new ball after the delay time
	IEnumerator SpawnBallCo(float delay, Vector3 spawnPoint){
		yield return new WaitForSeconds (delay);

		Ball = Instantiate (theBall, transform.position, Quaternion.identity) as GameObject;
		ChangeBallSprite ();
		rigBall = Ball.GetComponent<Rigidbody2D> ();

	}

	// // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // 
	// CYCLE BALL //
	public void CycleBallSprite(){
		Destroy (Ball);
	}

	// // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // 
	// CHANGE SPRITE //
	//change the ball's sprite
	public void ChangeBallSprite(){
		randIndex = randInd.Peek ();	// the Random Index to pick a random object in itemObj 
		AssignItemValue (randIndex);	// call to assign object value befor it goes into itemObj
		if (BallSprite != null){
			Ball.GetComponent<Ball> ().ChangeSprite (itemObj[randIndex]); //change the ball sprite to the random index in itemObj 
		}
	}

	// // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // 
	// ITEM VALUE //
	public void AssignItemValue(int index) {
		Ball.GetComponent<Ball> ().AssignValue (index); // where each object gets a value to correspond to a bin  
	}

	// // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // 
	// DEQUEUE ITEM //
	public void dequeueItem(bool correct) {
		if (correct) {
			randInd.Dequeue ();
			randInd.Enqueue (Random.Range (0, 12));
			count++;
		}
		var spawnPoint = SpawnPoint.Length > 0 ? SpawnPoint [Random.Range (0, SpawnPoint.Length)].position : transform.position;
		StartCoroutine (SpawnBallCo (0f, spawnPoint));
	}

	// // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // 
	// RESET //
	//Called by GameManager
	public void Reset(){
		if (Ball != null)
			Ball.transform.position = transform.position;
	}
	// // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // 
	// NORMAL //
	private Vector2 Normal(Vector2 vec2){
		Vector2 result;
		result.x = vec2.x / Screen.width;
		result.y = vec2.y / Screen.height;

		return result;
	}
	// // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // 
}// end of TheBall class