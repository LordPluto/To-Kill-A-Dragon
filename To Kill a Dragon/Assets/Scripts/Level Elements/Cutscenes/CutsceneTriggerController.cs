using UnityEngine;
using System.Collections;

public class CutsceneTriggerController : MonoBehaviour {

	public double Ground;

	private GameController _controller;

	public bool PlayerInvolved;

	public bool NPCInvolved;
	public string NPCName;

	public Transform[] PlayerPathPoints;
	public Transform[] NPCPathPoints;

	public bool Repeatable;

	/**
	 * Use this for initialization
	 * **/
	void Start () {
				_controller = GameObject.Find ("_GameController").GetComponent<GameController> ();
		}

	void Awake () {
				GetComponent<Renderer>().enabled = false;
				transform.position = new Vector3 (transform.position.x, (float)Ground, transform.position.z);
		}
	
	/**
	 * Update is called once per frame
	 * **/
	void Update () {
	
	}

	void OnTriggerEnter(Collider c){
		if (c.CompareTag ("Player")) {
			_controller.EnterCutscene (PlayerInvolved, NPCInvolved, PlayerPathPoints, NPCPathPoints, NPCName);

			if (!Repeatable) {
				Destroy (gameObject);
			}
		}
	}
}
