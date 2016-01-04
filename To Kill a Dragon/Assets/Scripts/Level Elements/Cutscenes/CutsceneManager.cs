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

	void Awake () {
		GetComponent<Renderer> ().enabled = false;
		eventNum = 0;

		Ray ray = new Ray (transform.position, Vector3.down);
		RaycastHit hit;
		Physics.Raycast (ray, out hit, Mathf.Infinity, (1 << 13));
		transform.position = hit.point + new Vector3(0,0.3f,0); //The modifier is to ensure that the player hits it
	}
	
	void OnTriggerEnter(Collider c){
		if (c.CompareTag ("Player") && !_controller.CutsceneActive()) {
			_controller.EnterCutscene ();

			CutsceneEvents [eventNum].Execute (this);
		}
	}

	public void EndEvent () {
		++eventNum;

		if (eventNum >= CutsceneEvents.Length) {
			eventNum = 0;
			_controller.EndCutscene ();

			if (!Repeatable) {
				Destroy (gameObject);
			}
		} else {
			CutsceneEvents [eventNum].Execute (this);
		}
	}
}
