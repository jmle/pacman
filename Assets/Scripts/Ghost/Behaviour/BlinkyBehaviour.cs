using UnityEngine;
using System.Collections;

public class BlinkyBehaviour : AbstractBehaviour {

	private static Vector2 targetScatter = new Vector2 (23, 27);
	private static Vector2 STARTING_POSITION = new Vector2 (13.5f, 19f);

	protected override Vector2 GetTargetForChase () {
		return VectorUtils.Truncate (player.rigidbody2D.position);
	}

	protected override Vector2 GetTargetForScatter () {
		return targetScatter;
	}

	protected override Vector2 GetStartingPosition () {
		return STARTING_POSITION;
	}

}
