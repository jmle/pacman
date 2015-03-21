using UnityEngine;
using System.Collections;

public class PacmanColliderController : MonoBehaviour {

	private PacmanController pacman;

	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.tag == "Ghost") {
			GhostController ghost = collision.gameObject.GetComponent<GhostController>();
			ReactToGhost (ghost);
		}
	
	}

	private void ReactToGhost (GhostController ghost) {
		switch (ghost.GetGhostState()) {
			case GhostState.FRIGHTENED:
				ghost.Die ();
				break;
			default:
				pacman.Die ();
				break;
		}
	}
}

