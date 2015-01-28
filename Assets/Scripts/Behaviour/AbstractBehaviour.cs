using UnityEngine;
using System.Collections;

public abstract class AbstractBehaviour : MonoBehaviour {
	private static Vector2 startingPosition = new Vector2 (13.5f, 19f);
	
	public GameObject player;

	protected PlayerController playerController;
	protected GhostPathFinder pathFinder;
	public GhostState ghostState;

	void Start () {
		pathFinder = GetComponent<GhostPathFinder>();
		playerController = player.GetComponent<PlayerController> ();
		ghostState = GhostState.CHASE;
	}

	void Update () {
		CalculateTargetPosition ();
		pathFinder.SetTargetTile (CalculateTargetPosition ());
	}

	// TODO: multicast message to all *Behaviour objects with the new behaviour mode
	public void SetGhostBehaviourMode (GhostState ghostBehaviourMode) {
		this.ghostState = ghostBehaviourMode;
	}
	
	private Vector2 CalculateTargetPosition () {
		Vector2 target = Vector2.zero;

		switch (ghostState) {
		case GhostState.CHASE:
			target = GetTargetForChase ();
			break;

		case GhostState.SCATTER:
			target = GetTargetForScatter ();
			break;

		case GhostState.FRIGHTENED:
			break;

		default:
			break;
		}

		return target;
	}

	protected abstract Vector2 GetTargetForChase ();
	protected abstract Vector2 GetTargetForScatter ();
}