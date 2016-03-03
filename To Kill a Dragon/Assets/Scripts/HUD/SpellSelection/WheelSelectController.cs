using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WheelSelectController : MonoBehaviour {

	private Canvas parentCanvas;

	// Use this for initialization
	void Start () {

	}

	void Awake () {
		parentCanvas = GetComponentInParent<Canvas> ();
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.LeftShift)) {
			ToggleCanvas ();
		}
	}

	void ToggleCanvas () {
		parentCanvas.enabled = !parentCanvas.enabled;
	}
}
