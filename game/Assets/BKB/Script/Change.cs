
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class change : MonoBehaviour {
	public Transform mainMenu, optionsMenu;
	public AudioSource someAudioSource;

	// Reference to the AudioSource component

	public void Start ()
	{

	}

	// Turns Music on and off
	public void musicOnOFF()
	{
		if (someAudioSource.mute == true)
			someAudioSource.mute = false;
		else
			someAudioSource.mute = true;
	}

	// Update is called once per frame
	public void changeToScene (string sceneTochangeTo)
	{
		//Application.LoadLevel (sceneTochangeTo);

		SceneManager.LoadScene (sceneTochangeTo);
	}

	public void QuitGame(){
		Application.Quit ();
	}

	public void OptionsMenu (bool clicked){
		if (clicked == true) {
			optionsMenu.gameObject.SetActive (clicked);
			mainMenu.gameObject.SetActive (false);
		} else {
			optionsMenu.gameObject.SetActive (clicked);
			mainMenu.gameObject.SetActive (true);
		}
	}

}
