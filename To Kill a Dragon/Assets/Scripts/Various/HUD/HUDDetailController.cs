using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUDDetailController : MonoBehaviour {

	private RawImage image;

	// Use this for initialization
	void Start () {
		image = GetComponent<RawImage> ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	/**
	 * Hides the HUD element
	 * **/
	public void Hide () {
		image.enabled = false;
	}
	
	/**
	 * Shows the HUD element
	 * **/
	public void Show () {
		image.enabled = true;
	}
}
