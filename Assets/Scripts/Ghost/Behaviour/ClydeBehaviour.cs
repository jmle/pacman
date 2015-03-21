using UnityEngine;
using System.Collections;

public class ClydeBehaviour : AbstractBehaviour {

	private static Vector2 targetScatter = new Vector2 (4, 3);

	protected override Vector2 GetTargetForChase () {
		Vector2 targetPosition;

		if (Vector2.Distance (rigidbody2D.position, pacman.rigidbody2D.position) > 8) {
			targetPosition = pacman.rigidbody2D.position;
		} else {
			targetPosition = targetScatter;
		}
		
		return VectorUtils.Truncate (targetPosition);
	}
	
	protected override Vector2 GetTargetForScatter () {
		return targetScatter;
	}

}
