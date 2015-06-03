using UnityEngine;
using System.Collections;

public class HUDMoneyController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
				float x = (float)(Camera.main.pixelWidth / 2 - guiTexture.pixelInset.width);
				float y = (float)(Camera.main.pixelHeight / 2 - guiTexture.pixelInset.height);	
				guiTexture.pixelInset = new Rect (x, y, guiTexture.pixelInset.width, guiTexture.pixelInset.height);
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
