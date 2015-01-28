using UnityEngine;
using System.Collections;

public class PinkyBehaviour : Behaviour {
	
	private static Vector2 targetScatter = new Vector2 (23, 3);
	
	protected override Vector2 GetTargetForChase () {
		Vector2 targetPosition = player.rigidbody2D.position + playerController.GetDirection () * 4;
	
		return VectorUtils.Truncate (targetPosition);
	}
	
	protected override Vector2 GetTargetForScatter () {
		return targetScatter;
	}
}
