using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// TODO: add scatter stuff and timers for dots
public class GhostManager : MonoBehaviour {

	public GhostController[] ghosts = new GhostController[4];

	private GhostController currentGhost;
	private int currentGhostIndex;
	private int globalDotCounter;
	private bool livesLost;

	// Use this for initialization
	void Start () {
		currentGhost = ghosts[currentGhostIndex];
	}

	// Called each time a dot is eaten
	public void PacmanAteDot() {
		if (!livesLost) {
			UpdateGhostCounter ();
		} else {
			UpdateGlobalCounter ();
		}
	}

	private void UpdateGhostCounter () {
		if (currentGhost != null) {
			if (currentGhost.GetGhostState() == GhostState.HOME) {
				currentGhost.IncrementDotCounter();
			} else {
				UpdateCurrentGhost();
			}
		}
	}

	private void UpdateCurrentGhost () {
		try {
			currentGhost = ghosts[++currentGhostIndex];
		} catch (System.IndexOutOfRangeException) {
			// If the current ghost is null, it will mean that all ghosts have exited
			// the house.
			currentGhost = null;
		}
	}

	private void UpdateGlobalCounter () {
		// Broadcast message to all ghosts (children)
		gameObject.BroadcastMessage ("IncrementGlobalCounter");
	}

}
