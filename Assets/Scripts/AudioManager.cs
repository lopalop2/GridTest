using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
	AudioSource bgm;
	AudioSource sfx;
	public enum Tracklist
	{
		cave,

		numtracks
	};
	[SerializeField]
	AudioClip[] sound = new AudioClip[(int)Tracklist.numtracks];

	// Use this for initialization
	void Start () 
	{
		DontDestroyOnLoad (this.gameObject);
		if(FindObjectsOfType<AudioManager> ().Length > 1)
			FindObjectsOfType<AudioManager> ()[0].gameObject.SetActive(false);
		bgm = GetComponents<AudioSource> () [0];
		sfx = GetComponents<AudioSource> () [1];
		if (SceneManager.GetActiveScene ().name.Contains ("Cave"))
			PlayBGM (Tracklist.cave);
	}

	void PlayBGM(Tracklist track)
	{
		bgm.clip = sound [(int)track];
		bgm.Play ();
	}

	void PlaySFX()
	{
		sfx.Play ();
	}

}
