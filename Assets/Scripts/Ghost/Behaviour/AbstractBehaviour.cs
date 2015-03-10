using UnityEngine;
using System.Collections;

public abstract class AbstractBehaviour : MonoBehaviour {
	private static Vector2 startingPosition = new Vector2 (13.5f, 19f);

	public GameObject player;

	protected PlayerController playerController;
	protected GhostPathFinder pathFinder;

	void Start () {
		pathFinder = GetComponent<GhostPathFinder>();
		playerController = player.GetComponent<PlayerController> ();
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
			target = startingPosition;
			break;

		default:
			break;
		}

		return target;
	}

	// TODO: This will make it change constantly. Maybe add timer?
	private Vector2 GetTargetForFrightened () {
		return VectorUtils.GetRandomVector ();
	}

	protected abstract Vector2 GetTargetForChase ();
	protected abstract Vector2 GetTargetForScatter ();
}
