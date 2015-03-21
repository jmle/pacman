using UnityEngine;
using System.Collections;

public class GhostController : MonoBehaviour {
	private Vector2 direction;

	private Motor motor;
	private GhostPathFinder ghostPathFinder;
	private GhostScriptedMovement ghostScriptedMovement;
	private GhostStateManager ghostStateManager;

	// Use this for initialization
	void Start () {
		motor = GetComponent<Motor>();
		ghostPathFinder = GetComponent<GhostPathFinder>();
		ghostScriptedMovement = GetComponent<GhostScriptedMovement>();
		ghostStateManager = GetComponent<GhostStateManager>();	

		direction = Vector2.zero;

		// Choose Pathfinder now, otherwise both will modify
		// the direction at the beginning.
		ChoosePathFinder ();
	}

	// Update is called once per frame
	void Update () {
		ChoosePathFinder ();

		motor.SetDirection (direction);
	}

	public void ChoosePathFinder () {
		switch (ghostStateManager.GetGhostState()) {
		case GhostState.CHASE:
		case GhostState.DEAD:
		case GhostState.SCATTER:
		case GhostState.FRIGHTENED:
			EnablePathFinding ();
			break;

		case GhostState.HOME:
		case GhostState.EXIT:
			EnableScriptedMovement ();
			break;

		default:
			break;
		}
	}

	public void EnablePathFinding () {
		ghostScriptedMovement.enabled = false;
		ghostPathFinder.enabled = true;
	}

	public void EnableScriptedMovement () {
		ghostScriptedMovement.enabled = true;
		ghostPathFinder.enabled = false;
	}

	public void SetDirection (Vector2 direction) {
		this.direction = direction;
	}

	public Vector2 GetDirection () {
		return direction;
	}

	public GhostStateManager GetGhostStateManager () {
		return ghostStateManager;
	}

}
