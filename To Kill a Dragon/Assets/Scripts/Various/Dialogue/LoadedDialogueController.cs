using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LoadedDialogueController : MonoBehaviour {

	public string[] loadingNPC;

	private Dictionary<string, List<Dialogue>> npcLines;

	private GameController gameControl;

	// Use this for initialization
	void Start () {
				npcLines = new Dictionary<string, List<Dialogue>> ();
				renderer.enabled = false;
		}

	void Awake () {
				gameControl = GameObject.Find ("_GameController").GetComponent<GameController> ();
		}
	
	// Update is called once per frame
	void Update () {
	
	}

	/**
	 * Load the dialogues of the NPCs listed in the array
	 * **/
	public void LoadNPCs(DialogueDump dialogueSource) {
				foreach (string s in loadingNPC) {
						List<Dialogue> dialogueHolder = new List<Dialogue> ();

						dialogueHolder = SetUpTree (dialogueSource.GetAsset (s, gameControl.getNPCFlag (s)));

						if (dialogueHolder == null) {
								Debug.LogError ("Holy fuck something went VERY wrong");
						} else {
								npcLines.Remove (s);
								npcLines.Add (s, dialogueHolder);
						}
				}
		}

	/**
	 * Set up the dialogue tree given the text and image source
	 * **/
	private List<Dialogue> SetUpTree (TextAsset NPCDialogue) {
				List<Dialogue> lines = new List<Dialogue> ();

				if (NPCDialogue == null || NPCDialogue.text.Length <= 0) {
						return null;
				}
		
				string[] splitData = NPCDialogue.text.Split ('\n');		/* Possibly slow */
				bool isImage = true;
				string image = null;
				
				for (int i = 0; i<splitData.Length; i++) {
						if (isImage == true) {
								image = splitData [i];	/* Possibly slow */
								isImage = false;
						} else if (splitData [i] == "" || splitData[i] == "\r") {
								isImage = true;
						} else {
								string text = splitData [i];
								lines.Add (new Dialogue (text, image));
						}
				}

				return lines;
		}

	/**
	 * Gets the loaded dialogue for the NPC
	 * **/
	public List<Dialogue> getDialogue(string NPCName){
				List<Dialogue> returnedDialogue = new List<Dialogue> ();
				if (npcLines.TryGetValue (NPCName, out returnedDialogue)) {
						return returnedDialogue;
				} else {
						return null;
				}
		}

	void OnTriggerEnter (Collider c){
				if (c.CompareTag ("Player")) {
						LoadNPCs (GameObject.Find ("_DialogueText").GetComponent<DialogueDump> ());
				}
		}
}
