using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DialogueBackgroundController : MonoBehaviour {

	private RawImage image;

	// Use this for initialization
	void Awake () {
		image = GetComponent<RawImage> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/**
	 * Shows the box background.
	 * **/
	public void Activate(){
				image.enabled = true;
		}

	/**
	 * Hides the box background.
	 * **/
	public void Deactivate(){
				image.enabled = false;
		}
}
