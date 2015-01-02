using UnityEngine;
using System.Collections;

public class DialogueController : MonoBehaviour {

	private string text;

	private double widthOffset;
	private double heightOffset;

	private float guiHeight;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		widthOffset = Camera.main.pixelWidth / 1280;
		heightOffset = Camera.main.pixelHeight / 720;
	}

	void OnGUI () {
				GUI.skin.box.fontSize = (int)(20 * (widthOffset + heightOffset)/2);
				GUI.skin.box.wordWrap = true;
				GUI.skin.box.alignment = TextAnchor.UpperLeft;
				GUI.Box (new Rect (228 * (float)widthOffset, 582 * (float)heightOffset, 845 * (float)widthOffset, 102 * (float)heightOffset), text);
		}

	public void SetText (string textString) {
				text = textString;
		}

}
