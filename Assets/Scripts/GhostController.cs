using UnityEngine;
using System.Collections;

public class GhostController : MonoBehaviour {

	private Motor motor;
	private Vector2 direction;

	// Use this for initialization
	void Start () {
		motor = GetComponent<Motor>();
		direction = Vector2.zero;
	}

	// Update is called once per frame
	void Update () {
		motor.SetDirection (direction);
	}

	public void SetDirection (Vector2 direction) {
		this.direction = direction;
	}

	public Vector2 GetDirection () {
		return direction;
	}
}
