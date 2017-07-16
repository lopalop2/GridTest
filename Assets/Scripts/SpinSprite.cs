using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinSprite : MonoBehaviour {
	float rotSpeed;

	// Use this for initialization
	void Start () {
		rotSpeed = Random.Range(0,9)*0.1f;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (new Vector3 (0, 0, rotSpeed));
	}
}
