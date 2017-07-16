using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour {
	GridManager gridManager;
	Vector2[] path;
	public bool done1 = false;
	Vector2 final;
	bool movedHor = false;

	public struct ReturnVal
	{
		public Vector2 moveLoc;
		public int moveDist;
	}

	ReturnVal ret;

	// Use this for initialization
	void Start () {
		gridManager = FindObjectOfType<GridManager> ();
	}
		
	public ReturnVal findRoute(int startRow, int startColumn, int goalRow, int goalColumn, int movement)
	{
		//clear previous path
		if (done1) 
		{
			reset ();
		}

		//return value
		final = new Vector3(startRow,startColumn,0);
		//sets length of path to available movement
		path = new Vector2[movement + 1];

		path [0] = final;
		int i;
		for (i = 1; i <= movement; i++)
		{
			gridManager.SetColor((int)final.x, (int)final.y, gridManager.Movement);
			//vertical movement
			if (Mathf.Abs (goalRow - final.x) > Mathf.Abs (goalColumn - final.y))
			{
				VerticalMove (goalRow, i);
				movedHor = false;
			}
			//stop building path if reached destination
			else if (Mathf.Abs (goalRow - final.x) == 0 && Mathf.Abs (goalColumn - final.y) == 0)
			{
				break;
			}
			//horizontal movement
			else 
			{
				HorizontalMove (goalColumn, i);
				movedHor = true;
			}
			//Prevent movement out of movable area
			if (gridManager.Grid [(int)path [i].x, (int)path [i].y] == 0)
			{
				path [i] = path [i - 1];
				final = path [i - 1];
				if (movedHor)
					VerticalMove (goalRow, i);
				else
					HorizontalMove (goalColumn, i);
				if (gridManager.Grid [(int)path [i].x, (int)path [i].y] == 0) {
					path [i] = path [i - 1];
					gridManager.SetColor ((int)path [i].x, (int)path [i].y, gridManager.MoveLoc);
					ret.moveLoc = new Vector2 (path [i].x, path [i].y);
					ret.moveDist = i - 1;
					return(ret);
				}
			}
		}

		if(!done1)
			done1 = true;
		gridManager.SetColor ((int)final.x, (int)final.y, gridManager.MoveLoc);
		ret.moveLoc = final;
		ret.moveDist = i - 1;
		return ret;
	}
	void HorizontalMove(int goalColumn, int i)
	{
		path [i].x = final.x;
		//moving down
		if(goalColumn - final.y > 0)
			path [i].y = ++final.y;
		//moving up
		else
			path [i].y = --final.y;
	}
	void VerticalMove (int goalRow, int i)
	{
		//moving right
		if(goalRow - final.x > 0)
			path [i].x = ++final.x;
		//moving left
		else
			path [i].x = --final.x;
		path [i].y = final.y;
	}


	//clears path
	public void reset()
	{
		foreach (var step in path) {
			if (gridManager.Grid [(int)step.x, (int)step.y] == 1)
				gridManager.SetColor((int)step.x, (int)step.y, gridManager.Open);
			else													
				gridManager.SetColor((int)step.x, (int)step.y, gridManager.Closed);
		}
	}
}
