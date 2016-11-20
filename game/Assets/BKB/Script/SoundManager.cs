using UnityEngine;
using System.Collections;
/*
 * This is SoundManager
 * In other script, you just need to call SoundManager.PlaySfx(AudioClip) to play the sound
*/
public class SoundManager : MonoBehaviour {

	public static SoundManager Instance;

	[Tooltip("Play music clip when start")]
	public AudioClip musicsMenu;
	[Range(0,1)]
	public float musicMenuVolume = 0.5f;
	public AudioClip musicsGame;
	[Range(0,1)]
	public float musicsGameVolume = 0.5f;

	[Tooltip("Place the sound in this to call it in another script by: SoundManager.PlaySfx(soundname);")]
	public AudioClip soundClick;
	public AudioClip soundGamefinish;
	public AudioClip soundGameover;

	private AudioSource musicAudio;
	private AudioSource soundFx;

	// // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // //

	//GET and SET
	public static float MusicVolume{
		set{ Instance.musicAudio.volume = value; }
		get{ return Instance.musicAudio.volume; }
	}
	public static float SoundVolume{
		set{ Instance.soundFx.volume = value; }
		get{ return Instance.soundFx.volume; }
	}

	// // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // //
	// AWAKE // 
	// Use this for initialization
	void Awake(){
		Instance = this;
		musicAudio = gameObject.AddComponent<AudioSource> ();
		musicAudio.loop = true;
		musicAudio.volume = 0.5f;
		soundFx = gameObject.AddComponent<AudioSource> ();
	}

	// // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // //
	// START // 
	void Start () {
//		//Check auido and sound
//		if (!GlobalValue.isMusic)
//			musicAudio.volume = 0;
//		if (!GlobalValue.isSound)
//			soundFx.volume = 0;
//	  PlayMusic(musicsGame,musicsGameVolume);
	}

	// // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // //
	// functions that play various sound for the game 
	public static void PlaySfx(AudioClip clip){				//play Game Sound FX 
		Instance.PlaySound(clip, Instance.soundFx);
	}

	public static void PlaySfx(AudioClip clip, float volume){ //play Game Sound FX 
		Instance.PlaySound(clip, Instance.soundFx, volume);
	}

	public static void PlayMusic(AudioClip clip){			//play game music 
		Instance.PlaySound (clip, Instance.musicAudio);
	}

	public static void PlayMusic(AudioClip clip, float volume){	 //play game music 
		Instance.PlaySound (clip, Instance.musicAudio, volume);
	}

	private void PlaySound(AudioClip clip,AudioSource audioOut){	// play game sounds 
		if (clip == null) {
//			Debug.Log ("There are no audio file to play", gameObject);
			return;
		}

		if (audioOut == musicAudio) {
			audioOut.clip = clip;
			audioOut.Play ();
		} else
			audioOut.PlayOneShot (clip, SoundVolume);
	}

	private void PlaySound(AudioClip clip,AudioSource audioOut, float volume){ // play game sounds
		if (clip == null) {
//			Debug.Log ("There are no audio file to play", gameObject);
			return;
		}

		if (audioOut == musicAudio) {
			audioOut.clip = clip;
			audioOut.Play ();
		} else
			audioOut.PlayOneShot (clip, SoundVolume * volume);
	}
	// // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // //
}// end of Sound 
