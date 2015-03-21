using UnityEngine;
using System.Collections;

// TODO: Change name to something meaningful
public class GhostScriptedMovement : MonoBehaviour {
	private static Vector2 middleHousePosition = new Vector2 (13.5f, 16);
	private static Vector2 outOfTheHousePosition = new Vector2 (13.5f, 19);

	public Vector2 bouncePosition1;
	public Vector2 bouncePosition2;

	private GhostController ghostController;

	// Navigation control
	private bool bouncingUp;
	private bool hasArrivedToCenter;

	// Use this for initialization
	void Start () {
		ghostController = GetComponent<GhostController>();

		bouncingUp = true;
		hasArrivedToCenter = false;
	}

	void OnAwake () {
		hasArrivedToCenter = false;
	}

	// Update is called once per frame
	void FixedUpdate () {
		switch (ghostController.GetGhostState ()) {
		case GhostState.HOME:
			Bounce ();
			break;
		case GhostState.EXIT:
			GetOut ();
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
	// TODO: remove, should use pathfinding inside the house too
	private void GetOut () {
		if (!hasArrivedToCenter) {
			if (HasArrived (middleHousePosition)) {
				hasArrivedToCenter = true;
			} else {
				GoTo (middleHousePosition);
			}
		} else {
			if (HasArrived (outOfTheHousePosition)) {
				ghostController.SetGhostState (GhostState.CHASE);
			} else {
				GoTo (outOfTheHousePosition);
			}
		}
	}

	private void GoTo (Vector2 position) {
		ghostController.SetDirection ((position - rigidbody2D.position).normalized);
	}

	private bool HasArrived (Vector2 position) {
		return Vector2.Distance (rigidbody2D.position, position) < 0.1;
	}
}
