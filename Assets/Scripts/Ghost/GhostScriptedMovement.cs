using UnityEngine;
using System.Collections;

/// <summary>
/// Pre-scripted movements for the ghosts.
/// Controls the routines of the ghosts when inside the house,
/// but doesn't change their state (that's the job of the GhostHouseHandler)
/// </summary>
public class GhostScriptedMovement : MonoBehaviour {
	public Vector2 startingPosition;
	public Vector2 bouncePosition1;
	public Vector2 bouncePosition2;

	private GhostController ghostController;
	private GhostStateManager ghostStateManager;

	// Navigation control
	private bool bouncingUp;
	private bool hasArrivedToCenter;

	// Use this for initialization
	void Start () {
		ghostController = GetComponent<GhostController>();
		ghostStateManager = ghostController.GetGhostStateManager ();

		bouncingUp = true;
		hasArrivedToCenter = false;
	}

	void OnAwake () {
		hasArrivedToCenter = false;
	}

	// Update is called once per frame
	void FixedUpdate () {
		switch (ghostStateManager.GetGhostState ()) {
		case GhostState.HOME:
			Bounce ();
			break;
		case GhostState.EXIT:
			GetOut ();
			break;
		case GhostState.ENTER:
			GetIn ();
			break;
		default:
			break;
		}
	}

	// Bounce at home!
	private void Bounce () {
		if (bouncingUp) {
			if (HasArrived (bouncePosition1)) {
				bouncingUp = false;
			} else {
				GoTo (bouncePosition1);
			}
		} else {
			if (HasArrived (bouncePosition2)) {
				bouncingUp = true;
			} else {
				GoTo (bouncePosition2);
			}
		}
	}

	// Get out of the house
	private void GetOut () {
		if (!hasArrivedToCenter) {
			if (HasArrived (GlobalPositions.HOUSE_MIDDLE)) {
				hasArrivedToCenter = true;
			} else {
				GoTo (GlobalPositions.HOUSE_MIDDLE);
			}
		} else {
			GoTo (GlobalPositions.HOUSE_DOOR);
		}
	}

	private void GetIn () {
		if (!hasArrivedToCenter) {
			if (HasArrived (GlobalPositions.HOUSE_MIDDLE)) {
				hasArrivedToCenter = true;
			} else {
				GoTo (GlobalPositions.HOUSE_MIDDLE);
			}
		} else {
//			GoTo (startingPosition);
		}
	}

	private void GoTo (Vector2 position) {
		ghostController.SetDirection ((position - rigidbody2D.position).normalized);
	}

	private bool HasArrived (Vector2 position) {
		return Vector2.Distance (rigidbody2D.position, position) < 0.15;
	}
}
