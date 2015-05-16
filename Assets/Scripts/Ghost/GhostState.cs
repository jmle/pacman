using UnityEngine;
using System.Collections;

/// <summary>
/// Enum holding the different states a ghost can be in.
/// </summary>
public enum GhostState {
	CHASE,				// Chasing Pacman
	SCATTER,			// Going to the Scatter position
	FRIGHTENED,			// Pacman ate the energizer
	DEAD,				// Was just eaten
	HOME,				// Waiting to be released
	EXIT,				// Exiting home
	ENTER,				// Entering home
}
