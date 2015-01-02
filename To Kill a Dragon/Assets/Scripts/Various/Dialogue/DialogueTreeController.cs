using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DialogueTreeController : MonoBehaviour {

	private TextAsset dialogue;

	private List<Dialogue> lines;
	private int count;

	private DialogueMasterController dialogueControl;
	private GameController gameControl;
	private ImageDump textImages;

	// Use this for initialization
	void Start () {
				lines = new List<Dialogue> ();
				count = 0;
		}

	void Awake () {
				dialogueControl = GameObject.Find ("DialogueBox").GetComponent<DialogueMasterController> ();
				textImages = GameObject.Find ("_DialogueImages").GetComponent<ImageDump> ();
				gameControl = GameObject.Find ("_GameController").GetComponent<GameController> ();
		}
	
	// Update is called once per frame
	void Update () {

		}

	void OnGUI () {
				if (lines.Count > 1) {
						if (count + 1 == lines.Count) {
								if (GUI.Button (new Rect (1080, 582, 128, 48), "Previous")) {
										PreviousTextBox ();
								}
								if (GUI.Button (new Rect (1080, 636, 128, 48), "Close")) {
										gameControl.HideDialogue ();
								}
						} else if (count == 0) {
								if (GUI.Button (new Rect (1080, 636, 128, 48), "Next")) {
										NextTextBox ();
								}
						} else {
								if (GUI.Button (new Rect (1080, 582, 128, 48), "Previous")) {
										PreviousTextBox ();
								}
								if (GUI.Button (new Rect (1080, 636, 128, 48), "Next")) {
										NextTextBox ();
								}
						}
				} else if (lines.Count == 1) {
						if (GUI.Button (new Rect (1080, 636, 128, 48), "Close")) {
								gameControl.HideDialogue ();
						}
				}
		}

	//Character Limit for one box is 374 characters
	void SetUpTree () {
				if (dialogue.text.Length <= 0) {
						return;
				}
			
				string[] splitData = dialogue.text.Split ('\n');		/* Possibly slow */

				for (int i = 0; i<splitData.Length/2; i++) {
						Texture image = textImages.GetImage (splitData [(2 * i)]);	/* Possibly slow */
						string text = splitData [(2 * i) + 1];

						while (text.Length > 374) {
								lines.Add (new Dialogue (text.Substring (0, 375), image));
								text = text.Substring (374);
						}											/* Possibly slow */
						lines.Add (new Dialogue (text, image));
				}
		}

	public void Activate (string objectName, int flag) {
				dialogue = GameObject.Find ("_DialogueText").GetComponent<DialogueDump> ().GetAsset (objectName, flag);

				SetUpTree ();
		
				dialogueControl.SetText (lines [count].getText ());
				dialogueControl.SetTexture (lines [count].getImage ());

				dialogueControl.Activate ();
		}

	public void Deactivate () {
				lines.Clear ();
				dialogue = null;
				count = 0;

				dialogueControl.Deactivate ();
		}

	void NextTextBox () {
				count++;
		
				dialogueControl.SetText (lines [count].getText ());
				dialogueControl.SetTexture (lines [count].getImage ());
		
				dialogueControl.Activate ();
		}

	void PreviousTextBox () {
				count--;

				dialogueControl.SetText (lines [count].getText ());
				dialogueControl.SetTexture (lines [count].getImage ());

				dialogueControl.Activate ();
		}
}

public class Dialogue {

	private string text;
	private Texture image;

	public Dialogue(string te, Texture im){
				text = te;
				image = im;
		}

	public string getText () {
				return text;
		}

	public Texture getImage () {
				return image;
		}
}