using UnityEngine;
using System.Collections;

/* 
Trash_Display class is used for the moving trash pile 
*/ 

public class TrashDisplay : MonoBehaviour {
	//public Sprite[] trashLevels; 
	GameObject trashpile;
	//public Vector3 pos1 = new Vector3(0,-3.6f,0);
	//public Vector3 pos2 = new Vector3(0,-4.4f,0);
	int trashcount = 0;
	float posy = -3.1f; 	 	// starting y position
	float posOffset = 0.05f; 	// y position offset 

	// // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // 
	// START //
	void Start () {
		// Set position of foreground/trashpile
		transform.position = new Vector3 (0, posy, 0);
	}

	// // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // 
	// UPDATE //
	void Update () {
		if (TheBall.Instance.count > trashcount) {
			posy -= posOffset;
			transform.position = new Vector3 (0, posy, 0);
			trashcount++;
		}
		//		if (TheBall.Instance.count == 4)
		//			changeSprite (10);
		//		else if (TheBall.Instance.count == 6)
		//			changeSprite (20);
	}

	// // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // 

	//	void changeSprite(int count){
	//		if (count == 10) {
	//			this.GetComponent<SpriteRenderer> ().sprite = trashLevels [0];
	//			transform.position = pos1;
	//		} else if (count == 20) {
	//			this.GetComponent<SpriteRenderer> ().sprite = trashLevels [1];
	//			transform.position = pos2;
	//		}
	//	}
}
