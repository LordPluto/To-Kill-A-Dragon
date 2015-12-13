using UnityEngine;
using System.Collections;

public class DialogueMasterController : MonoBehaviour {

	private DialogueController text;
	private DialogueImageController image;
	private DialogueBackgroundController background;
	private NextButtonController button;
	private TextboxController textbox;

	private string textString;
	private string imageName;

	// Use this for initialization
	void Start () {
				text = GetComponentInChildren<DialogueController> ();
				image = GetComponentInChildren<DialogueImageController> ();
				background = GetComponentInChildren<DialogueBackgroundController> ();
				button = GameObject.Find ("NextTextBox").GetComponent<NextButtonController> ();
				textbox = GetComponentInChildren<TextboxController> ();

				DontDestroyOnLoad (GameObject.Find ("DialogueButton Canvas"));
		}

	void Awake () {
		DontDestroyOnLoad (transform.gameObject);
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
				textbox.Activate ();
		}

	public void Deactivate () {
				text.SetText ("");
				text.enabled = false;
				image.Wipe ();
				background.Deactivate ();
				button.Deactivate ();
				textbox.Deactivate ();
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

	public bool IsTextFinished(){
		return !text.enabled;
	}
}
