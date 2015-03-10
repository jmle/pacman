using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GhostHomeManager : MonoBehaviour {

	public GhostController[] ghosts = new GhostController[4];

	private GhostController currentGhost;
	private int currentGhostIndex;
	private int globalDotCounter;
	private bool livesLost;

	// Use this for initialization
	void Start () {
		currentGhost = ghosts[currentGhostIndex];
	}
	
	// Update is called once per frame
	void Update () {

	}

	// Called each time a dot is eaten
	void pacmanAteDot() {
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

	private void UpdateGlobalCounter () {
		// Broadcast message to all ghosts
		gameObject.BroadcastMessage ("IncrementGlobalCounter");
	}

	private void UpdateCurrentGhost () {
		try {
			currentGhost = ghosts[++currentGhostIndex];
		} catch (System.IndexOutOfRangeException) {
			currentGhost = null;
		}
	}
}
