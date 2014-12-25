using UnityEngine;
using System.Collections;

public class DialogueController : MonoBehaviour {

	private string text;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI () {
				GUI.skin.box.fontSize = 20;
				GUI.skin.box.wordWrap = true;
				GUI.skin.box.alignment = TextAnchor.UpperLeft;
				GUI.Box (new Rect (228, 566, 845, 102), text);
		}

	public void SetText (string textString) {
				text = textString;
		}

}
