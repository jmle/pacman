﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Selects the next direction to go to
public class GhostPathFinder : MonoBehaviour {

	private GhostController ghostController;
	private Vector2 target;
	private Vector2 truncatedPosition;
	private Vector2 lastPositionDecided;
	private Rigidbody2D ghost;
	private Tile[,] tileMap;
	private bool kickstarting;

	public GameObject tileMapCreator;

	// Use this for initialization
	void Start () {
		truncatedPosition = new Vector2 ();
		lastPositionDecided = new Vector2 ();
		ghost = gameObject.rigidbody2D;
		ghostController = GetComponent<GhostController>();
		TileMapFactory factory = tileMapCreator.GetComponent<TileMapFactory>();
		tileMap = factory.GetTileMap ();
		kickstarting = true;
	}

	void OnEnable () {
		kickstarting = true;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		truncatedPosition = VectorUtils.Truncate (ghost.position);

		if (ShouldChangeDirection ()) {
			ghostController.SetDirection (DecideNextDirection ());
			lastPositionDecided = truncatedPosition;
		}

		// Debug.DrawLine (rigidbody2D.position, VectorUtils.Truncate (target), Color.green);
	}

	private bool ShouldChangeDirection () {
		if (kickstarting) {
			kickstarting = false;
			return true;
		} else {
			bool isCloseToCenterOfTheTile = Vector2.Distance (ghost.position, truncatedPosition) < 0.2;
			bool hasChangedTileSinceLastDecided = !lastPositionDecided.Equals (truncatedPosition);

			return (isCloseToCenterOfTheTile && hasChangedTileSinceLastDecided);
		}
	}

	private Vector2 DecideNextDirection () {
		Tile currentTile = tileMap[(int) truncatedPosition.x, (int) truncatedPosition.y];
		IList<Vector2> directions = currentTile.GetDirections ();

		Vector2 closestToTarget = new Vector2 (1000, 1000);
		foreach (Vector2 dir in directions) {
			if (!VectorUtils.AreOpposite (ghostController.GetDirection (), dir)) {
				if (Vector2.Distance (ghost.position + dir, target) <
				    Vector2.Distance (ghost.position + closestToTarget, target)) {
					closestToTarget = dir;
				}
			}
		}

		return closestToTarget;
	}

	public void SetTargetTile (Vector2 tile) {
		target = tile;
	}

	public GhostController GetGhostController () {
		return ghostController;
	}
}