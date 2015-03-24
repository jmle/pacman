using UnityEngine;
using System.Collections;

public class GhostStateManager : MonoBehaviour {
	public GhostState ghostState;
	public int dotLimit;
	public int globalLimit;

	private GhostPathFinder pathFinder;
	private GhostManager ghostManager;
	private int dotCounter;
	private int globalCounter;

	// Use this for initialization
	void Start () {
		pathFinder = GetComponent<GhostPathFinder>();
		ghostManager = GetComponentInParent<GhostManager>();
	}
	
	// Update is called once per frame
	void Update () {
		switch (ghostState) {
		case GhostState.DEAD:
			HandleDeadGhost ();
			break;
		case GhostState.HOME:
			HandleGhostAtHome ();
			break;
		default:
			break;
		}
	}

	private void HandleDeadGhost () {
		if (HasArrived(pathFinder.GetTargetTile())) {
			// Blinky is the only ghost that never enters the house
			if (gameObject.name.Equals ("Blinky")) {
				ghostState = ghostManager.GetCurrentGlobalState ();
			} else {
				ghostState = GhostState.ENTER;
			}
		}
	}

	private void HandleGhostAtHome () {
		if (ghostState == GhostState.HOME) {
			if (dotCounter >= dotLimit || globalCounter >= globalLimit) {
				ghostState = GhostState.EXIT;
			}
		}
	}

	public void GoFrightened () {
		// We don't want to get frightened if we are inside the house
		if (ghostState == GhostState.CHASE || ghostState == GhostState.SCATTER) {
			ghostState = GhostState.FRIGHTENED;

			if (ghostState == GhostState.CHASE) {
				pathFinder.ChangeDirection ();
			}
		}
	}

	public void Die () {
		ghostState = GhostState.DEAD;
	}

	public void IncrementDotCounter () {
		dotCounter++;
	}
	
	public void IncrementGlobalCounter () {
		globalCounter++;
	}

	public GhostState GetGhostState () {
		return this.ghostState;
	}
	
	public void SetGhostState (GhostState ghostState) {
		this.ghostState = ghostState;
	}

	public void SetGhostStateToCurrentGlobalState () {
		ghostState = ghostManager.GetCurrentGlobalState ();
	}

	// TODO: duplicate from GhostScriptedMovements
	private bool HasArrived (Vector2 position) {
		return Vector2.Distance (rigidbody2D.position, position) < 0.1;
	}
}
