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

	private double widthOffset;
	private double heightOffset;

	private float buttonX = 1080;
	private float buttonWidth = 128;
	private float buttonHeight = 48;

	private float topButtonY = 582;
	private float bottomButtonY = 636;

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
				widthOffset = Camera.main.pixelWidth / 1280;
				heightOffset = Camera.main.pixelHeight / 720;
		}

	void OnGUI () {
				if (lines.Count > 1) {
						if (count + 1 == lines.Count) {
								if (GUI.Button (new Rect (buttonX * (float)widthOffset, topButtonY * (float)heightOffset,
				                          buttonWidth * (float)widthOffset, buttonHeight * (float)heightOffset), "Previous")) {
										PreviousTextBox ();
								}
								if (GUI.Button (new Rect (buttonX * (float)widthOffset, bottomButtonY * (float)heightOffset, 
				                          buttonWidth * (float)widthOffset, buttonHeight * (float)heightOffset), "Close")) {
										gameControl.HideDialogue ();
								}
						} else if (count == 0) {
								if (GUI.Button (new Rect (buttonX * (float)widthOffset, bottomButtonY * (float)heightOffset, 
				                          buttonWidth * (float)widthOffset, buttonHeight * (float)heightOffset), "Next")) {
										NextTextBox ();
								}
						} else {
								if (GUI.Button (new Rect (buttonX * (float)widthOffset, topButtonY * (float)heightOffset,
				                          buttonWidth * (float)widthOffset, buttonHeight * (float)heightOffset), "Previous")) {
										PreviousTextBox ();
								}
								if (GUI.Button (new Rect (buttonX * (float)widthOffset, bottomButtonY * (float)heightOffset, 
				                          buttonWidth * (float)widthOffset, buttonHeight * (float)heightOffset), "Next")) {
										NextTextBox ();
								}
						}
				} else if (lines.Count == 1) {
						if (GUI.Button (new Rect (buttonX * (float)widthOffset, bottomButtonY * (float)heightOffset, 
			                          buttonWidth * (float)widthOffset, buttonHeight * (float)heightOffset), "Close")) {
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

				int splitPoint = (int)(374 * (float)(widthOffset + heightOffset) / 2);

				for (int i = 0; i<splitData.Length/2; i++) {
						Texture image = textImages.GetImage (splitData [(2 * i)]);	/* Possibly slow */
						string text = splitData [(2 * i) + 1];

						while (text.Length > splitPoint) {
								lines.Add (new Dialogue (text.Substring (0, splitPoint), image));
								text = text.Substring (splitPoint);
						}											/* Possibly slow */
						lines.Add (new Dialogue (text, image));
				}
		}

	public void Activate (string NPCName) {
				foreach (GameObject o in GameObject.FindGameObjectsWithTag ("DialogueLoading")) {
						lines = o.GetComponent<LoadedDialogueController> ().getDialogue (NPCName);
						if (lines != null) {
								break;
						}
				}

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