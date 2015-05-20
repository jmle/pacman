using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Manages the group of ghosts and keeps track of
/// global states for all ghosts
/// </summary>
public class GhostManager : MonoBehaviour {

	public GhostController[] ghosts = new GhostController[4];
	public GhostState currentGlobalState;
	private GhostController currentGhost;

	private int currentGhostIndex;
	private int globalDotCounter;
	private bool livesLost;

	// Timing variables for global state changes
	private static float FRIGHTENED_TIME = 5;
	private static float SCATTER_TIME = 7;
	private static float CHASE_TIME = 20;
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
		case GhostState.CHASE:
			UpdateAutomaticStateChange (CHASE_TIME, GhostState.SCATTER);
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
		ChangeStateTo (GhostState.FRIGHTENED);
	}

	private void UpdateAutomaticStateChange (float time, GhostState nextState) {
		if (!counting) {
			counting = true;
			elapsed = 0;
		} else {
			elapsed += Time.deltaTime;
			
			if (elapsed >= time) {
				counting = false;
				ChangeStateTo (nextState);
			}
		}
	}

	private void ChangeStateTo (GhostState state) {
		currentGlobalState = state;

		// Reset timers and stuff
		// TODO: separate counters if we want to persist previous timers
		elapsed = 0;
		counting = false;

		// Broadcast message to all ghosts
		switch (state) {
		case GhostState.FRIGHTENED:
			gameObject.BroadcastMessage ("GoFrightened");
			break;
		case GhostState.CHASE:
			gameObject.BroadcastMessage ("StartChasing");
			break;
		case GhostState.SCATTER:
			gameObject.BroadcastMessage ("Scatter");
			break;
		default:
			break;
		}
	}

	private void UpdateGlobalCounter () {
		// Broadcast message to all ghosts (children)
		gameObject.BroadcastMessage ("IncrementGlobalCounter");
	}

	private void UpdateGhostCounter () {
		if (currentGhost != null) {
			GhostStateManager ghostStateManager = currentGhost.GetGhostStateManager();
//			if (ghostStateManager.GetGhostState() == GhostState.HOME) {
//				ghostStateManager.IncrementDotCounter();
//			} else {
				UpdateCurrentGhost();
//			}
		}
	}

	private void UpdateCurrentGhost () {
		try {
			currentGhost = ghosts[++currentGhostIndex];
		} catch (System.IndexOutOfRangeException) {
			// If the current ghost is null, it will mean that all ghosts have exited
			// the house.
			// TODO: ghosts could enter the house again, meaning this needs more handling
			currentGhost = null;
		}
	}

	public GhostState GetCurrentGlobalState () {
		return currentGlobalState;
	}

}
