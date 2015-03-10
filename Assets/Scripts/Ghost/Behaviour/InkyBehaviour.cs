using UnityEngine;
using System.Collections;

// Calculates the target tile for the ghost.
public class InkyBehaviour : AbstractBehaviour {

	private static Vector2 targetScatter = new Vector2 (23, 27);

	public GameObject blinky;

	protected override Vector2 GetTargetForChase () {
		Vector2 playerOffset = player.rigidbody2D.position + playerController.GetDirection () * 2;
		Vector2 targetPosition = (playerOffset * 2) - blinky.rigidbody2D.position;

		return VectorUtils.Truncate (targetPosition);
	}

	protected override Vector2 GetTargetForScatter () {
		return targetScatter;
	}

}
