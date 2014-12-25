using UnityEngine;
using System.Collections;

public class TextboxController : MonoBehaviour {

	private GUITexture image;
	
	private float xDist = 0;
	private float yDist = 0;
	
	private float wDist;
	private float hDist;
	
	// Use this for initialization
	void Start () {
				image = GetComponent<GUITexture> ();
		}
	
	// Update is called once per frame
	void Update () {
				float x, y, w, h;
				w = Screen.width;
				h = Screen.height/4;

				x = xDist;
				y = yDist;
		
				image.pixelInset = new Rect (x, y, w, h);
		}

	public void Activate () {
				image.enabled = true;
		}

	public void Deactivate () {
				image.enabled = false;
		}
}