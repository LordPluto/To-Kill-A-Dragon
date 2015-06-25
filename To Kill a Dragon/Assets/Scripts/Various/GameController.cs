using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum Head{
	Fine = 0,
	HPLow = 1,
	Damaged = 2,
	Dead = 3,
	MPLow = 4
}

public enum Pole{
	North = 0,
	South = 1
}

/**
 * Class GameController extends MonoBehaviour
 * Essentially runs the game logic regarding pretty much everything
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
	private int spellIndex;

	#region MagnetSpell
	public bool MagnetActive = false;
	private Pole MagnetPole = Pole.North;
	#endregion MagnetSpell

	#endregion

	#region Cutscene

	private bool PlayerInvolved;
	private bool NPCInvolved;

	#endregion

	#region HUD Head Icon
	
	private Head currentHead;
	
	#endregion

	#region Player Wallet

	private float wallet;
	private const float WalletMax = 999999999999;

	#endregion

	private int notStartedDialogue = 2;

	/**
	 * Use this for initialization
	 * **/
	void Start () {
				Screen.showCursor = false;
				Screen.SetResolution (1280, 720, false);

				currentHead = Head.Fine;

				wallet = 0;
		}

	void Awake () {
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
				dialogueDump.AddPerson ("Victor", assetTest);
				characterFlags.Add ("Victor", 1);
				dialogueDump.AddLines (2, (TextAsset)Resources.Load ("Test/Chapter Two"), ref assetTest);
				dialogueDump.AddPerson ("Victor2", assetTest);
				characterFlags.Add ("Victor2", 2);
				//END TEST

				spellBook = GameObject.Find ("_SpellBook").GetComponent<SpellList> ();
				KnownSpells = new List<Spell> ();

				//Testing for Spells. MARKED FOR DELETION
				AddSpell (new FireSpell ());
				AddSpell (new IceSpell ());
				AddSpell (new LightningSpell ());
				AddSpell (new HealSpell ());
				AddSpell (new WindSpell ());
				AddSpell (new MagnetSpell ());
		
				spellIndex = 0;
		
				selectedSpell = KnownSpells [spellIndex];
				//END TEST
		}
	
	// Update is called once per frame
	void Update () {
				if (notStartedDialogue == 2) {
						treeControl.Activate ();
						notStartedDialogue = 1;
				} else if (notStartedDialogue == 1) {
						treeControl.Deactivate ();
						notStartedDialogue = 0;
				}

				if (!(currentHead == Head.Damaged)) {
						currentHead = SelectHead ();
						HUDControl.changeHead (currentHead);
				} else if (!playerControl.isFlinching ()) {
						currentHead = SelectHead ();
						HUDControl.changeHead (currentHead);
				}
		}

	/**
	 * Shows the dialogue box.
	 * Parameter: string NPCName - Name to look for
	 * Result: Displaying the dialogue box and dialogue, if successful. Player can't move or cast spells.
	 * **/
	public void ShowDialogue (string NPCName) {
				textBoxControl.Activate ();

				treeControl.Activate (NPCName);

				playerControl.TalkingFreeze ();

				storedNPC = GameObject.Find (NPCName).GetComponent<NPCController> ();
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
				if (spellIndex >= KnownSpells.Count - 1) {
						spellIndex = 0;
				} else {
						spellIndex++;
				}

				selectedSpell = KnownSpells [spellIndex];

				HUDControl.setIcon (selectedSpell);
		}

	/**
	 * Goes to the previous spell
	 * **/
	public void PreviousSpell(){
				if (spellIndex < 1) {
						spellIndex = KnownSpells.Count - 1;
						
				} else {
						spellIndex--;
				}

				selectedSpell = KnownSpells [spellIndex];

				HUDControl.setIcon (selectedSpell);
		}

	/**
	 * Goes to the quick spell slot
	 * **/
	public void QuickSelect(int quickSlot){
				if (quickSlot >= 0 && quickSlot < 5 && QuickSpells [quickSlot] != null) {
						selectedSpell = QuickSpells [quickSlot];
						spellIndex = KnownSpells.IndexOf (selectedSpell);
				}

				HUDControl.setIcon (selectedSpell);
		}

	/**
	 * Casts the spell selected
	 * **/
	public void CastSpell (float _characterFacing){
				if (MagnetActive || playerControl.getMP() < selectedSpell.getCost()) {
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
			                  180 - (int)_characterFacing * 90,
						                  0));
				} else if (selectedSpell is HealSpell) {
						Instantiate (selectedSpell.getSpellForm (), playerControl.getPosition (), Quaternion.Euler (0, 0, 0));
				} else if (selectedSpell is WindSpell) {
						GameObject windClone = GameObject.Find ("Wind(Clone)");
						if (windClone) {
								Destroy (windClone);
						}
						
						Instantiate (selectedSpell.getSpellForm (), playerControl.getPosition (), Quaternion.Euler (90, 0, 0));
				} else if (selectedSpell is MagnetSpell) {
						Instantiate (selectedSpell.getSpellForm (), playerControl.getPosition () + (new Vector3 (0, 101, 0) / 100),
			             Quaternion.Euler (90,
			                  180 - (int)_characterFacing * 90,
			                  0));
				}

				playerControl.changeMP (-(selectedSpell.getCost ()));
				HUDControl.setManaPercentage (playerControl.getPercentMP ());

		}

	/**
	 * Handles entering cutscene - the little black bars, taking control from the player, etc.
	 * **/
	public void EnterCutscene(bool PlayerInvolved, bool NPCInvolved, Transform[] PlayerPathPoints, Transform[] NPCPathPoints, string NPCName){
				this.PlayerInvolved = PlayerInvolved;
				this.NPCInvolved = NPCInvolved;

				if (this.PlayerInvolved) {
						playerControl.EnterCutscene (PlayerPathPoints);
				}
				if (this.NPCInvolved) {
						GameObject.Find (NPCName).GetComponent<NPCController> ().EnterCutscene (NPCPathPoints);
				}

				HUDControl.Hide ();

				foreach (GameObject enemy in GameObject.FindGameObjectsWithTag ("Enemy")) {
						enemy.GetComponent<EnemyController> ().EnterCutscene ();
				}
		}

	/**
	 * Handles leaving cutscene - the little black bars, giving control to the player, etc.
	 * **/
	private void EndCutscene() {
				playerControl.ExitCutscene ();

				HUDControl.Show ();

				foreach (GameObject enemy in GameObject.FindGameObjectsWithTag ("Enemy")) {
						enemy.GetComponent<EnemyController> ().ExitCutscene ();
				}
		}

	/**
	 * Checks to see if the game is in a cutscene.
	 * **/
	public bool InCutscene () {
				return (PlayerInvolved || NPCInvolved);
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

	/**
	 * Returns the current flag of the NPC
	 * **/
	public int getNPCFlag(string NPCName){
		int flag;
		characterFlags.TryGetValue (NPCName, out flag);
		return flag;
	}

	/**
	 * Deal damage to player
	 * **/
	public void DealPlayerDamage(GameObject monster, Vector3 playerDirection, Vector3 monsterAngle){
				if (!InCutscene ()) {
						BasicEnemyController monsterControl = monster.GetComponent<BasicEnemyController> ();

						float monsterAtk = monsterControl.Atk;

						playerControl.TakeMonsterDamage (monsterAtk, -playerDirection);

						monsterControl.FlinchBack (-monsterAngle);

						HUDControl.setHealthPercentage (playerControl.getPercentHP ());

						currentHead = Head.Damaged;
				}
		}

	/**
	 * Destroys the enemy and gives the EXP to the player
	 * **/
	public void DestroyMonster(GameObject monster){
				BasicEnemyController monsterControl = monster.GetComponent<BasicEnemyController> ();
				monsterControl.DropItems ();

				playerControl.increaseEXP (monsterControl.valueEXP ());
				Destroy (monster);
				HUDControl.setEXPPercentage (playerControl.getPercentEXP ());
		}

	/**
	 * Deals the player damage from getting hit by a bullet
	 * **/
	public void DealPlayerBulletDamage(float damage, Vector3 bulletDirection){
				if (!InCutscene ()) {
						playerControl.TakeMonsterDamage (damage, bulletDirection);

						HUDControl.setHealthPercentage (playerControl.getPercentHP ());

						currentHead = Head.Damaged;
				}
		}

	/**
	 * Heal the player the amount
	 * **/
	public void HealPlayer(float healAmount) {
				playerControl.changeHP (healAmount);

				HUDControl.setHealthPercentage (playerControl.getPercentHP ());
		}

	/**
	 * Restores the player's MP by the amount
	 * **/
	public void HealPlayerMana (float healAmount) {
				playerControl.changeMP (healAmount);

				HUDControl.setManaPercentage (playerControl.getPercentMP ());
		}

	/**
	 * Tells the Game to choose the correct head icon
	 * **/
	private Head SelectHead () {
				if (playerControl.getPercentHP () <= 0) {
						return Head.Dead;
				} else if (playerControl.getPercentHP () <= 25) {
						return Head.HPLow;
				} else if (playerControl.getPercentMP () <= 25) {
						return Head.MPLow;
				} else {
						return Head.Fine;
				}
		}

	/**
	 * Adds a given spell to the known list.
	 * **/
	public void AddSpell (Spell newSpell){
				// Not great, but the best I can do. Find is a slow function.
				if (KnownSpells.Find (x => x.getNumber () == newSpell.getNumber ()) == null) {
						newSpell.setSpellForm (spellBook.getPrefab (newSpell));
						KnownSpells.Add (newSpell);
				}
		}

	/**
	 * Handles what happens when you collect an item.
	 * **/
	public void ItemCollected(string tag, float value){
				switch (tag.Substring (4)) {
				case "Coin":
						if (wallet <= WalletMax - value) {
								wallet += value;
								HUDControl.setWallet (wallet);
						}
						break;
				case "Health":
						HealPlayer (value);
						break;
				case "Mana":
						HealPlayerMana (value);
						break;
				case "GradeA":
						break;
				default:
						Debug.Log ("Item not given the correct tag.");
						break;
				}
		}

	/**
	 * Handles what happens when a spell is upgraded
	 * **/
	public void SpellUpgrade(Transform spellForm, Spell newSpell){
				newSpell.setSpellForm (spellForm);
				int baseSpell = KnownSpells.FindIndex (x => x.getNumber () == newSpell.getNumber () - 1);

				if (baseSpell == -1) {
						return;
				} else {
						KnownSpells [baseSpell] = newSpell;
				}
		}

	/**
	 * Activates/Deactivates Magnet as necessary
	 * **/
	public void SetMagnet(bool activate){
				MagnetActive = activate;
				if (!MagnetActive) {
						playerControl.MagnetMove ();
						MagnetPole = (MagnetPole == Pole.North ? Pole.South : Pole.North);
				}
		}

	/**
	 * Handles the magnet movement if a pole is found.
	 * **/
	public void SetMagnetDirection(Vector3 magnetDirection){
		playerControl.MagnetFreeze ((MagnetPole == Pole.North ? magnetDirection : -1 * magnetDirection));
	}

	/**
	 * Handles the magnet movement if a cube is found
	 * **/
	public void SetMagnetDirection(GameObject magnetCube, Vector3 magnetDirection){
		MagnetCubeController cubeControl = magnetCube.GetComponent<MagnetCubeController> ();
		cubeControl.MagnetMovement ((MagnetPole == Pole.North ? magnetDirection : -1 * magnetDirection));
		playerControl.MagnetFreeze (Vector3.zero);
	}
}