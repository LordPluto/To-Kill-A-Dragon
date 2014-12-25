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
				image.pixelInset = new Rect (50, 26, 150, 200);
		}
}
