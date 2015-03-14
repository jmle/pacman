using UnityEngine;
using System.IO;
using System.Collections;

public class DotInstantiator : MonoBehaviour {

	public Transform dot;
	public Transform energizer;

	// Use this for initialization
	void Start () {
		StreamReader sr = new StreamReader("dot_mapping");

		string row;
		int i = 0;
		while ((row = sr.ReadLine ()) != null) {
			InstantiateDotsForRow (row, i);
			i++;
		}
	}

	private void InstantiateDotsForRow (string dots, int row) {
		int col = 0;
		foreach (char c in dots) {
			switch (c) {
			case 'o':
				Instantiate (dot, new Vector2 (col, row), Quaternion.identity);
				break;
			case 'O':
				Instantiate (energizer, new Vector2 (col, row), Quaternion.identity);
				break;
			default:
				break;
			}

			col++;
		}
	}

}
