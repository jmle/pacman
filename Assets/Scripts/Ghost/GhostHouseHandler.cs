using UnityEngine;
using System.Collections;

/// <summary>
/// Handles the ghost when entering and exiting the house.
/// </summary>
public class GhostHouseHandler : MonoBehaviour {

	private GhostStateManager ghostStateManager;

	// Use this for initialization
	void Start () {
		ghostStateManager = GetComponent<GhostStateManager>();
	}
	
	// Update is called once per frame
	void Update () {
		switch (ghostStateManager.GetGhostState()) {
		case GhostState.EXIT:
			HandleHouseExit ();
			break;
		case GhostState.DEAD:
			HandleHouseEnter ();
			break;
		case GhostState.ENTER:
			HandleHouseInside ();
			break;
		default:
			break;
		}
	}

	private void HandleHouseExit () {
		if (HasArrived (GlobalPositions.HOUSE_DOOR)) {
			ghostStateManager.SetGhostStateToCurrentGlobalState ();
		}
	}

	private void HandleHouseEnter () {
		if (HasArrived (GlobalPositions.HOUSE_DOOR)) {
			ghostStateManager.SetGhostState (GhostState.ENTER);
		}
	}

	private void HandleHouseInside () {
		if (HasArrived (GlobalPositions.HOUSE_MIDDLE)) {
			// If the ghost was dead, he goes out immediately (not actually true)
			// TODO: This depends on more things, but for now it's fine
			ghostStateManager.SetGhostState (GhostState.EXIT);
		}
	}

	private bool HasArrived (Vector2 position) {
		return Vector2.Distance (rigidbody2D.position, position) < 0.15;
	}
}
