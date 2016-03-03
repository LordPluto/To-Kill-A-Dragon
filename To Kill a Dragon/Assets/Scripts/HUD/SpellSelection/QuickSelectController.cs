using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class QuickSelectController : MonoBehaviour {

	private Canvas parentCanvas;

	// Use this for initialization
	void Start () {
	
	}

	void Awake () {
		parentCanvas = GetComponentInParent<Canvas> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.LeftShift)) {
			parentCanvas.enabled = true;
		} else if (Input.GetKeyUp (KeyCode.LeftShift)) {
			parentCanvas.enabled = false;
		}
	}
}
