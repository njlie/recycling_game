using UnityEngine;
using System.Collections;

/*
 * stretchs or fits the game into the screen 
*/

public class BGStretch : MonoBehaviour {

	float srx, sry;

	void Start () {
		srx = GetComponent<SpriteRenderer> ().bounds.size.x;
		sry = GetComponent<SpriteRenderer> ().bounds.size.y;
		float xmas = Screen.width*Camera.main.orthographicSize*2.0f /(Screen.height*1.0f);//
		float yScale = Camera.main.orthographicSize*2.0f  / sry; 
		float xScale = 0;
		if (Screen.height > Screen.width)
			xScale = xmas / srx;
		else
			xScale = 1.5f; //for web view etc . you can change 1.5 according to you
		transform.localScale = new Vector3 (xScale,yScale,1);// I am using 2d so z doesn't needed.
	}
} //end of BGStretch
