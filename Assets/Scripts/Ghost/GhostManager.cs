using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GhostManager : MonoBehaviour {

	// All the ghosts
	public GhostController[] ghosts = new GhostController[4];

	// Use this for initialization
	void Start () {
//		ghosts = new List<GhostController> ();
//		foreach (Transform child in transform) {
//			ghosts.Add (child.gameObject.GetComponent<GhostController>());
//		}
	}
	
	// Update is called once per frame
	void Update () {

	}
}
