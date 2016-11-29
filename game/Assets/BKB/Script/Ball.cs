using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour { 
	public Vector3 scaleTo;		//the ball image will be scale to this size after it's fired
	public float speed = 1f;	//the speed to scale the ball
	public string itemValue; // value of item (recycling, compost, landfill)
	[HideInInspector]
	public bool fire = false;	//telling that the ball is fired already
	public Transform Ballsprite;

    private int compostCount = 2;
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

	// Use this for initialization
	void Start () {
		Debug.Log ("Ball/Start");
		rig = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		coll = GetComponent<CircleCollider2D> ();
		coll.enabled = true;
	}

	public void Fire(){
		Debug.Log ("Ball/Fire");
		fire = true;
		coll.enabled = false;		//turn off collider when the ball begin move up to avoid the unnecessary collides
	}

	public void ChangeSprite(Sprite sprite){
		Debug.Log ("Ball/ChangeSprite");
		Ballsprite.GetComponent<SpriteRenderer> ().sprite = sprite;		//change the ball sprite
	}

	public void AssignValue(int index){
		if (index < compostCount)
			itemValue = "Compost";
		else if (index < compostCount + landfillCount)
			itemValue = "Landfill";
		else
			itemValue = "Recycle";
	}

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

	void OnMouseDown() {
		Debug.Log ("Ball/onMouseDown");
		touchTheBall = true;		//touch on the ball, mean the first touch must on the ball then we can fire it
	}

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
				if (itemValue == "Landfill") {
					AddPoint ();
				} else {
					Debug.Log ("WRONG");
					TheBall.Instance.dequeueItem (false);
				}

			} else if (other.tag == "Compost") {
				Debug.Log ("Compost");
				if (itemValue == "Compost") {
					AddPoint ();
				} else {
					Debug.Log ("WRONG");
					TheBall.Instance.dequeueItem (false);
				}
			} else if (other.tag == "Recycle") {
				Debug.Log ("Recycle");
				if (itemValue == "Recycle") {
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
}
