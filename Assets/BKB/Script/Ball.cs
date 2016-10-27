using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour { 
	public Vector3 scaleTo;		//the ball image will be scale to this size after it's fired
	public float speed = 1f;	//the speed to scale the ball
	[HideInInspector]
	public bool fire = false;	//telling that the ball is fired already
	public Transform Ballsprite;

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
		rig = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		coll = GetComponent<CircleCollider2D> ();
		coll.enabled = true;
	}

	public void Fire(){
		fire = true;
		coll.enabled = false;		//turn off collider when the ball begin move up to avoid the unnecessary collides
	}

	public void ChangeSprite(Sprite sprite){
		Ballsprite.GetComponent<SpriteRenderer> ().sprite = sprite;		//change the ball sprite
	}
	
	// Update is called once per frame
	void Update () {
		if (end)
			return;
		
		if(fire)	//scale the ball with the speed
			Ballsprite.transform.localScale = Vector3.Lerp (Ballsprite.transform.localScale, scaleTo, speed * Time.deltaTime);

		//just allow enable collider when the ball begin falling
		if (rig.velocity.y < 0) {	//if the ball begin falling, turn on the collider
			coll.enabled = true;
			//make the ball behide the basket bar when it's falling
			Ballsprite.GetComponent<SpriteRenderer> ().sortingOrder = 0;		//make the ball image behind the basket when it's falling
		}
	}

	void OnCollisionEnter2D(Collision2D other){
		SoundManager.PlaySfx (bounceSound[Random.Range(0,bounceSound.Length)], bounceSoundVolume);
		isPerfect = false;		//no perfect when the ball collide with the basket
	}

	void OnMouseDown() {
		touchTheBall = true;		//touch on the ball, mean the first touch must on the ball then we can fire it
	}

	void OnTriggerEnter2D(Collider2D other){
		if (!end && (other.tag == "Success" || other.tag == "Fail")) {
			if (transform.position.y < other.gameObject.transform.position.y)
				return;

			if (other.tag == "Success") {
				if (isPerfect) {
					
					GameManager.Instance.Point += 2 * GlobalValue.combo;
					SoundManager.PlaySfx (perfectSound, perfectSoundVolume);
					GameManager.Instance.ShowFloatingText ("+" + 2 * GlobalValue.combo, transform.position, Color.red);

					GlobalValue.combo++;
				} else {
					GlobalValue.combo = 1;
					GameManager.Instance.Point++;
				}

			} else if (other.tag == "Fail")
				GameManager.Instance.GameOver ();

			coll.enabled = false;
			anim.SetTrigger ("disappear");
			Destroy (gameObject, 0.2f);
			end = true;

		}
	}
}
