﻿using UnityEngine;
using System.Collections;

public enum GhostState {
	CHASE,				// Chasing Pacman
	SCATTER,			// Going to the Scatter position
	FRIGHTENED,			// Pacman ate the energizer
	DEAD,				// Was just eaten
	HOME,				// Waiting to be released
	EXIT,				// Exiting home
	START,				// About to start moving
}
