using UnityEngine;
using System.Collections;

public static class VectorUtils {

	public static Vector2 up = new Vector2 (0, 1);
	public static Vector2 left = new Vector2 (-1, 0);
	public static Vector2 down = new Vector2 (0, -1);
	public static Vector2 right = new Vector2 (1, 0);

	// Returns the Vector that is closer to target
	public static Vector2 ShortestDistance (Vector2 a, Vector2 b, Vector2 target) {
		if (Vector2.Distance (a, target) < Vector2.Distance (b, target)) {
			return a;
		} else {
			return b;
		}
	}

	// Eliminates the decimal parts of the coordinates of the given Vector2
	public static Vector2 Truncate (Vector2 a) {
		float newX = Mathf.Round (a.x);
		float newY = Mathf.Round (a.y);

		return new Vector2(newX, newY);
	}

	// Determines whether the given Vector2s are opposite from one another
	public static bool AreOpposite (Vector2 a, Vector2 b) {
		return a.Equals (-b);
	}

	// Truncates the value of a Vector2 to stay inside the scenario
	public static Vector2 RemoveMapOverflows (Vector2 a) {
		// TODO: mirar lo de "out" en los argumentos
		if (a.x < 0)
			a.Set (0, a.y);
		if (a.y < 0)
			a.Set (a.x, 0);
		if (a.x > 27)
			a.Set (27, a.y);
		if (a.y > 30)
			a.Set (a.x, 30);

		return a;
	}

	public static Vector2 GetRandomVector () {
		float randomX = Random.Range (0, 27);
		float randomY = Random.Range (0, 30);
		
		return new Vector2 (randomX, randomY);
	}

}
