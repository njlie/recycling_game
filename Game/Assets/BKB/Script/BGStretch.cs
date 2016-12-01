using UnityEngine;
using System.Collections;

// ==============================================================================================================================
// BGStretch stretchs or fits the image to the screen 
// ==============================================================================================================================

public class BGStretch : MonoBehaviour {

	private float       srx, sry;


    // ==============================================================================================================================
    // START
    // ==============================================================================================================================

    void Start () {
		srx = GetComponent<SpriteRenderer> ().bounds.size.x;
		sry = GetComponent<SpriteRenderer> ().bounds.size.y;
		float xmas = Screen.width*Camera.main.orthographicSize*2.0f /(Screen.height*1.0f);//
		float yScale = Camera.main.orthographicSize*2.0f  / sry; 
		float xScale = 0;
		if (Screen.height > Screen.width)
			xScale = xmas / srx;
		else
			xScale = 1.5f;
		transform.localScale = new Vector3 (xScale,yScale,1);
	}


} // end of BGStretch
