using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public int livesLeft;
	public float timeToRestart;

	public PacmanController pacman;
	public GhostManager ghosts;

	private bool restartLevel;
	private float restartCounter;

	// Use this for initialization
	void Start () {
		restartCounter = timeToRestart;
	}
	
	// Update is called once per frame
	void Update () {
		if (restartLevel) {
			if (restartCounter <= 0) {
				restartLevel = false;
				restartCounter = timeToRestart;

				RestartLevel ();
			} else {
				restartCounter -= Time.deltaTime;
			}
		}
	}

	private void RestartLevel () {
		livesLeft--;

		if (livesLeft == 0) {
			// TODO: game over
		} else {
//			Application.LoadLevel (0);
			Debug.Log ("Restarting level...");
		}
	}

	/// <summary>
	/// Pacman died
	/// </summary>
	public void Die () {
		ghosts.FreezeGhosts ();
		pacman.Freeze ();

		restartLevel = true;
	}
}
