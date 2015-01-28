using UnityEngine;
using System.Collections;

public enum GhostState {
	CHASE,				// Chasing Pacman
	SCATTER,			// Going to the Scatter position
	FRIGHTENED,			// Pacman ate the energizer
	HOME,				// Waiting to be released
	DEAD,				// Was just eaten
}
