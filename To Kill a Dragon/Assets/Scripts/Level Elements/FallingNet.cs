using UnityEngine;
using System.Collections;

public class FallingNet : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter(Collider c) {
				if (c.CompareTag ("Player")) {
						c.GetComponent<PlayerMasterController> ().PitSpawn ();
				}
		}
}
