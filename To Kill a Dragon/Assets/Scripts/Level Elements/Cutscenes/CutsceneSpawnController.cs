using UnityEngine;
using System.Collections;

public class CutsceneSpawnController : CutsceneEvent {

	public GameObject SpawningObject;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	override public void Execute() {
		SpawningObject.SetActive (true);
	}
}
