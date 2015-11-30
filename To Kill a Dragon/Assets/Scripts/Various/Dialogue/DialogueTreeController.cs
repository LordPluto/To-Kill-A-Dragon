using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DialogueTreeController : MonoBehaviour {

	private List<Dialogue> lines;
	private int count;

	private DialogueMasterController dialogueControl;
	private GameController gameControl;

	private int nextBox;

	// Use this for initialization
	void Start () {
				lines = null;
				count = 0;
		nextBox = 0;
		}

	void Awake () {
				dialogueControl = GameObject.Find ("Dialogue Canvas").GetComponent<DialogueMasterController> ();
				gameControl = GameObject.Find ("_GameController").GetComponent<GameController> ();
		}
	
	// Update is called once per frame
	void Update () {
		if (nextBox > 0) {
						nextBox--;
				}
		}

	/**
	 * Activate and load dialogue for a given NPC Name.
	 * **/
	public void Activate (string NPCName) {
		nextBox = 10;
				foreach (GameObject o in GameObject.FindGameObjectsWithTag ("DialogueLoading SpellIgnore")) {
						lines = o.GetComponent<LoadedDialogueController> ().getDialogue (NPCName);
						if (lines != null) {
								break;
						}
				}

				dialogueControl.SetText (lines [count].getText ());
				dialogueControl.SetHead (lines [count].getImageName ());

				dialogueControl.Activate ();
		}

	public void Deactivate () {
				lines = null;
				count = 0;

				dialogueControl.Deactivate ();
		}

	public void NextTextBox () {
				if (dialogueControl.IsTextFinished () && nextBox == 0) {
			nextBox = 10;
						count++;
		
						if (count < lines.Count) {
								dialogueControl.SetText (lines [count].getText ());
								dialogueControl.SetHead (lines [count].getImageName ());
		
								dialogueControl.Activate ();
						} else {
								Close ();
						}
				}
		}

	void Close () {
				gameControl.HideDialogue ();
		}
}

public class Dialogue {

	private string text;
	private string imageName;

	public Dialogue(string te, string imageName){
				text = te;
				this.imageName = imageName;
		}

	public string getText () {
				return text;
		}

	public string getImageName () {
				return imageName;
		}
}