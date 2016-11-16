using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TheBall : MonoBehaviour { //The Ball CLASS calls ball
	public static TheBall Instance;
	public float force = 0f;		//the push force
	public float mulDistaceDrag = 1.5f;	//scale the distance of the begin touch and the current touch	
	public GameObject theBall;
	public float allowFireDistance = 2f;//when the distance of the begin and the current touch over this value then fire the ball
	public Transform[] SpawnPoint;	//random spawn the ball with this postion list
	public Sprite[] itemObj;		//array to hold sprite objects 

	public Queue<int> randInd = new Queue<int> ();

	[HideInInspector]
	public Sprite BallSprite;	//change every Ball sprite with this BallSprite 
	private int randIndex;		//random number for picking a random index in itemObj

	/*
	public GameObject Star;
	[Range(10,100)]
	public float percentShowStar = 20;

	GameObject _Star;
	
    */

	private Vector2 pos2;				 //the current or end touch position
	private bool isDragging = false;
	public float distance;
	Rigidbody2D rigBall;

	public AudioClip fireSound;			 //audio Sound 
	[Range(0,1)]
	public float fireSoundVolume = 0.5f; //audio Volume

	public Vector2 direction;			 // the direction the object has been fired 

	GameObject Ball;     // objects 
	Camera camera;
	Basket _Basket;
	StartMenu _StartMenu;

	void Awake(){
		Instance = this;
	}

	// // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // 

	// START  //
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
		randInd.Enqueue(Random.Range (0, 7));
		randInd.Enqueue(Random.Range (0, 7));
	}// End of START //

	// // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // 

	// UPDATE  //
	void Update () {

		// keep the object from firing automaticity when the play button is pressed 
		if (Ball == null || !Ball.GetComponent<Ball> ().touchTheBall || GameManager.Instance.State != GameManager.GameState.Playing) 
			return;


		if (Input.GetButtonDown ("Fire1")) {
			isDragging = true;		// user is dragging object to be fired   
		}

		/*
		if (Input.GetButtonUp ("Fire1") && isDragging) {
			isDragging = false;		
			if (distance > allowFireDistance)
				Fire ();			
		}*/

		if (isDragging) {	// if dragging is true 
			pos2 = camera.ScreenToWorldPoint(Input.mousePosition);
			distance = Vector2.Distance (Ball.transform.position, pos2) * mulDistaceDrag;	//mul 1.5 time drag touch screen
			if (distance > allowFireDistance) {
				Fire ();		//fire the ball when the distance is greater than the allowfiredistance
				isDragging = false; //object has been fired and nolonger is being dragged 
			}
		}

	} // END OF UPDATE

	// // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // 

	public float a;

	// // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // 
	// FIRE  // 
	void Fire(){
		//_StartMenu.HideMenu ();
		//if (BasketManager.Instance.Mode == BasketManager.PlayMode.TimeChallenge)

		if (BasketManager.Instance.Mode == BasketManager.PlayMode.Easy)
			BasketTimeChallenge.Instance.StartRun ();

		// Math stuff for determining the direction the object has been fired 
		direction = (pos2 - (Vector2)Ball.transform.position).normalized;
		a = Mathf.Atan2 (direction.y, direction.x) * Mathf.Rad2Deg;

		if (a > 110) {
			direction.x = -Mathf.Sin (10*Mathf.Deg2Rad);
			direction.y = Mathf.Cos (10*Mathf.Deg2Rad);
		} else if (a < 80) {
			direction.x = Mathf.Sin (10*Mathf.Deg2Rad);
			direction.y = Mathf.Cos (10*Mathf.Deg2Rad);
		}
		// call to play sound FX when object if fired 
		SoundManager.PlaySfx (fireSound, fireSoundVolume); 

		rigBall.isKinematic = false;
		rigBall.AddForce (direction * force);
		rigBall.gameObject.GetComponent<Ball> ().Fire ();

		//make the ball rotate a little bit
		rigBall.AddTorque (direction.x > 0 ? -100 : 100);

		Ball = null;

		var spawnPoint = SpawnPoint.Length > 0 ? SpawnPoint [Random.Range (0, SpawnPoint.Length)].position : transform.position;
		StartCoroutine (SpawnBallCo (.5f, spawnPoint));
	}// end of fire 

	// // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // 

	// SPAWN BALL // 
	//spawn the new ball after the delay time
	IEnumerator SpawnBallCo(float delay, Vector3 spawnPoint){

		yield return new WaitForSeconds (delay);

		Ball = Instantiate (theBall, transform.position, Quaternion.identity) as GameObject;

		ChangeBallSprite (); 	// call to change object sprite
		rigBall = Ball.GetComponent<Rigidbody2D> ();

	}// end of SpawnBall 

	// // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // 
	// CHANGE BALL SPRITE // 
	//change the ball's sprite
	public void ChangeBallSprite(){

		randInd.Enqueue (Random.Range (0, 7));
		randIndex = randInd.Dequeue ();		//get a ramdom number for ramdom index
		AssignItemValue (randIndex);

		if (BallSprite != null){	// if BallSprite is not equal to null then do this stuff
			//change the image sprite to a random image in items
			Ball.GetComponent<Ball> ().ChangeSprite (itemObj[randIndex]);
		}

	}// end of change ball 

	// // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // 
	// ASSIGN ITEM VALUE // 
	public void AssignItemValue(int index) {
		Ball.GetComponent<Ball> ().AssignValue (index);
	}

	// // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // 
	// RESET //
	//Called by GameManager
	public void Reset(){
		if (Ball != null)
			Ball.transform.position = transform.position;
	}

	// // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // 
	// Normal //
	private Vector2 Normal(Vector2 vec2){
		Vector2 result;
		result.x = vec2.x / Screen.width;
		result.y = vec2.y / Screen.height;

		return result;
	}
	// // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // 
} // of the ball class