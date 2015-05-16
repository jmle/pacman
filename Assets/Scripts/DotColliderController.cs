using UnityEngine;
using System.Collections;

/// <summary>
/// Handles collisions for dots and energizers
/// </summary>
public class DotColliderController : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.tag == "Player") {
			// Send message to Player object...
			if (gameObject.tag == "Dot") {
				collision.collider.gameObject.SendMessage ("EatDot");
			} else {
				collision.collider.gameObject.SendMessage ("EatEnergizer");
			}

			// ... and destroy this dot
			Destroy(this.gameObject);
		}
	}
}
