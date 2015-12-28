using UnityEngine;
using System.Collections;

public class DestinationController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Ray ray = new Ray (transform.position, Vector3.down);
		RaycastHit hit;

		Physics.Raycast (ray, out hit, Mathf.Infinity, (1 << 13));
		transform.position = hit.point;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
