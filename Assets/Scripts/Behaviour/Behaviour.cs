using UnityEngine;
using System.Collections;

public abstract class Behaviour : MonoBehaviour {
	
	public GameObject player;

	protected GhostPathFinder pathFinder;
	protected GhostState ghostState;
	protected Vector2 target;
	protected PlayerController playerController;
	
	private static Vector2 targetScatter = new Vector2 (23, 27);

	void Start () {
		pathFinder = GetComponent<GhostPathFinder>();
		playerController = player.GetComponent<PlayerController> ();
		target = new Vector2 ();
		ghostState = GhostState.CHASE;
	}

	void Update () {
		CalculateTargetPosition ();
		pathFinder.SetTargetTile (target);
	}

	// TODO: multicast message to all *Behaviour objects with the new behaviour mode
	public void SetGhostBehaviourMode (GhostState ghostBehaviourMode) {
		this.ghostState = ghostBehaviourMode;
	}
	
	private void CalculateTargetPosition () {

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
	}

	protected abstract Vector2 GetTargetForChase ();
	protected abstract Vector2 GetTargetForScatter ();
}