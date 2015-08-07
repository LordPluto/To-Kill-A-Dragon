using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUDManaController : MonoBehaviour {

	private float percent;
	
	private RawImage image;

	private bool needUpdate = true;
	
	// Use this for initialization
	void Start () {
		image = GetComponent<RawImage> ();
		percent = 100;
	}
	
	// Update is called once per frame
	void Update () {
		if (needUpdate) {
			needUpdate = false;
						setPercent (percent);
				}
		}

	/**
	 * Sets the percentage
	 * **/
	public void setPercent(float newPercent){
				percent = newPercent;
				image.GetComponent<RectTransform> ().sizeDelta = new Vector2 (1.06f * percent, 14.4f);
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
