using UnityEngine;
using System.Collections;

/// <summary>
/// Manipulates the state of the ghost and keeps track of it.
/// </summary>
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
			// TODO: the above statement is actually false; he enters too
			if (gameObject.name.Equals ("Blinky")) {
				ghostState = ghostManager.GetCurrentGlobalState ();
			} else {
				ghostState = GhostState.ENTER;
			}
		}
	}

	private void HandleGhostAtHome () {
		if (dotCounter >= dotLimit || globalCounter >= globalLimit) {
			ghostState = GhostState.EXIT;
		}
	}

	public void GoFrightened () {
		// We don't want to get frightened if we are inside the house
		// TODO: in the actual game they change the sprite even inside the house. Is that a new state?
		// ^ The only thing that changes is the animation, so we can just add a new animation maybe
		if (ghostState == GhostState.CHASE || ghostState == GhostState.SCATTER) {
			// Ghosts reverse direction always in this case
			pathFinder.ReverseDirection ();
			ghostState = GhostState.FRIGHTENED;
		}
	}

	public void StartChasing () {
		// Again, don't want to change state if inside the house
		if (ghostState == GhostState.SCATTER || ghostState == GhostState.FRIGHTENED) {
			if (ghostState == GhostState.SCATTER) {
				pathFinder.ReverseDirection ();
			}
			ghostState = GhostState.CHASE;
		}
	}

	public void Scatter () {
		if (ghostState == GhostState.CHASE || ghostState == GhostState.FRIGHTENED) {
			if (ghostState == GhostState.CHASE) {
				pathFinder.ReverseDirection ();
			}
			ghostState = GhostState.SCATTER;
		}
	}

	public void Die () {
		ghostState = GhostState.DEAD;

		// Deactivate collider to pass through pacman and stuff
		GetComponent<CircleCollider2D>().enabled = false;

		// TODO: stop game for a sec, reactivate collider
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
