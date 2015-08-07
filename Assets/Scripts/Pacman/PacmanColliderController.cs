using UnityEngine;
using System.Collections;

/// <summary>
/// Controls collisions between pacman and the ghosts.
/// </summary>
public class PacmanColliderController : MonoBehaviour {

	private PacmanController pacman;

	public GameController gameController;

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
		if (ghostStateManager.GetGhostState() == GhostState.FRIGHTENED) {
			ghostStateManager.Die ();
//			ghostStateManager.gameObject.GetComponent<GhostController>().Die()...
		} else {
			gameController.Die ();
		}
	}
}
