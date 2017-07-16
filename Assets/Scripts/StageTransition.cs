using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageTransition : MonoBehaviour {
	[SerializeField]
	public string target;
	public int[] gridPos;
	GridManager gridManager;

	// Use this for initialization
	void Start () {
		gridManager = FindObjectOfType<GridManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Load()
	{
 		if (gridManager.playerLoc [0] == 9)
			gridManager.playerLoc [0] = 0;
		else if (gridManager.playerLoc [0] == 0)
			gridManager.playerLoc [0] = 9;
		if (gridManager.playerLoc [1] == 9)
			gridManager.playerLoc [1] = 0;
		else if (gridManager.playerLoc [1] == 0)
			gridManager.playerLoc [1] = 9;
		PlayerPrefs.SetInt("playerX", gridManager.playerLoc[0]);
		PlayerPrefs.SetInt("playerY", gridManager.playerLoc[1]);
		PlayerPrefs.SetString ("curRoom", target);
		SceneManager.LoadScene (target);
	}
}
