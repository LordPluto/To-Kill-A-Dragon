using UnityEngine;
using System.Collections;

public class CutsceneManager : MonoBehaviour {

	public CutsceneEvent[] CutsceneEvents;
	private int eventNum;

	private GameController _controller;

	public bool Repeatable;

	// Use this for initialization
	void Start () {
		_controller = GameObject.Find ("_GameController").GetComponent<GameController> ();
	}
	
	void OnTriggerEnter(Collider c){
		if (c.CompareTag ("Player")) {
			_controller.EnterCutscene ();

			if (!Repeatable) {
				Destroy (gameObject);
			}
		}
	}
}
