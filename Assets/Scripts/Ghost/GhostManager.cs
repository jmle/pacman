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
	public void pacmanAteDot() {
		Debug.Log ("Pacman ate dot");
		if (!livesLost) {
			UpdateGhostCounter ();
		} else {
			UpdateGlobalCounter ();
		}
	}

	private void UpdateGhostCounter () {
		if (currentGhost != null) {
			currentGhost.IncrementDotCounter ();

			if (currentGhost.GetGhostState() != GhostState.HOME) {
				UpdateCurrentGhost();
			}
		}
	}
	
	private void UpdateCurrentGhost () {
		try {
			currentGhost = ghosts[++currentGhostIndex];
		} catch (System.IndexOutOfRangeException) {
			currentGhost = null;
		}
	}

	private void UpdateGlobalCounter () {
		// Broadcast message to all ghosts (children)
		gameObject.BroadcastMessage ("IncrementGlobalCounter");
	}

}
