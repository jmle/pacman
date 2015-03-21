using UnityEngine;
using System.Collections;

public abstract class AbstractBehaviour : MonoBehaviour {

	public GameObject player;

	private GhostState ghostState;

	protected PacmanController pacmanController;
	protected GhostPathFinder pathFinder;

	void Start () {
		pathFinder = GetComponent<GhostPathFinder>();
		pacmanController = player.GetComponent<PacmanController> ();
	}

	void Update () {
		pathFinder.SetTargetTile (CalculateTargetPosition ());
	}

	private Vector2 CalculateTargetPosition () {
		Vector2 target = Vector2.zero;
		GhostState ghostState = pathFinder.GetGhostController ().GetGhostState ();

		switch (ghostState) {
		case GhostState.CHASE:
			target = GetTargetForChase ();
			break;

		case GhostState.SCATTER:
			target = GetTargetForScatter ();
			break;

		case GhostState.FRIGHTENED:
			target = GetTargetForFrightened ();
			break;

		case GhostState.DEAD:
			target = GetStartingPosition ();
			break;

		default:
			break;
		}

		return target;
	}

	private void UpdateGhostState () {
		if (ghostState == GhostState.DEAD) {
			// If ghost has arrived to 
		}
	}

	// TODO: This will make it change constantly. Maybe add timer?
	private Vector2 GetTargetForFrightened () {
		return VectorUtils.GetRandomVector ();
	}

	protected abstract Vector2 GetTargetForChase ();
	protected abstract Vector2 GetTargetForScatter ();
	protected abstract Vector2 GetStartingPosition ();
}
