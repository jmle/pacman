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

	// Timing variables for global state changes
	private static float FRIGHTENED_TIME = 5;
	private static float SCATTER_TIME = 5;
	private static float CHASE_TIME = 5;
	private float elapsed;
	private bool counting;

	// Use this for initialization
	void Start () {
		currentGhost = ghosts[currentGhostIndex];
	}

	void Update () {
		// Control timing of these states
		switch (currentGlobalState) {
		case GhostState.SCATTER:
			UpdateAutomaticStateChange (SCATTER_TIME, GhostState.CHASE);
			break;
		case GhostState.FRIGHTENED:
			UpdateAutomaticStateChange (FRIGHTENED_TIME, GhostState.CHASE);
			break;
		default:
			break;
		}
	}

	// Called each time a dot is eaten
	public void PacmanAteDot() {
		if (livesLost) {
			UpdateGlobalCounter ();
		} else {
			UpdateGhostCounter ();
		}
	}
	
	public void PacmanAteEnergizer () {
		currentGlobalState = GhostState.FRIGHTENED;
		gameObject.BroadcastMessage ("GoFrightened");
	}

	private void UpdateAutomaticStateChange (float time, GhostState nextState) {
		if (!counting) {
			counting = true;
			elapsed = 0;
		} else {
			elapsed += Time.deltaTime;
			
			if (elapsed >= time) {
				counting = false;
				currentGlobalState = nextState;
			}
		}
	}

	private void UpdateGlobalCounter () {
		// Broadcast message to all ghosts (children)
		gameObject.BroadcastMessage ("IncrementGlobalCounter");
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
