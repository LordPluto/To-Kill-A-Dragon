using UnityEngine;
using System.Collections;

public class WarpStartController : MonoBehaviour {

	private WarpController parentControl;

	// Use this for initialization
	void Start () {
		parentControl = GetComponentInParent<WarpController> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter (Collider c) {
		parentControl.triggerWarp (c);
	}
}
