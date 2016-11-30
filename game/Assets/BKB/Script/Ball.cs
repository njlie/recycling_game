using UnityEngine;
using System.Collections;

/*
 * Ball class is the trash objects themselves  
 * Ball is used to refer to trash objects for clarity
 * called by TheBall class 
*/

public class Ball : MonoBehaviour { 
	public Vector3 scaleTo;		//the ball image will be scale to this size after it's fired
	public float speed = 1f;	//the speed to scale the ball
	public string itemValue; // value of item (recycling, compost, landfill)
	[HideInInspector]
	public bool fire = false;	//telling that the ball is fired already
	public Transform Ballsprite;

	private int compostCount = 2;  // values used to Assign values
    private int landfillCount = 2;
    private int recycleCount = 8;

	public AudioClip[] bounceSound;
	[Range(0,1)]
	public float bounceSoundVolume = 0.5f;

	public AudioClip hitBasketSound;
	[Range(0,1)]
	public float hitBasketSoundVolume = 0.5f;

	public AudioClip pointSound;
	[Range(0,1)]
	public float pointSoundVolume = 0.5f;

	public AudioClip perfectSound;
	[Range(0,1)]
	public float perfectSoundVolume = 0.5f;

	[HideInInspector]
	public int points;

	Rigidbody2D rig;
	Animator anim;
	bool isPerfect = true;
	CircleCollider2D coll;
	bool end = false;
	[HideInInspector]
	public int combo = 1;

	[HideInInspector]
	public bool touchTheBall = false;

	// // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // 
	// START //
	// Use this for initialization
	void Start () {
		Debug.Log ("Ball/Start");
		rig = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		coll = GetComponent<CircleCollider2D> ();
		coll.enabled = true;
	}

	// // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // 
	// FIRE //
	public void Fire(){
		Debug.Log ("Ball/Fire");
		fire = true;
		coll.enabled = false;		//turn off collider when the ball begin move up to avoid the unnecessary collides
	}

	// // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // 
	// CHANGE SPRITE //
	public void ChangeSprite(Sprite sprite){
		Debug.Log ("Ball/ChangeSprite");
		Ballsprite.GetComponent<SpriteRenderer> ().sprite = sprite;		//change the ball sprite
	}

	// // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // 
	// ASSIGN VAlUE //
	// assign the value of each object 
	public void AssignValue(int index){
		if (index < compostCount)
			itemValue = "Compost";		// it's compost item 
		else if (index < compostCount + landfillCount)
			itemValue = "Landfill";	// it's LandFill item 
		else
			itemValue = "Recycle";	// it's Recycle item 
	}

	// // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // 
	// UPDATE //
	// Update is called once per frame
	void Update () {
		if (end)
			return;

		if(fire)	//scale the ball with the speed
			Ballsprite.transform.localScale = Vector3.Lerp (Ballsprite.transform.localScale, scaleTo, speed * Time.deltaTime);

		//just allow enable collider when the ball begin falling
		if (rig.velocity.y < 0) {	//if the ball begin falling, turn on the collider
//			//Debug.Log ("Ball/Update/velocity<0");
			coll.enabled = true;
//			//make the ball behide the basket bar when it's falling
//			Ballsprite.GetComponent<SpriteRenderer> ().sortingOrder = 0;		//make the ball image behind the basket when it's falling
		}
	}

//	void OnCollisionEnter2D(Collision2D other){
//		Debug.Log ("Ball/onCollisionEnter");
//		SoundManager.PlaySfx (bounceSound[Random.Range(0,bounceSound.Length)], bounceSoundVolume);
//		isPerfect = false;		//no perfect when the ball collide with the basket
//	}

	// // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // 
	// MOUSE DOWN //
	void OnMouseDown() {
		Debug.Log ("Ball/onMouseDown");
		touchTheBall = true;		//touch on the ball, mean the first touch must on the ball then we can fire it
	}
	// // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // 
	// ADD POINT //
	void AddPoint() {
		Debug.Log ("ADDPOINT");
		TheBall.Instance.dequeueItem (true);
		//if (isPerfect) {
		//	GameManager.Instance.Point += 2 * GlobalValue.combo;
			SoundManager.PlaySfx (perfectSound, perfectSoundVolume);
		//	GameManager.Instance.ShowFloatingText ("+" + 2 * GlobalValue.combo, transform.position, Color.red);

		//	GlobalValue.combo++;
		//} else {
		//	GlobalValue.combo = 1;
			GameManager.Instance.Point++;
		//}
	}

	// // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // 
	// ON TRIGGER  //
	// This is where the collision detection is done 
	void OnTriggerEnter2D(Collider2D other){
		Debug.Log (itemValue);
		if (!end && (other.tag == "Compost" || other.tag == "Landfill" || other.tag == "Recycle")) {
			Debug.Log ("Ball/onTriggerEnter/!end && (other.tag == recycle, landfill, or compost");
			if (transform.position.y < other.gameObject.transform.position.y) {
				Debug.Log ("Ball/onTriggerEnter/!end && (other.tag == Success || other.tag == Fail/transform.position.y < other.gameObject.transform.position.y");
				return;
			}

			if (other.tag == "Landfill") {
				Debug.Log ("Landfill");
				if (itemValue == "Landfill") { // check the item value of the object, is it LandFill? 
					AddPoint ();
				} else {
					Debug.Log ("WRONG");
					TheBall.Instance.dequeueItem (false);
				}

			} else if (other.tag == "Compost") {
				Debug.Log ("Compost");
				if (itemValue == "Compost") { // check the item value of the object, is it Compost? 
					AddPoint ();
				} else {
					Debug.Log ("WRONG");
					TheBall.Instance.dequeueItem (false);
				}
			} else if (other.tag == "Recycle") {
				Debug.Log ("Recycle");
				if (itemValue == "Recycle") { // check the item value of the object, is it Recycle? 
					AddPoint ();
				} else {
					Debug.Log ("WRONG");
					TheBall.Instance.dequeueItem (false);
				}
			}
//			else if (other.tag == "Fail") {
//				Debug.Log ("Ball/onTriggerEnter/!end && (other.tag == Success || other.tag == Fail/Fail");
//				GameManager.Instance.GameOver ();
//			}
			Debug.Log ("Ball/onTriggerEnter..ending");
			coll.enabled = false;
			anim.SetTrigger ("disappear");
			Destroy (gameObject, 0f);
			end = true;
		}
	}
} // end of ball class