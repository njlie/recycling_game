using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// ==============================================================================================================================
// Ball is used to refer to trash objects for clarity
// class is used for the trash objects themself 
// TheBall calls ball class 
// ==============================================================================================================================

public class TheBall : MonoBehaviour {

	public  static  TheBall Instance; 
	public  float           force = 0f;		            // the push force
	public  float           mulDistaceDrag = 1.5f;	    // scale the distance of the begin touch and the current touch	
	public  GameObject      theBall;
	public  float           allowFireDistance = 2f;     // when the distance of the begin and the current touch over this value then fire the ball
	public  Transform[]     SpawnPoint;	                // random spawn the ball with this postion list
	public  Sprite[]        itemObj;		            // Item object is an array that is used to hold trash objects 
    public  int             numItems = 12;              // number of items
	public  int             count = 0;			
	public  Queue<int>      randInd = new Queue<int> ();

	[HideInInspector]
	public  Sprite          BallSprite;                 // change every Ball sprite with this BallSprite 
	private int             randIndex;		            // Used for picking a random Index in item Object. 
	private Vector2         pos2;	                    // the current or end touch position
	private bool            isDragging = false;	        // is the ball being fired 
	public  float           distance;
	public  float           a;                          // used in Fire for calculation of direction 
    public  Vector2         direction;                  // direction that the trash object was fired 
    Rigidbody2D             rigBall;
    GameObject              Ball;
	Camera                  camera;
	Basket                  _Basket;
	StartMenu               _StartMenu;
    
    public  AudioClip       fireSound;	                // Audio Clip for when the ball is fired  
	[Range(0,1)]
	public float fireSoundVolume = 0.5f;                // volume 


    // ==============================================================================================================================
    // AWAKE
    // ==============================================================================================================================

    void Awake() {
        // refer to TheBAll itself 
		Instance = this; 
	}


    // ==============================================================================================================================
    // START
    // Use this for initialization
    // ==============================================================================================================================
    void Start () {
		StartCoroutine (SpawnBallCo (0, transform.position));
		_Basket = FindObjectOfType<Basket> ();
		_StartMenu = FindObjectOfType<StartMenu> ();
		camera = Camera.main;

		//get the choosen ball sprite
		BallSprite = ItemManager.Instance.GetItemImage (PlayerPrefs.GetInt (GlobalValue.ChoosenBall, 0));
		randInd.Clear();
		randInd.Enqueue(Random.Range (0, numItems));
	}


    // ==============================================================================================================================
    // UPDATE
    // ==============================================================================================================================

    void Update () {
		// keep the object from Firing when the play button is pressed 
		if (Ball == null || !Ball.GetComponent<Ball> ().touchTheBall || GameManager.Instance.State != GameManager.GameState.Playing)
			return;

		if (Input.GetButtonDown ("Fire1"))
			isDragging = true;

		if (Input.GetButtonUp ("Fire1") && isDragging) {
			isDragging = false;
			if (distance > allowFireDistance)
				Fire ();
		}

		if (isDragging) {
			pos2 = camera.ScreenToWorldPoint(Input.mousePosition);
            // mul 1.5 time drag touch screen
			distance = Vector2.Distance (Ball.transform.position, pos2) * mulDistaceDrag;	
			if (distance > allowFireDistance) {
                //fire the ball when the distance is greater than the allowfiredistance
				Fire ();		
				isDragging = false;
			}
		}
	}


    // ==============================================================================================================================
    // FIRE
    // ==============================================================================================================================

    void Fire() {
        // where using the BasketTimeChallenge Version of the game
		if (BasketManager.Instance.Mode == BasketManager.PlayMode.Easy)  
            // create an instance of BasketTimeChallenge
			BasketTimeChallenge.Instance.StartRun ();		 
        
        // the direction the object has been fired 
		direction = (pos2 - (Vector2)Ball.transform.position).normalized; 
        // math stuff for determining the direction 
		a = Mathf.Atan2 (direction.y, direction.x) * Mathf.Rad2Deg; 
		if (a > 115) {
			direction.x = -Mathf.Sin (10*Mathf.Deg2Rad);
			direction.y = Mathf.Cos (10*Mathf.Deg2Rad);
		} else if (a < 75) {
			direction.x = Mathf.Sin (10*Mathf.Deg2Rad);
			direction.y = Mathf.Cos (10*Mathf.Deg2Rad);
		}

        // play the fireSound when the object is fired 
		SoundManager.PlaySfx (fireSound, fireSoundVolume);	

		rigBall.isKinematic = false;
		rigBall.AddForce (direction * force);
		rigBall.gameObject.GetComponent<Ball> ().Fire ();

		//make the ball rotate a little bit
		rigBall.AddTorque (direction.x > 0 ? -100 : 100);

		Ball = null;
	}


    // ==============================================================================================================================
    // SPAWN
    // spawn the new ball after the delay time
    // ==============================================================================================================================

    IEnumerator SpawnBallCo(float delay, Vector3 spawnPoint) {
		yield return new WaitForSeconds (delay);

		Ball = Instantiate (theBall, transform.position, Quaternion.identity) as GameObject;
		ChangeBallSprite ();
		rigBall = Ball.GetComponent<Rigidbody2D> ();

	}


    // ==============================================================================================================================
    // CYCLE BALL
    // ==============================================================================================================================

    public void CycleBallSprite() {
		Destroy (Ball);
	}


    // ==============================================================================================================================
    // CHANGE SPRITE 
    // change the ball's sprite
    // ==============================================================================================================================

    public void ChangeBallSprite() {
        // the Random Index to pick a random object in itemObj
		randIndex = randInd.Peek ();	 
        // call to assign object value befor it goes into itemObj
		AssignItemValue (randIndex);	
		if (BallSprite != null)
            //change the ball sprite to the random index in itemObj 
			Ball.GetComponent<Ball> ().ChangeSprite (itemObj[randIndex]); 
	}


    // ==============================================================================================================================
    // ITEM VALUE
    // ==============================================================================================================================

    public void AssignItemValue(int index) {
        // where each object gets a value to correspond to a bin
		Ball.GetComponent<Ball> ().AssignValue (index);   
	}


    // ==============================================================================================================================
    // DEQUEUE ITEM
    // ==============================================================================================================================

    public void dequeueItem(bool correct) {
		if (correct) {
			randInd.Dequeue ();
			randInd.Enqueue (Random.Range (0, numItems));
			count++;
		}
		var spawnPoint = SpawnPoint.Length > 0 ? SpawnPoint [Random.Range (0, SpawnPoint.Length)].position : transform.position;
		StartCoroutine (SpawnBallCo (0f, spawnPoint));
	}


    // ============================================================================================================================== 
    // RESET
    // Called by GameManager
    // ==============================================================================================================================

    public void Reset() {
		if (Ball != null)
			Ball.transform.position = transform.position;
	}


    // ============================================================================================================================== 
    // NORMAL
    // ==============================================================================================================================

    private Vector2 Normal(Vector2 vec2) {
		Vector2 result;
		result.x = vec2.x / Screen.width;
		result.y = vec2.y / Screen.height;
		return result;
	}


}// end of TheBall class