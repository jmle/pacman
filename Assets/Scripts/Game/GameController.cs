using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public int livesLeft;

	public PacmanController pacman;
	public GhostManager ghosts;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}

	/// <summary>
	/// Pacman died
	/// </summary>
	public void Die () {
		ghosts.FreezeGhosts ();
		pacman.Freeze ();
	}
}
