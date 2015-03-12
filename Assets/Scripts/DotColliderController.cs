using UnityEngine;
using System.Collections;

public class DotColliderController : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.tag == "Player") {
			// Send message to Player object and destroy this dot
			collision.collider.gameObject.SendMessage ("EatDot");
			Destroy(this.gameObject);
		}
	}
}
