using UnityEngine;
using System.Collections;

// ==============================================================================================================================
// Ball class is the trash objects themselves  
// Ball is used to refer to trash objects for clarity
// called by TheBall class 
// ==============================================================================================================================

public class Ball : MonoBehaviour {

    public  Vector3     scaleTo;	            // the ball image will be scale to this size after it's fired
	public  float       speed = 1f;             // the speed to scale the ball
	public  string      itemValue;              // value of item (recycling, compost, landfill)

	[HideInInspector]
	public  bool        fire = false;           // telling that the ball is fired already
	public  bool        touchTheBall = false;
    private bool        end = false;
    public  Transform   Ballsprite;
    public  int         points;
	private int         compostCount = 2;       // values used to assign compost index
    private int         landfillCount = 2;      // values used to assign landfill index
    private int         recycleCount = 8;       // values used to assign recycle index
    private float       trashTimer = 0;
    
	Rigidbody2D         rig;
	Animator anim;
	CircleCollider2D coll;

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


    // ==============================================================================================================================
    // START
    // Use this for initialization
    // ==============================================================================================================================

    void Start () {
		rig = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		coll = GetComponent<CircleCollider2D> ();
		coll.enabled = true;
	}


    // ==============================================================================================================================
    // FIRE
    // ==============================================================================================================================

    public void Fire() {
		fire = true;
        //turn off collider when the ball begin move up to avoid the unnecessary collides
		coll.enabled = false;		
	}

    // ==============================================================================================================================
    // CHANGE SPRITE 
    // ==============================================================================================================================

    public void ChangeSprite(Sprite sprite) {
        //change the sprite
		Ballsprite.GetComponent<SpriteRenderer> ().sprite = sprite;		
	}


    // ==============================================================================================================================
    // ASSIGN VAlUE
    // assign the value of each object 
    // ==============================================================================================================================

    public void AssignValue(int index) {
        // compost item
		if (index < compostCount)
			itemValue = "Compost";
        // landfill item
		else if (index < compostCount + landfillCount)
			itemValue = "Landfill";	 
        // recycle item
		else
			itemValue = "Recycle";
	}


    // ==============================================================================================================================
    // UPDATE
    // Update is called once per frame
    // ==============================================================================================================================

    void Update () {
		if (end)
			return;
        
        // scale the ball with the speed
		if (fire)	
			Ballsprite.transform.localScale = Vector3.Lerp (Ballsprite.transform.localScale, scaleTo, speed * Time.deltaTime);

        // just allow enable collider when the ball begin falling
        if (rig.velocity.y < 0) {
            coll.enabled = true;
            trashTimer += Time.deltaTime;
            print(trashTimer);
        }
        
        if (trashTimer >= 0.5f )
        {
            TheBall.Instance.dequeueItem(false);
            coll.enabled = false;
            anim.SetTrigger("disappear");
            Destroy(gameObject, 0f);
            end = true;
        }
        
    }


    // ==============================================================================================================================
    // MOUSE DOWN 
    // ==============================================================================================================================

    void OnMouseDown() {
        // touch on the ball, mean the first touch must on the ball then we can fire it
		touchTheBall = true; 
	}


    // ==============================================================================================================================
    // ADD POINT
    // ==============================================================================================================================

    void AddPoint() {
		TheBall.Instance.dequeueItem (true);
			SoundManager.PlaySfx (perfectSound, perfectSoundVolume);
			GameManager.Instance.Point++;
	}


    // ==============================================================================================================================
    // ON TRIGGER 
    // This is where the collision detection is done 
    // ==============================================================================================================================

    void OnTriggerEnter2D(Collider2D other) {
		if (!end && (other.tag == "Compost" || other.tag == "Landfill" || other.tag == "Recycle")) {
			if (transform.position.y < other.gameObject.transform.position.y) {
				return;
			}

			if (other.tag == "Landfill") {
				// check the item value of the object, is it LandFill?
				if (itemValue == "Landfill") {  
					AddPoint ();
				} else {
					TheBall.Instance.dequeueItem (false);
				}

			} else if (other.tag == "Compost") {
				// check the item value of the object, is it Compost?
				if (itemValue == "Compost") {  
					AddPoint ();
				} else {
					TheBall.Instance.dequeueItem (false);
				}
			} else if (other.tag == "Recycle") {
				// check the item value of the object, is it Recycle?
				if (itemValue == "Recycle") {  
					AddPoint ();
				} else {
					TheBall.Instance.dequeueItem (false);
				}
			}

			coll.enabled = false;
			anim.SetTrigger ("disappear");
			Destroy (gameObject, 0f);
			end = true;
		}
	}


} // end of ball class
