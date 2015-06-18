using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// This class represents a single tile from the scenario.
/// Each tile holds the possible directions of travel to choose.
/// </summary>
public class Tile {
	private IList<Vector2> directions = new List<Vector2> ();

	bool Up() {return false;}
	bool Down() {return false;}
	bool Left() {return false;}
	bool Right() {return false;}

	public void AddDirection (Vector2 dir) {
		directions.Add (dir);
	}

	public IList<Vector2> GetDirections () {
		return directions;
	}
	
}

