using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine.UI;
public class GridManager : MonoBehaviour
{
	
	public static int GRIDSIZE = 10;
	public int [,] Grid;
	public Vector3 [,] gridPositions;
	public	Material Open, Closed, Movement, MoveDest, MoveLoc;
	public int[] curTarg = new int[2], playerLoc = new int[2];
	int[] lastTarg = new int[2];
	public MeshRenderer[,] meshRenderers;
	public string mapName;
	public GameObject gridSquare;
	public bool doneLoad = false;
	public MeshRenderer floor;
	StageTransition[] stageTransitions;
	public TextAsset map;
	//public Text debug;

	// Use this for initialization
	void Start ()
    {		
		//debug.text = "started";
		LoadMap ();
		stageTransitions = FindObjectsOfType<StageTransition> ();
	}
	
	// Update is called once per frame
	void Update ()
    {
		foreach (StageTransition transition in stageTransitions) 
		{
			if(playerLoc[0] == transition.gridPos[0] && playerLoc[1] == transition.gridPos[1])
			{
				transition.Load();
				break;
			}
		}
		if (!doneLoad)
			return;
		if (curTarg [0] != lastTarg [0] || curTarg [1] != lastTarg [1]) {
			if (meshRenderers [curTarg [0], curTarg [1]].material.color != MoveLoc.color) {
				meshRenderers [curTarg [0], curTarg [1]].material = MoveDest;
			}
			if (Grid [lastTarg [0], lastTarg [1]] == 1)
				meshRenderers [lastTarg [0], lastTarg [1]].material = Open;
			else
				meshRenderers [lastTarg [0], lastTarg [1]].material = Closed;
			lastTarg = curTarg;
		}
	}

	public void SetColor(int x, int y, Material col)
	{
		meshRenderers [x, y].material = col;
	}

	void SaveMap()
	{
		string path = "Assets/Maps/" + mapName + ".txt";

		StreamWriter writer = new StreamWriter(path, false);
		writer.WriteLine (GRIDSIZE);

		for (int i = 0; i < GRIDSIZE; i++)
		{
			for (int c = 0; c < GRIDSIZE; c++) 
			{
				writer.Write (Grid [i, c]);
			}
			writer.WriteLine ();
		}
		writer.Close();
	}

	void LoadMap()
	{
		string[] mapLines = Regex.Split(map.text, "\n");
		GRIDSIZE = int.Parse (mapLines [0]);
		Grid = new int[GRIDSIZE, GRIDSIZE];
		meshRenderers = new MeshRenderer[GRIDSIZE, GRIDSIZE];
		gridPositions = new Vector3[GRIDSIZE, GRIDSIZE];

		for (int i = 0; i < GRIDSIZE; i++)
		{
			for (int c = 0; c < GRIDSIZE; c++) 
			{
				Grid [i, c] = (int)char.GetNumericValue (mapLines [i + 1].ToCharArray () [c]);
				GameObject tile = Instantiate<GameObject> (gridSquare, new Vector3 (0, 0, 0), new Quaternion (0, 0, 0, 0), transform);
				tile.transform.localPosition = new Vector3 ((float)(c), 0, (float)(-i));
				gridPositions [i, c] = tile.transform.position;
				tile.transform.localRotation = new Quaternion (0, 0, 0, 0);

				meshRenderers [i, c] = tile.GetComponent<MeshRenderer> ();
				if (Grid [i, c] == 1) {
					meshRenderers [i, c].material = Open;
				} else
					meshRenderers [i, c].material = Closed;
			}
		}
		doneLoad = true;
		//debug.text = (doneLoad.ToString());
	}
}
