using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUDHeadController : MonoBehaviour {

	private RawImage image;

	/**
	 * 0 - Okay
	 * 1 - Low
	 * 2 - Damage
	 * 3 - Dead
	 * 4 - MP Low
	 * **/
	public Texture[] heads;
	
	// Use this for initialization
	void Start () {
				image = GetComponent<RawImage> ();
		}
	
	// Update is called once per frame
	void Update () {
		}

	/**
	 * Sets the head using an Enum
	 * **/
	public void SetHead(Head head){
				image.texture = heads [(int)head];
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
