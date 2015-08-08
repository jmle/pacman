using UnityEngine;
using System.Collections;

/// <summary>
/// Handles motion.
/// </summary>
public class Motor : MonoBehaviour {

	private Rigidbody2D ghost;
	private Vector2 direction;
	private GhostStateManager stateManager;

	public float speed;

	// Use this for initialization
	void Start () {
		ghost = gameObject.rigidbody2D;
		stateManager = GetComponent<GhostStateManager>();
	}

	// Update is called once per frame
	void FixedUpdate () {
		ghost.velocity = direction * speed;

		if (stateManager.GetGhostState() == GhostState.DEAD) {
			ghost.velocity *= 2;
		}
	}

	public void SetDirection (Vector2 direction) {
		this.direction = direction;
	}

	public void GoTo (Vector2 position) {
		direction = (position - ghost.position).normalized;
	}

}
