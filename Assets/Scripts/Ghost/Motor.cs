using UnityEngine;
using System.Collections;

/// <summary>
/// Handles motion.
/// </summary>
public class Motor : MonoBehaviour {

	private Rigidbody2D ghost;
	private Vector2 direction;

	public float speed;

	// Use this for initialization
	void Start () {
		ghost = gameObject.rigidbody2D;
	}

	// Update is called once per frame
	void FixedUpdate () {
		ghost.velocity = direction * speed;
	}

	public void SetDirection (Vector2 direction) {
		this.direction = direction;
	}

	public void GoTo (Vector2 position) {
		direction = (position - ghost.position).normalized;
	}

}
