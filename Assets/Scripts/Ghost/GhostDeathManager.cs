using UnityEngine;
using System.Collections;

public class GhostDeathManager : MonoBehaviour {

	// Ugly: we need this for the target tile
	private GhostPathFinder pathFinder;

	// Use this for initialization
	void Start () {
		pathFinder = GetComponent<GhostPathFinder>();
	}
	
	// Update is called once per frame
	void Update () {
		GhostState ghostState = pathFinder.GetGhostController().GetGhostState();
		if (ghostState == GhostState.DEAD) {
			if (HasArrived(pathFinder.GetTargetTile())) {
				pathFinder.GetGhostController().SetGhostState(GhostState.HOME);
			}
		}
	}

	// TODO: duplicate from GhostScriptedMovements
	private bool HasArrived (Vector2 position) {
		return Vector2.Distance (rigidbody2D.position, position) < 0.1;
	}
}
