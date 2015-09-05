using UnityEngine;
using System.Collections;

public class AdviController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		//Advi's Position
		transform.position = Input.mousePosition;
	}
}
