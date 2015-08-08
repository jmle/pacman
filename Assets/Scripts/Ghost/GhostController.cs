using UnityEngine;
using System.Collections;

/// <summary>
/// Central class for controlling a ghost. Orchestrates the switch
/// between pathfinding and scripted ways of navigation. Tells the
/// Motor the direction to go.
/// </summary>
public class GhostController : MonoBehaviour {
	private Vector2 direction;
	private bool move;

	private Motor motor;
	private GhostPathFinder ghostPathFinder;
	private GhostScriptedMovement ghostScriptedMovement;
	private GhostStateManager ghostStateManager;
	private Animator animator;

	public GameController gameController;

	// Use this for initialization
	void Start () {
		motor = GetComponent<Motor>();
		ghostPathFinder = GetComponent<GhostPathFinder>();
		ghostScriptedMovement = GetComponent<GhostScriptedMovement>();
		ghostStateManager = GetComponent<GhostStateManager>();
		animator = GetComponent<Animator>();

		direction = Vector2.zero;
		move = true;

		// Choose Pathfinder now, otherwise both will modify
		// the direction at the beginning.
		ChoosePathFinder ();
	}

	// Update is called once per frame
	void Update () {
		ChoosePathFinder ();
		UpdateAnimator ();

		if (move) {
			motor.SetDirection (direction);
		} else {
			motor.SetDirection (Vector2.zero);
		}
	}

	public void UpdateAnimator () {
		// TODO: improve this code
		if (ghostStateManager.GetGhostState().Equals(GhostState.FRIGHTENED)) {
			animator.SetBool ("isFrightened", true);
			animator.SetBool ("isDead", false);
		} else if (ghostStateManager.GetGhostState().Equals(GhostState.DEAD)) {
			animator.SetBool ("isDead", true);
			animator.SetBool ("isFrightened", false);
		} else {
			animator.SetBool ("isFrightened", false);
			animator.SetBool ("isDead", false);
		}
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
		case GhostState.ENTER:
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

	public void Freeze () {
		move = false;
	}

	public void Unfreeze () {
		move = true;
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
