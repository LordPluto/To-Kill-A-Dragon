using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DialogueTreeController : MonoBehaviour {

	private TextAsset dialogue;

	private List<Dialogue> lines;
	private int count;

	// Use this for initialization
	void Start () {
				lines = new List<Dialogue> ();
				count = 0;
		}
	
	// Update is called once per frame
	void Update () {

		}

	void OnGUI () {
				if (lines.Count > 1) {
						if (count + 1 == lines.Count) {
								if (GUI.Button (new Rect (1080, 566, 128, 48), "Previous")) {
										PreviousTextBox ();
								}
								if (GUI.Button (new Rect (1080, 620, 128, 48), "Close")) {
										GameObject.Find ("_GameController").GetComponent<GameController> ().HideDialogue ();
								}
						} else if (count == 0) {
								if (GUI.Button (new Rect (1080, 620, 128, 48), "Next")) {
										NextTextBox ();
								}
						} else {
								if (GUI.Button (new Rect (1080, 566, 128, 48), "Previous")) {
										PreviousTextBox ();
								}
								if (GUI.Button (new Rect (1080, 620, 128, 48), "Next")) {
										NextTextBox ();
								}
						}
				} else if (lines.Count == 1) {
						if (GUI.Button (new Rect (1080, 620, 128, 48), "Close")) {
								GameObject.Find ("_GameController").GetComponent<GameController> ().HideDialogue ();
						}
				}
		}

	//Character Limit for one box is 374 characters
	void SetUpTree () {
				if (dialogue.text.Length <= 0) {
						return;
				}
			
				string[] splitData = dialogue.text.Split ('\n');

				for (int i = 0; i<splitData.Length/2; i++) {
						Texture image = GameObject.Find ("_DialogueImages").GetComponent<ImageDump> ().GetImage (splitData [(2 * i)]);
						string text = splitData [(2 * i) + 1];

						while (text.Length > 374) {
								lines.Add (new Dialogue (text.Substring (0, 375), image));
								text = text.Substring (374);
						}
						lines.Add (new Dialogue (text, image));
				}
		}

	public void Activate (string objectName, int flag) {
				dialogue = GameObject.Find ("_DialogueText").GetComponent<DialogueDump> ().GetAsset (objectName, flag);

				SetUpTree ();
		
				GameObject.Find ("DialogueBox").GetComponent<DialogueMasterController> ().SetText (lines [count].getText ());
				GameObject.Find ("DialogueBox").GetComponent<DialogueMasterController> ().SetTexture (lines [count].getImage ());

				GameObject.Find ("DialogueBox").GetComponent<DialogueMasterController> ().Activate ();
		}

	public void Deactivate () {
				lines.Clear ();
				dialogue = null;
		count = 0;

		GameObject.Find ("DialogueBox").GetComponent<DialogueMasterController> ().Deactivate ();
		}

	void NextTextBox () {
				count++;
		
				GameObject.Find ("DialogueBox").GetComponent<DialogueMasterController> ().SetText (lines [count].getText ());
				GameObject.Find ("DialogueBox").GetComponent<DialogueMasterController> ().SetTexture (lines [count].getImage ());
		
				GameObject.Find ("DialogueBox").GetComponent<DialogueMasterController> ().Activate ();
		}

	void PreviousTextBox () {
				count--;

				GameObject.Find ("DialogueBox").GetComponent<DialogueMasterController> ().SetText (lines [count].getText ());
				GameObject.Find ("DialogueBox").GetComponent<DialogueMasterController> ().SetTexture (lines [count].getImage ());

				GameObject.Find ("DialogueBox").GetComponent<DialogueMasterController> ().Activate ();
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