using UnityEngine;
using System.Collections;

public class DialogueImageController : MonoBehaviour {

	private GUITexture image;

	// Use this for initialization
	void Awake () {
				image = GetComponent<GUITexture> ();
		}
	
	// Update is called once per frame
	void Update () {
		}

	public void Wipe () {
				image.texture = null;
		}

	public void SetTexture (Texture t) {
				image.texture = t;

				double widthOffset = Camera.main.pixelWidth / 1280;
				double heightOffset = Camera.main.pixelHeight / 720;

				image.pixelInset = new Rect (50 * (float)widthOffset, 0, 150 * (float)widthOffset, 200 * (float)heightOffset);
		}
}
