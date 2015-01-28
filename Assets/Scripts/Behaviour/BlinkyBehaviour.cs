﻿using UnityEngine;
using System.Collections;

public class BlinkyBehaviour : Behaviour {

	private static Vector2 targetScatter = new Vector2 (23, 27);

	protected override Vector2 GetTargetForChase () {
		return VectorUtils.Truncate (player.rigidbody2D.position);
	}

	protected override Vector2 GetTargetForScatter () {
		return targetScatter;
	}
}
