using UnityEngine;
using System.Collections;

public static class VectorUtils {

	public static Vector2 up = new Vector2 (0, 1);
	public static Vector2 left = new Vector2 (-1, 0);
	public static Vector2 down = new Vector2 (0, -1);
	public static Vector2 right = new Vector2 (1, 0);
	
	public static Vector2 ShortestDistance (Vector2 a, Vector2 b, Vector2 target) {
		if (Vector2.Distance (a, target) < Vector2.Distance (b, target)) {
			return a;
		} else {
			return b;
		}
	}

	public static Vector2 Truncate (Vector2 a) {
		float newX = (float)((int) a.x);
		float newY = (float)((int) a.y);

		return new Vector2(newX, newY);
	}

	public static bool AreOpposite (Vector2 a, Vector2 b) {
		return a.Equals (-b);
	}

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

}
