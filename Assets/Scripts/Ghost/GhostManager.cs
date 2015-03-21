using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// TODO: add scatter stuff and timers for dots
public class GhostManager : MonoBehaviour {

	public GhostController[] ghosts = new GhostController[4];
	public GhostState currentGlobalState;

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

	// Called each time an energizer is eaten
	public void PacmanAteEnergizer () {
		// TODO: Add counter to go back to normal
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
