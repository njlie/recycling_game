using UnityEngine;
using System.Collections;

// ==============================================================================================================================
// BasketManager is used to manage the types of games 
// for this version of the game we only care about timechallenge version 
// ==============================================================================================================================

public class BasketManager : MonoBehaviour {

	public static BasketManager Instance;		
	public enum         PlayMode{Easy};

	[HideInInspector]
	public PlayMode     Mode;
	public GameObject   timechallenge;          // game mode


    // ==============================================================================================================================
    // AWAKE
    // ==============================================================================================================================

    void Awake() {
		Instance = this;
		timechallenge.SetActive (true);
	}


    // ==============================================================================================================================
    // START
    // Use this for initialization
    // ==============================================================================================================================

    void Start () {
		Mode = PlayMode.Easy;
	}


    // ============================================================================================================================== 
    // TIME CHALLENGE
    // ==============================================================================================================================

    public void TimeChallenge() {
		timechallenge.SetActive (true);
		Mode = PlayMode.Easy;
		SoundManager.PlaySfx (SoundManager.Instance.soundClick);
	}


}// end of BasketManager