using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUDSpellController : MonoBehaviour {

	private RawImage image;

	public Texture[] spellIcons;
	
	// Use this for initialization
	void Start () {
				image = GetComponent<RawImage> ();
		}
	
	// Update is called once per frame
	void Update () {
	}

	RawImage GetImage () {
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
		image.enabled = false;
	}
	
	/**
	 * Shows the HUD element
	 * **/
	public void Show () {
		image.enabled = true;
	}
}
