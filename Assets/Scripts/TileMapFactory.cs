using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class TileMapFactory : MonoBehaviour {

	private Tile[,] tileMap = new Tile[28,31];

	void Start () {
		tileMap = CreateTileMap ();
	}

	private Tile[,] CreateTileMap() {
		StreamReader sr = new StreamReader("tile_dir_mapping");

		string line;
		while ((line = sr.ReadLine ()) != null) {
			ParseLine (line);
		}

		return tileMap;
	}

	private void ParseLine(string line) {
		if (line == "") {
			return;
		}
		Tile tile = new Tile ();

		line.Trim();		//Debug.Log (line);
		char[] separators = {' ', ',', '(', ')'};
		string[] split = line.Split (separators);

		int x = int.Parse (split[1]);
		int y = int.Parse (split[2]);
		// Debug.Log (x + "," + y);

		for (int i = 4; i < split.Length; i++) {
			switch (split[i].Trim ()) {
			case "l":
				// Debug.Log ("Adding LEFT");
				tile.AddDirection (Direction.left);
				break;
			case "r":
				// Debug.Log ("Adding RIGHT");
				tile.AddDirection (Direction.right);
				break;
			case "u":
				// Debug.Log ("Adding UP");
				tile.AddDirection (Direction.up);
				break;
			case "d":
				// Debug.Log ("Adding DOWN");
				tile.AddDirection (Direction.down);
				break;
			default:
				break;
			}
		}

		tileMap[x,y] = tile;
	}

	public Tile[,] GetTileMap () {
		return tileMap;
	}

}
