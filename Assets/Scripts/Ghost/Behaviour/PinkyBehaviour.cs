using UnityEngine;
using System.Collections;

public class PinkyBehaviour : AbstractBehaviour {
	
	private static Vector2 targetScatter = new Vector2 (23, 3);
	private static Vector2 STARTING_POSITION = new Vector2 (13.5f, 19f);
	
	protected override Vector2 GetTargetForChase () {
		Vector2 targetPosition = player.rigidbody2D.position + pacmanController.GetDirection () * 4;
	
		return VectorUtils.Truncate (targetPosition);
	}

	protected override Vector2 GetTargetForScatter () {
		return targetScatter;
	}

	protected override Vector2 GetStartingPosition () {
		return STARTING_POSITION;
	}

}
