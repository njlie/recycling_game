//class not need for this version of the game
/*
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Item : MonoBehaviour {
	public bool isFree = false;
	public int ID;		//set the ID of the item
	public int price;		//set the price of the item
	public Image itemImage;		//the image of the item
	bool isUnlock;	//check if it's unlocked or not
	public AudioClip soundUnlock;
	public GameObject Shop;

	// Use this for initialization
	void Start () {
		CheckUnlock ();		//check

	}

	private void CheckUnlock(){
		if (isFree)
			isUnlock = true;
		else
			isUnlock = ItemManager.Instance.isUnlocked (ID);
		
		if (isUnlock)
			itemImage.sprite = ItemManager.Instance.GetItemImage (ID);		//if the ball is unlocked, set the image for it
	}

	//call by the button event itself
	public void Click(){

	}
		
}
*/