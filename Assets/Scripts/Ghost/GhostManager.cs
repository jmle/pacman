using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Manages the group of ghosts and keeps track of
/// global states for all ghosts
/// </summary>
// TODO: add scatter stuff and timers for dots
public class GhostManager : MonoBehaviour {

	public GhostController[] ghosts = new GhostController[4];
	public GhostState currentGlobalState;
	private GhostController currentGhost;

	private int currentGhostIndex;
	private int globalDotCounter;
	private bool livesLost;

	private float frightenedTimeout;
	private float scatterTimeout;

	// Use this for initialization
	void Start () {
		currentGhost = ghosts[currentGhostIndex];
	}

	// Called each time a dot is eaten
	public void PacmanAteDot() {
		if (livesLost) {
			UpdateGlobalCounter ();
		} else {
			UpdateGhostCounter ();
		}
	}
	
	private void UpdateGlobalCounter () {
		// Broadcast message to all ghosts (children)
		gameObject.BroadcastMessage ("IncrementGlobalCounter");
	}

	public void PacmanAteEnergizer () {
		// TODO: Add counter to go back to normal
		currentGlobalState = GhostState.FRIGHTENED;
		gameObject.BroadcastMessage ("GoFrightened");
	}

	private void UpdateGhostCounter () {
		if (currentGhost != null) {
			GhostStateManager ghostStateManager = currentGhost.GetGhostStateManager();
			if (ghostStateManager.GetGhostState() == GhostState.HOME) {
				ghostStateManager.IncrementDotCounter();
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

	public GhostState GetCurrentGlobalState () {
		return currentGlobalState;
	}

}
