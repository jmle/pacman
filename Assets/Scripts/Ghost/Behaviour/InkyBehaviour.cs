using UnityEngine;
using System.Collections;

// Calculates the target tile for the ghost.
public class InkyBehaviour : AbstractBehaviour {

	private static Vector2 targetScatter = new Vector2 (23, 27);
	private static Vector2 STARTING_POSITION = new Vector2 (13.5f, 19f);

	public GameObject blinky;

	protected override Vector2 GetTargetForChase () {
		Vector2 playerOffset = player.rigidbody2D.position + pacmanController.GetDirection () * 2;
		Vector2 targetPosition = (playerOffset * 2) - blinky.rigidbody2D.position;

		return VectorUtils.Truncate (targetPosition);
	}

	protected override Vector2 GetTargetForScatter () {
		return targetScatter;
	}

	protected override Vector2 GetStartingPosition () {
		return STARTING_POSITION;
	}

}
