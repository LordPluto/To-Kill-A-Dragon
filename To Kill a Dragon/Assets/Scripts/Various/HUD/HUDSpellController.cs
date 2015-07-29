using UnityEngine;
using System.Collections;

public class HUDSpellController : MonoBehaviour {

	public float xDist;
	public float yDist;
	
	private GUITexture image;
	
	public float wDist;
	public float hDist;

	public Texture[] spellIcons;
	
	// Use this for initialization
	void Start () {
				image = GetComponent<GUITexture> ();
		}
	
	// Update is called once per frame
	void Update () {
		float x, y, w, h;
		w = wDist;
		h = hDist;
		
		x = (float)-(Camera.main.pixelWidth / 2 - xDist);
		y = (float)(Camera.main.pixelHeight / 2 - h - yDist);
		
		image.pixelInset = new Rect (x, y, w, h);
	}

	GUITexture GetImage () {
				return image;
		}

	Texture[] GetIcons () {
				return spellIcons;
		}

	public void SetTexture(int index){
				if (index >= 0 && index < spellIcons.Length) {
						image.texture = spellIcons[index];
				}
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
