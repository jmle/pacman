using UnityEngine;
using System.Collections;

public abstract class AbstractBehaviour : MonoBehaviour {

	private static Vector2 HOME_DOOR = new Vector2 (13.5f, 19);

	public GameObject pacman;

	protected PacmanController pacmanController;
	protected GhostPathFinder pathFinder;
	protected GhostStateManager ghostStateManager;

	void Start () {
		pacmanController = pacman.GetComponent<PacmanController> ();
		pathFinder = GetComponent<GhostPathFinder>();
		ghostStateManager = GetComponent<GhostStateManager>();
	}

	void Update () {
		pathFinder.SetTargetTile (CalculateTargetPosition ());
	}

	private Vector2 CalculateTargetPosition () {
		Vector2 target = Vector2.zero;
		GhostState ghostState = ghostStateManager.GetGhostState ();

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

		default:
			break;
		}

		return target;
	}

	private Vector2 GetTargetForFrightened () {
		return VectorUtils.GetRandomVector ();
	}

	protected abstract Vector2 GetTargetForChase ();
	protected abstract Vector2 GetTargetForScatter ();
}
