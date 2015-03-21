using UnityEngine;
using System.Collections;

public class PacmanColliderController : MonoBehaviour {

	private PacmanController pacman;

	void Start () {
		pacman = GetComponent<PacmanController>();
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.tag == "Ghost") {
			GhostStateManager ghostStateManager = collision.gameObject.GetComponent<GhostStateManager>();
			ReactToGhost (ghostStateManager);
		}
	
	}

	private void ReactToGhost (GhostStateManager ghostStateManager) {
		switch (ghostStateManager.GetGhostState()) {
			case GhostState.FRIGHTENED:
				ghostStateManager.Die ();
				break;
			default:
				pacman.Die ();
				break;
		}
	}
}

