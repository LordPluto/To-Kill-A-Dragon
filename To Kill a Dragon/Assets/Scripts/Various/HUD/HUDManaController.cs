using UnityEngine;
using System.Collections;

public class HUDManaController : MonoBehaviour {

	private float percent;
	public float xDist;
	public float yDist;
	
	private GUITexture image;
	
	public float wDist;
	public float hDist;
	
	// Use this for initialization
	void Start () {
		image = GetComponent<GUITexture> ();
		percent = 100;
	}
	
	// Update is called once per frame
	void Update () {
		float x, y, w, h;
		w = (percent/100) * wDist;
		h = hDist;
		
		x = (float)-(Camera.main.pixelWidth / 2 - xDist);
		y = (float)(Camera.main.pixelHeight / 2 - h - yDist);
		
		image.pixelInset = new Rect (x, y, w, h);
	}

	/**
	 * Sets the percentage
	 * **/
	public void setPercent(float newPercent){
				percent = newPercent;
		}

	/**
	 * Hides the HUD element
	 * **/
	public void Hide () {
		guiTexture.enabled = false;
	}
	
	/**
	 * Shows the HUD element
	 * **/
	public void Show () {
		guiTexture.enabled = true;
	}
}
