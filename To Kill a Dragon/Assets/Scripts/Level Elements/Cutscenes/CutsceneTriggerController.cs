using UnityEngine;
using System.Collections;

public class CutsceneTriggerController : MonoBehaviour {

	public bool Repeatable;

	/**
	 * Use this for initialization
	 * **/
	void Start () {
		}

	void Awake () {
		GetComponent<Renderer> ().enabled = false;

		Ray ray = new Ray (transform.position, Vector3.down);
		RaycastHit hit;
		Physics.Raycast (ray, out hit, Mathf.Infinity, (1 << 13));
		transform.position = hit.point + new Vector3(0,0.3f,0); //The modifier is to ensure that the player hits it
	}
	
	/**
	 * Update is called once per frame
	 * **/
	void Update () {
	
	}

	void OnTriggerEnter(Collider c){
		if (c.CompareTag ("Player")) {
			if (!Repeatable) {
				Destroy (gameObject);
			}
		}
	}
}
