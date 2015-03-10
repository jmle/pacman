using UnityEngine;
using System.Collections;

public class GhostController : MonoBehaviour {

	public GhostState ghostState;

	private Vector2 direction;

	private Motor motor;
	private GhostPathFinder ghostPathFinder;
	private GhostScriptedMovement ghostScriptedMovement;

	// Use this for initialization
	void Start () {
		motor = GetComponent<Motor>();
		ghostPathFinder = GetComponent<GhostPathFinder>();
		ghostScriptedMovement = GetComponent<GhostScriptedMovement>();

		direction = Vector2.zero;
	}

	// Update is called once per frame
	void Update () {
		ChoosePathFinder ();

		motor.SetDirection (direction);
	}

	public void ChoosePathFinder () {
		switch (ghostState) {
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

	public GhostState GetGhostState () {
		return this.ghostState;
	}

	public void SetGhostState (GhostState ghostState) {
		this.ghostState = ghostState;
	}
}
