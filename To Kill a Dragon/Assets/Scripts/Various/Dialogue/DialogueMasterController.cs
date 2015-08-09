using UnityEngine;
using System.Collections;

public class DialogueMasterController : MonoBehaviour {

	private DialogueController text;
	private DialogueImageController image;
	private DialogueBackgroundController background;
	private NextButtonController button;

	private string textString;
	private string imageName;

	// Use this for initialization
	void Awake () {
				text = GetComponentInChildren<DialogueController> ();
				image = GetComponentInChildren<DialogueImageController> ();
				background = GetComponentInChildren<DialogueBackgroundController> ();
				button = GameObject.Find ("NextTextBox").GetComponent<NextButtonController> ();
		}
	
	// Update is called once per frame
	void Update () {
	}

	public void Activate () {
				text.SetText (textString);
				text.enabled = true;
				image.SetHead (imageName);
				background.Activate ();
				button.Deactivate ();
		}

	public void Deactivate () {
				text.SetText ("");
				text.enabled = false;
				image.Wipe ();
				background.Deactivate ();
				button.Deactivate ();
		}

	public void SetHead (string imageName){
				this.imageName = imageName;
		}

	public void SetText (string tS){
				textString = tS;
		}

	public void TextFinished(){
				text.enabled = false;
				button.Activate ();
		}
}
