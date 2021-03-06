﻿using UnityEngine;
using System.Collections;

/// <summary>
/// Holds the possible directions.
/// </summary>
public static class Direction {
	// Ordered in ghost preference.
	public static Vector2 up = new Vector2 (0, 1);
	public static Vector2 left = new Vector2 (-1, 0);
	public static Vector2 down = new Vector2 (0, -1);
	public static Vector2 right = new Vector2 (1, 0);
}
