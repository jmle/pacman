using UnityEngine;
using System.Collections;

public class PinkyBehaviour : AbstractBehaviour {
	
	private static Vector2 targetScatter = new Vector2 (23, 3);

	protected override Vector2 GetTargetForChase () {
		Vector2 targetPosition = pacman.rigidbody2D.position + pacmanController.GetDirection () * 4;
	
		return VectorUtils.Truncate (targetPosition);
	}

	protected override Vector2 GetTargetForScatter () {
		return targetScatter;
	}

}
