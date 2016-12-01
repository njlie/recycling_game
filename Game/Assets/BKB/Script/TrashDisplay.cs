using UnityEngine;
using System.Collections;

// ==============================================================================================================================
// Trash_Display class is used for the moving trash pile 
// ==============================================================================================================================

public class TrashDisplay : MonoBehaviour {

	GameObject trashpile;
	private     int     trashcount = 0;
	public      float   posy = -3.1f; 	 	            // starting y position
	private     float   posOffset = 0.05f;              // y position offset 


    // ==============================================================================================================================
    // START
    // ==============================================================================================================================

    void Start () {
		// Set position of foreground/trashpile
		transform.position = new Vector3 (0, posy, 0);
	}


    // ==============================================================================================================================
    // UPDATE
    // ==============================================================================================================================

    void Update () {
		if (TheBall.Instance.count > trashcount) {
			posy -= posOffset;
			transform.position = new Vector3 (0, posy, 0);
			trashcount++;
		}
	}


} // end of TrashDisplay
