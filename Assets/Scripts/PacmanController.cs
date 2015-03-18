using UnityEngine;
using System.Collections;

public class PacmanController : MonoBehaviour {

	private Animator animator;

	public GhostManager ghostManager;
	public float speed;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		UpdateAnimator ();
		UpdatePosition ();
		Flip ();
	}

	void UpdateAnimator () {
		animator.SetFloat ("speed", rigidbody2D.velocity.magnitude);
	}

	void UpdatePosition () {
		float movX = Input.GetAxisRaw ("Horizontal") * speed;
		float movY = Input.GetAxisRaw ("Vertical") * speed;

		rigidbody2D.velocity = new Vector2 (movX, movY);
	}

	void Flip () {
		float movX = Input.GetAxisRaw ("Horizontal");
		float movY = Input.GetAxisRaw ("Vertical");

		if (movX != 0 || movY != 0) {
			if (movX != 0) {
				transform.localScale = new Vector2 ((movX < 0 ? -1 : 1), 1);
				transform.rotation = Quaternion.identity;
			} else {
				transform.localScale = new Vector2 (1, 1);
				transform.rotation = Quaternion.AngleAxis ((movY < 0 ? -90 : 90), new Vector3 (0, 0, 1));
			}
		}
	}

	public Vector2 GetDirection () {
		Vector2 direction = new Vector2 ();

		if (transform.localScale.x == -1)
			direction = VectorUtils.left;
		else if (Mathf.Approximately (transform.rotation.eulerAngles.z, 270))
			direction = VectorUtils.down;
		else if (Mathf.Approximately (transform.rotation.eulerAngles.z, 90))
			direction = VectorUtils.up;
		else
			direction = VectorUtils.right;

		return direction;
	}

	public void EatDot () {
		ghostManager.PacmanAteDot ();
	}

	public void EatEnergizer () {

	}
}
