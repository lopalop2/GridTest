using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour {

	PlayerMovement movement;
	int[] Modifiers;
	public Text movementText;
	enum ModNums
	{
		movement = 0,

		numMods
	};

	// Use this for initialization
	void Start () {
		movement = GetComponent<PlayerMovement> ();
		Modifiers = new int[(int)ModNums.numMods];
		Modifiers [(int)ModNums.movement] = 0;
		updateModifiers ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.Space))
			NextTurn ();
	}

	public void NextTurn()
	{
		movement.NextTurn (Modifiers[(int)ModNums.movement]);
	}

	void updateModifiers()
	{
		movementText.text = Modifiers [(int)ModNums.movement].ToString();
	}
}
