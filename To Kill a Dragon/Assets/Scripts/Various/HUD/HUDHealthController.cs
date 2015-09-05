using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUDHealthController : MonoBehaviour {
	
	private float percent;

	private RawImage image;

	private bool needsUpdate = true;
	
	// Use this for initialization
	void Start () {
		image = GetComponent<RawImage> ();
		percent = 100;
	}
	
	// Update is called once per frame
	void Update () {
		if (needsUpdate) {
			needsUpdate = false;
						setPercent (percent);
				}
	}

	/**
	 * Sets the percentage
	 * **/
	public void setPercent(float newPercent){
				percent = newPercent;
		image.GetComponent<RectTransform>().sizeDelta = new Vector2(1.067f * percent, 14.4f);
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
