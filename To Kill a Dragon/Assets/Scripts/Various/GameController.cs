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

	#region Dictionaries

	private Dictionary<string, int> characterFlags;
	private Dictionary<int, TextAsset> characterLines;

	private Dictionary<int, TextAsset> assetTest;

	#endregion

	#region Components

	private DialogueDump dialogueDump;
	private DialogueTreeController treeControl;
	private TextboxController textBoxControl;
	private PlayerMasterController playerControl;
	private HUDController HUDControl;

	private NPCController storedNPC;

	#endregion

	#region Spells

	private SpellList spellBook;
	private List<Spell> KnownSpells;
	private Spell selectedSpell;
	private Spell[] QuickSpells = new Spell[5];

	#endregion

	#region Cutscene

	private bool PlayerInvolved;
	private bool NPCInvolved;

	#endregion

	// Use this for initialization
	void Start () {
				Screen.showCursor = false;
				Screen.SetResolution (1280, 720, false);
		}

	void Awake () {
				spellBook = GameObject.Find ("_SpellBook").GetComponent<SpellList> ();
				KnownSpells = new List<Spell> ();
				KnownSpells.Add (new FireSpell ());
				KnownSpells.Add (new IceSpell ());
				KnownSpells.Add (new LightningSpell ());
				foreach (Spell s in KnownSpells) {
						s.setSpellForm (spellBook.getPrefab (s));
				}
				selectedSpell = KnownSpells [0];


				treeControl = GameObject.Find ("DialogueTree").GetComponent<DialogueTreeController> ();
				textBoxControl = GameObject.Find ("_Textbox Controller").GetComponent<TextboxController> ();
				playerControl = GameObject.Find ("Player").GetComponent<PlayerMasterController> ();
				HUDControl = GameObject.Find ("HUD").GetComponent<HUDController> ();
				dialogueDump = GameObject.Find ("_DialogueText").GetComponent<DialogueDump> ();


				characterFlags = new Dictionary<string, int> ();
				characterLines = new Dictionary<int, TextAsset> ();

				
				//Testing for Dialogue logic. MARKED FOR DELETION
				assetTest = new Dictionary<int, TextAsset> ();
				dialogueDump.AddLines (1, (TextAsset)Resources.Load ("Test/Chapter One"), ref assetTest);
				dialogueDump.AddLines (2, (TextAsset)Resources.Load ("Test/Chapter Two"), ref assetTest);
				dialogueDump.AddPerson ("Victor", assetTest);
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
				textBoxControl.Activate ();

				int flag;
				characterFlags.TryGetValue (objectName, out flag);
				treeControl.Activate (objectName, flag);

				playerControl.TalkingFreeze ();

				storedNPC = GameObject.Find (objectName).GetComponent<NPCController> ();
				storedNPC.TalkingFreeze ();
		}

	/**
	 * Hides the dialogue box.
	 * Result: Removed the dialogue box and dialogue, if successful. Player can move and cast spells.
	 * **/
	public void HideDialogue () {
				textBoxControl.Deactivate ();
				treeControl.Deactivate ();

				playerControl.TalkingMove ();

				storedNPC.TalkingMove ();
		}

	/**
	 * Gets the selected spell
	 * **/
	public Spell getSpell(){
				return selectedSpell;
		}

	/**
	 * Goes to the next spell
	 * **/
	public void NextSpell(){
				if (selectedSpell.getNumber () >= KnownSpells.Count - 1) {
						selectedSpell = KnownSpells [0];
				} else {
						selectedSpell = KnownSpells [selectedSpell.getNumber () + 1];
				}

				HUDControl.setIcon (selectedSpell);
		}

	/**
	 * Goes to the previous spell
	 * **/
	public void PreviousSpell(){
				if (selectedSpell.getNumber () < 1) {
						selectedSpell = KnownSpells [KnownSpells.Count - 1];
				} else {
						selectedSpell = KnownSpells [selectedSpell.getNumber () - 1];
				}

				HUDControl.setIcon (selectedSpell);
		}

	/**
	 * Goes to the quick spell slot
	 * **/
	public void QuickSelect(int quickSlot){
				if (quickSlot >= 0 && quickSlot < 5 && QuickSpells [quickSlot] != null) {
						selectedSpell = QuickSpells [quickSlot];
				}

				HUDControl.setIcon (selectedSpell);
		}

	/**
	 * Casts the spell selected
	 * **/
	public void CastSpell (float _characterFacing){
				if (playerControl.getMP () < selectedSpell.getCost ()) {
						GameObject iceClone = GameObject.Find ("Ice(Clone)");
						if (iceClone) {
								Destroy (iceClone);
						}
						return;
				}

				if (selectedSpell is FireSpell) {
						Instantiate (selectedSpell.getSpellForm (), 
						             playerControl.getPosition () + new Vector3 ((_characterFacing % 2 == 1 ? 
			                                             								(_characterFacing == 1 ? 1 : -1) : 0),
											                                       (_characterFacing == 2 ? 2 : 1),
											                                       (_characterFacing % 2 == 0 ? 
			 																			(_characterFacing == 0 ? -1 : 1) : 0)),						 															
						             Quaternion.Euler (0,
						                  0,
						                  _characterFacing * 90));
				} else if (selectedSpell is IceSpell) {
						GameObject iceClone = GameObject.Find ("Ice(Clone)");
						if (iceClone) {
								if (Quaternion.Angle (iceClone.transform.rotation, Quaternion.Euler (90,
				                                                                   180 - (int)_characterFacing * 90,
				                                                                   0)) > 10) {
										Destroy (iceClone);
										Instantiate (selectedSpell.getSpellForm (),
								             playerControl.getPosition () + new Vector3 ((_characterFacing % 2 == 1 ?
								                                        					(_characterFacing == 1 ? 1 : -1) : 0),
												                                       2,
												                                       (_characterFacing % 2 == 0 ?
												 											(_characterFacing == 0 ? -1 : 1) : 0)) / 3,
								             Quaternion.Euler (90,
								                  180 - (int)_characterFacing * 90,
								                  0));
								}
						} else {
								Instantiate (selectedSpell.getSpellForm (),
							             	playerControl.getPosition () + new Vector3 ((_characterFacing % 2 == 1 ?
								                                            (_characterFacing == 1 ? 1 : -1) : 0),
								                                           2,
								                                           (_characterFacing % 2 == 0 ?
											 (_characterFacing == 0 ? -1 : 1) : 0)) / 3,
											             Quaternion.Euler (90,
											                  180 - (int)_characterFacing * 90,
											                  0));
						}
				} else if (selectedSpell is LightningSpell) {
						Instantiate (selectedSpell.getSpellForm (),
						             playerControl.getPosition () + new Vector3 ((_characterFacing % 2 == 1 ?
										                                        	(_characterFacing == 1 ? 1 : -1) : 0),
										                                       2,
										                                       (_characterFacing % 2 == 0 ?
															 						(_characterFacing == 0 ? -1 : 1) : 0)) / 3,
						             Quaternion.Euler (90,
						                  _characterFacing * -90,
						                  0));
				}

				playerControl.changeMP (-(selectedSpell.getCost ()));
				HUDControl.setMana (playerControl.getPercentMP ());

		}

	/**
	 * Handles entering cutscene - the little black bars, taking control from the player, etc.
	 * **/
	public void EnterCutscene(bool PlayerInvolved, bool NPCInvolved, Transform[] pathPoints){
				this.PlayerInvolved = PlayerInvolved;
				this.NPCInvolved = NPCInvolved;

				playerControl.EnterCutscene (pathPoints);

		HUDControl.Hide ();
		}

	/**
	 * Handles leaving cutscene - the little black bars, giving control to the player, etc.
	 * **/
	private void EndCutscene() {
		playerControl.ExitCutscene ();

		HUDControl.Show ();
	}

	/**
	 * Informs the system that the Player is done with their cutscene stuff
	 * **/
	public void PlayerFinishedCutscene(){
				PlayerInvolved = false;
				if (!NPCInvolved) {
						EndCutscene ();
				}
		}

	/**
	 * Informs the system that the NPC is done with their cutscene stuff
	 * **/
	public void NPCFinishedCutscene(){
				NPCInvolved = false;
				if (!PlayerInvolved) {
						EndCutscene ();
				}
		}
}
