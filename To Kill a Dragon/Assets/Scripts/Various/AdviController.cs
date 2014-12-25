using UnityEngine;
using System.Collections;

public class AdviController : MonoBehaviour {

	public float depth = 2;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		//Advi's Position
		Vector3 NewPosition = Camera.main.ScreenToWorldPoint(new Vector3 (Input.mousePosition.x, Input.mousePosition.y, depth));
		transform.position = NewPosition;
	}
}
