using UnityEngine;
using System.Collections;

// ==============================================================================================================================
// Used to manage the baskets, or in this version of the game the jellys 
// ==============================================================================================================================

public class Basket : MonoBehaviour {

	public Transform[]      Destination;		    //moving destinations
	public Transform        centerDes;	

	private int             currentDestination;	
    private	Vector2         target;
	public  int             pointToMove = 10;
	public  int             pointToMove2 = 20;
	public  float           speed = 1;
	private int             limitDes = 2;
	private bool            allowMoving = false;    //used for moving baskits, not needed for this version of the game 


    // ============================================================================================================================== 
    // START
    // Use this for initialization
    // ==============================================================================================================================

    void Start () {
		currentDestination = 0;
		target = Destination [currentDestination].position;
	}


    // ==============================================================================================================================
    // UPDATE
    // Update is called once per frame
    // ==============================================================================================================================

    void Update () {
		if (!allowMoving) 
            //alway moving to the first point, that's mean the center point
			transform.position = Vector2.MoveTowards (transform.position, centerDes.position, speed * Time.deltaTime);
		if (GameManager.Instance.Point >= pointToMove)
			allowMoving = true;
		if (GameManager.Instance.Point >= pointToMove2)
			limitDes = 3;
	}


    // ==============================================================================================================================
    // RESET 
    //called by GameManager when gameover
    // ==============================================================================================================================

    public void Reset(){
		currentDestination = 0;
		target = Destination [currentDestination].position;
		allowMoving = false;
	}


} // end of Basket