using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Selects the next direction to go to
public class GhostPathFinder : MonoBehaviour {

	private GhostController ghostController;
	private Vector2 target;
	private Vector2 truncatedPosition;
	private Vector2 lastPositionDecided;
	private Vector2 lastDirectionDecided;
	private Rigidbody2D ghost;
	private Tile[,] tileMap;
	private bool kickstarting;

	public GameObject tileMapCreator;

	// Use this for initialization
	void Start () {
		kickstarting = true;
		ghost = gameObject.rigidbody2D;
		ghostController = GetComponent<GhostController>();
		tileMap = tileMapCreator.GetComponent<TileMapFactory>().GetTileMap();
	}

	void OnEnable () {
		kickstarting = true;
	}
	
	// Update is called once per frame
	void Update () {
		truncatedPosition = VectorUtils.Truncate (ghost.position);

		if (ShouldChangeDirection ()) {
			ghostController.SetDirection (DecideNextDirection ());
			lastPositionDecided = truncatedPosition;
		}
	}

	private bool ShouldChangeDirection () {
		if (kickstarting) {
			kickstarting = false;
			return true;
		} else {
			bool isCloseToCenterOfTheTile = Vector2.Distance (ghost.position, truncatedPosition) < 0.15;
			bool hasChangedTileSinceLastDecided = !lastPositionDecided.Equals (truncatedPosition);

			// Debug.Log (ghost.position + "/" + truncatedPosition + "center:" + isCloseToCenterOfTheTile + " changed:" + hasChangedTileSinceLastDecided);
			return (isCloseToCenterOfTheTile && hasChangedTileSinceLastDecided);
		}
	}

	private Vector2 DecideNextDirection () {
		// Get the possible directions out of the current tile
		Tile currentTile = tileMap[(int) truncatedPosition.x, (int) truncatedPosition.y];
		IList<Vector2> directions = currentTile.GetDirections ();

		// Calculate which of the possible directions is the closest to the target
		Vector2 closestToTarget = new Vector2 (1000, 1000);
		foreach (Vector2 dir in directions) {
			// The opposite direction of the current one is discarded
			// TODO: ghosts should be able to change direction when changing behaviour
			// TODO: a DEAD ghost should be able to get into the house
			if (!VectorUtils.AreOpposite (ghostController.GetDirection (), dir)) {
				if (Vector2.Distance (ghost.position + dir, target) <
				    Vector2.Distance (ghost.position + closestToTarget, target)) {
					closestToTarget = dir;
				}
			}
		}

		lastDirectionDecided = closestToTarget;

		return closestToTarget;
	}

	public void SetTargetTile (Vector2 tile) {
		target = tile;
	}

	public Vector2 GetTargetTile () {
		return target;
	}

	public GhostController GetGhostController () {
		return ghostController;
	}
}
