using UnityEngine;
using System.Collections;

public class BlinkyBehaviour : AbstractBehaviour {

	private static Vector2 targetScatter = new Vector2 (23, 27);

	protected override Vector2 GetTargetForChase () {
		return VectorUtils.Truncate (pacman.rigidbody2D.position);
	}

	protected override Vector2 GetTargetForScatter () {
		return targetScatter;
	}

}
