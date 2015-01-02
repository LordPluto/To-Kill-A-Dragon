using UnityEngine;
using System.Collections;

public class DestinationController : MonoBehaviour {

	public double Ground;

	// Use this for initialization
	void Start () {
		transform.position = new Vector3 (transform.position.x, (float)Ground, transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
