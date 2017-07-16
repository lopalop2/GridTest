using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Initial : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if (PlayerPrefs.GetString ("curRoom") != SceneManager.GetActiveScene ().name) 
		{
			PlayerPrefs.SetInt("playerX", 5);
			PlayerPrefs.SetInt("playerY", 0);		
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
