using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {
	Vector3 mousePos;
	Ray ray;
	RaycastHit target;
	GridManager gridManager;
	public Camera cam;
	PathFinder path;
	public int maxMovement = 5;
	int currentMovement = 5;
	Vector2 moveLoc = new Vector2(0,0);
	SpriteRenderer sprite;
	public Text curMove,maxMove;
	PathFinder.ReturnVal ret;
	bool start = true;

	// Use this for initialization
	void Start () {
		gridManager = FindObjectOfType<GridManager> ();
		path = GetComponent<PathFinder> ();
		sprite = GetComponent<SpriteRenderer> ();
		maxMove.text = curMove.text = maxMovement.ToString();
		for (int i = 0; i < cam.layerCullDistances.Length; i++) {
			cam.layerCullDistances [i] = 1000;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (!gridManager.doneLoad)
			return;
		if (start) 
		{
			transform.position = gridManager.gridPositions [(int)PlayerPrefs.GetInt ("playerX"), (int)PlayerPrefs.GetInt ("playerY")];
			gridManager.playerLoc [0] = PlayerPrefs.GetInt ("playerX");
			gridManager.playerLoc [1] = PlayerPrefs.GetInt ("playerY");
			transform.position = new Vector3 (transform.position.x, .5f, transform.position.z);
			start = false;
		}
		//find currently targeted cube
		mousePos = Input.mousePosition;
		ray = cam.ScreenPointToRay (mousePos);
		if (Physics.Raycast (ray.origin, ray.direction, out target)) {			
			int i, c = 0;
			bool b = false;
			for (i = 0; i < GridManager.GRIDSIZE; i++) {
				for (c = 0; c < GridManager.GRIDSIZE; c++) {
					if (gridManager.gridPositions[i,c] == target.transform.position) {

						//target changed
						if (gridManager.curTarg [0] != i || gridManager.curTarg [1] != c) {
							gridManager.curTarg = new int[2] { i, c };

							//build path to current target
							ret = path.findRoute (gridManager.playerLoc [0], gridManager.playerLoc [1], i, c, currentMovement);
							moveLoc = ret.moveLoc;
							//flip sprite
							if (target.collider.transform.position.x < transform.position.x)
								sprite.flipX = true;
							else
								sprite.flipX = false;
						}
						b = true;
						break;
					}
				}
				if (b)
					break;
			}

			//move
			if (Input.GetMouseButtonDown (0) && i < GridManager.GRIDSIZE && c < GridManager.GRIDSIZE) {
					path.done1 = false;
					path.reset ();
					transform.position = gridManager.gridPositions [(int)moveLoc.x, (int)moveLoc.y];
					transform.position = new Vector3 (transform.position.x, .5f, transform.position.z);
					gridManager.playerLoc [0] = (int)moveLoc.x;
					gridManager.playerLoc [1] = (int)moveLoc.y;
					currentMovement -= ret.moveDist;
					curMove.text = currentMovement.ToString ();
				
			}
		}
	}

	public void NextTurn(int buffs = 0)
	{
		currentMovement = maxMovement + buffs;
		if (currentMovement < 0)
			currentMovement = 0;
		curMove.text = currentMovement.ToString();

	}
}
