using UnityEngine;
using System.Collections;

public class CutsceneTriggerController : MonoBehaviour {

	private GameController _controller;

	public bool PlayerInvolved;
	public bool NPCInvolved;

	public Transform[] pathPoints;

	public bool Repeatable;

	// Use this for initialization
	void Start () {
				renderer.enabled = false;
		}

	void Awake () {
				_controller = GameObject.Find ("_GameController").GetComponent<GameController> ();
		}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider c){
				if (c.CompareTag ("Player")) {
						_controller.EnterCutscene (PlayerInvolved, NPCInvolved, pathPoints);

						if (!Repeatable) {
								Destroy (gameObject);
						}
				}
		}
}
