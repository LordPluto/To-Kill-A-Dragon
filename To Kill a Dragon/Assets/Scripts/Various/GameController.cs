using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Class GameController extends MonoBehaviour
 * Essentially runs the game logic regarding dialogue boxes
 * Dictionary<string, int> characterFlags: a dictionary that holds what flag the character is set on
 * Dictionary<int, TextAsset> characterText: a (temporary) dictionary that holds the lines per character flag.
 * Dictionary<int, TextAsset> assetTest: a testing variable. MARKED FOR DELETION
 * **/
public class GameController : MonoBehaviour {

	private Dictionary<string, int> characterFlags;
	private Dictionary<int, TextAsset> characterLines;

	private Dictionary<int, TextAsset> assetTest;

	private string StoredName;

	// Use this for initialization
	void Start () {
				Screen.showCursor = false;
				Screen.SetResolution (1280, 720, false);
		}

	void Awake () {
				characterFlags = new Dictionary<string, int> ();
				characterLines = new Dictionary<int, TextAsset> ();

				//Testing for Dialogue logic. MARKED FOR DELETION
				assetTest = new Dictionary<int, TextAsset> ();
				GameObject.Find ("_DialogueText").GetComponent<DialogueDump> ().AddLines (1, (TextAsset)Resources.Load ("Test/Chapter One"),
		                                                                          ref assetTest);
				GameObject.Find ("_DialogueText").GetComponent<DialogueDump> ().AddLines (2, (TextAsset)Resources.Load ("Test/Chapter Two"),
		                                                                          ref assetTest);
				GameObject.Find ("_DialogueText").GetComponent<DialogueDump> ().AddPerson ("Victor", assetTest);
				characterFlags.Add ("Victor", 1);
				//END TEST
		}
	
	// Update is called once per frame
	void Update () {

		}

	/**
	 * Shows the dialogue box.
	 * Parameter: string objectName - Name to look for
	 * Result: Displaying the dialogue box and dialogue, if successful. Player can't move or cast spells.
	 * **/
	public void ShowDialogue (string objectName) {
				GameObject.Find ("_Textbox Controller").GetComponent<TextboxController> ().Activate ();

				int flag;
				characterFlags.TryGetValue (objectName, out flag);
				GameObject.Find ("DialogueTree").GetComponent<DialogueTreeController> ().Activate (objectName, flag);

				GameObject.Find ("Player").GetComponent<PlayerController> ().TalkingFreeze ();
				GameObject.Find ("Player").GetComponent<PlayerAnimationController> ().TalkingFreeze ();

				GameObject.Find (objectName).GetComponent<NPCController> ().TalkingFreeze ();
				StoredName = objectName;
		}

	/**
	 * Hides the dialogue box.
	 * Result: Removed the dialogue box and dialogue, if successful. Player can move and cast spells.
	 * **/
	public void HideDialogue () {
				GameObject.Find ("_Textbox Controller").GetComponent<TextboxController> ().Deactivate ();
				GameObject.Find ("DialogueTree").GetComponent<DialogueTreeController> ().Deactivate ();

				GameObject.Find ("Player").GetComponent<PlayerController> ().TalkingMove ();
				GameObject.Find ("Player").GetComponent<PlayerAnimationController> ().TalkingMove ();

				GameObject.Find (StoredName).GetComponent<NPCController> ().TalkingMove ();
		}
}
