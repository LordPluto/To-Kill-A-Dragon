using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUDExpController : MonoBehaviour {

	private RawImage image;
	private float percent;

	private bool needUpdate = true;
	
	// Use this for initialization
	void Start () {
		image = GetComponent<RawImage> ();
		percent = 0;
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
				image.GetComponent<RectTransform>().sizeDelta = new Vector2(0.582f * percent, 10f);
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
