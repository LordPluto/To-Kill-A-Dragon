using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LoadedDialogueController : MonoBehaviour {

	public string[] loadingNPC;
	public int[] loadingFlags;

	private Dictionary<string, Dictionary<int, List<Dialogue>>> npcLines;

	// Use this for initialization
	void Start () {
				if (loadingNPC.Length != loadingFlags.Length) {
						Debug.LogError ("Big problem in dialogue loading - NPC names and flags not set correctly.");
				}
		}

	void Awake () {
				npcLines = new Dictionary<string, Dictionary<int, List<Dialogue>>> ();
				GetComponent<Renderer>().enabled = false;
		}
	
	// Update is called once per frame
	void Update () {
	
	}

	/**
	 * Load the dialogues of the NPCs listed in the array
	 * **/
	public void LoadNPCs(DialogueDump dialogueSource) {
				npcLines.Clear ();

				for (int i = 0; i<loadingNPC.Length; ++i) {
						List<Dialogue> dialogueHolder = new List<Dialogue> ();

						dialogueHolder = SetUpTree (dialogueSource.GetAsset (loadingNPC [i], loadingFlags [i]));

						if (dialogueHolder == null) {
								Debug.LogError ("Holy fuck something went VERY wrong");
						} else {
								Dictionary<int, List<Dialogue>> flaggedDialogue = new Dictionary<int, List<Dialogue>> ();
								flaggedDialogue.Add (loadingFlags [i], dialogueHolder);
								npcLines.Add (loadingNPC [i], flaggedDialogue);
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
	public List<Dialogue> getDialogue(string NPCName, int NPCFlag){
				Dictionary<int, List<Dialogue>> returnedFlaggedDialogues = new Dictionary<int, List<Dialogue>> ();
				List<Dialogue> returnedDialogue = new List<Dialogue> ();
				if (npcLines.TryGetValue (NPCName, out returnedFlaggedDialogues)) {
						if (returnedFlaggedDialogues.TryGetValue (NPCFlag, out returnedDialogue)) {
								return returnedDialogue;
						} else {
								return null;
						}
				}
				return null;
		}

	void OnTriggerEnter (Collider c){
				if (c.CompareTag ("Player")) {
						LoadNPCs (GameObject.Find ("_DialogueText").GetComponent<DialogueDump> ());
				}
		}
}
