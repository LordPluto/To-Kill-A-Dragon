using UnityEngine;
using System.Collections;

public class WarpController : MonoBehaviour {

	public WarpDestinationController destination;

	// Use this for initialization
	void Start () {
				destination = GetComponentInChildren<WarpDestinationController> ();
		}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void triggerWarp(Collider c){
				if (c.CompareTag ("Player")) {
						c.GetComponent<PlayerMasterController> ().JumpPosition (destination.getPosition());
				}
		}
}
