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
	public void LoadNPCs(DialogueDump dialogueSource, ImageDump imageSource) {
				foreach (string s in loadingNPC) {
						List<Dialogue> dialogueHolder = new List<Dialogue> ();

						dialogueHolder = SetUpTree (dialogueSource.GetAsset (s, gameControl.getNPCFlag (s)), imageSource);

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
	private List<Dialogue> SetUpTree (TextAsset NPCDialogue, ImageDump imageSource) {
				List<Dialogue> lines = new List<Dialogue> ();

				double widthOffset = Camera.main.pixelWidth / 1280;
				double heightOffset = Camera.main.pixelHeight / 720;

				if (NPCDialogue.text.Length <= 0) {
						return null;
				}
		
				string[] splitData = NPCDialogue.text.Split ('\n');		/* Possibly slow */
		
				int splitPoint = (int)(374 * (float)(widthOffset + heightOffset) / 2);
		
				for (int i = 0; i<splitData.Length/2; i++) {
						Texture image = imageSource.GetImage (splitData [(2 * i)]);	/* Possibly slow */
						string text = splitData [(2 * i) + 1];
			
						while (text.Length > splitPoint) {
								lines.Add (new Dialogue (text.Substring (0, splitPoint), image));
								text = text.Substring (splitPoint);
						}											/* Possibly slow */
						lines.Add (new Dialogue (text, image));
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
						LoadNPCs (GameObject.Find ("_DialogueText").GetComponent<DialogueDump> (), GameObject.Find ("_DialogueImages").GetComponent<ImageDump> ());
				}
		}
}
