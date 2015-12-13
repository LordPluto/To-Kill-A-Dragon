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
	private PlayerMasterController playerControl;
	private HUDController HUDControl;

	private NPCController storedNPC;

	private MenuController MenuCanvas;

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
	private int magnetDelay;
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

	#region Menus

	private bool menuOpen;

	#endregion

	#region Holders
	
	private List<StopOnTalk> talkingList;
	private List<StopOnFreeze> freezingList;
	
	#endregion

	/**
	 * Use this for initialization
	 * **/
	void Start () {
				treeControl = GameObject.Find ("DialogueTree").GetComponent<DialogueTreeController> ();
				playerControl = GameObject.Find ("Player").GetComponent<PlayerMasterController> ();
				HUDControl = GameObject.Find ("HUD Canvas").GetComponent<HUDController> ();
				dialogueDump = GameObject.Find ("_DialogueText").GetComponent<DialogueDump> ();
				
				MenuCanvas = GameObject.Find ("Enter Menu").GetComponent<MenuController> ();
				MenuCanvas.SetEnabled (false);
		
		
				//Testing for Dialogue logic. MARKED FOR DELETION
				dialogueDump.AddLines ("Victor", 1, (TextAsset)Resources.Load ("Test/Chapter One"));
				SetNPCFlag ("Victor", 1);
				dialogueDump.AddLines ("Victor2", 1, (TextAsset)Resources.Load ("Test/Chapter Two"));
				SetNPCFlag ("Victor2", 1);
				dialogueDump.AddLines ("new Sarah Sprite", 3, (TextAsset)Resources.Load ("Test/Sarah Time Shenanigans"));
				SetNPCFlag ("new Sarah Sprite", 3);
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

				DontDestroyOnLoad (GameObject.Find ("EventSystem"));
		}

	void Awake () {
				Screen.showCursor = false;
				//Screen.SetResolution (1280, 720, false);
		
				currentHead = Head.Fine;
				menuOpen = false;
		
				wallet = 0;
		
				characterFlags = new Dictionary<string, int> ();
				characterLines = new Dictionary<int, TextAsset> ();

				talkingList = new List<StopOnTalk> ();
				freezingList = new List<StopOnFreeze> ();

				DontDestroyOnLoad (transform.gameObject);
		}

	void OnLevelWasLoaded (int level) {
				talkingList.Clear ();
				freezingList.Clear ();

				playerControl = GameObject.Find ("Player").GetComponent<PlayerMasterController> ();
				dialogueDump = GameObject.Find ("_DialogueText").GetComponent<DialogueDump> ();
				treeControl = GameObject.Find ("DialogueTree").GetComponent<DialogueTreeController> ();
		}
	
	// Update is called once per frame
	void Update () {
				if (!(currentHead == Head.Damaged)) {
						currentHead = SelectHead ();
						HUDControl.changeHead (currentHead);
				} else if (!playerControl.isFlinching ()) {
						currentHead = SelectHead ();
						HUDControl.changeHead (currentHead);
				}

				--magnetDelay;
		}

	/**
	 * <para>Shows the dialogue box.</para>
	 * <para>Player cannot move or cast spells.</para>
	 * <param name="NPCName">Name to look for</param>
	 * <param flag="NPCFlag">Flag to look for. Ignored by default.</param>
	 * **/
	public void ShowDialogue (string NPCName, int NPCFlag=-1) {
				if (NPCFlag > -1) {
						SetNPCFlag (NPCName, NPCFlag);
				}
				treeControl.Activate (NPCName, getNPCFlag (NPCName));

				HaltTalkingBehaviours ();
		}

	/**
	 * <para>Hides the dialogue box.</para>
	 * <para>Removed the dialogue box and dialogue, if successful. Player can move and cast spells.</para>
	 * **/
	public void HideDialogue () {
				treeControl.Deactivate ();

				MoveTalkingBehaviours ();
		}

	/**
	 * <para>Stops all objects that shouldn't move when dialogue is happening</para>
	 * **/
	private void HaltTalkingBehaviours () {
				foreach (StopOnTalk SoT in talkingList) {
						SoT.TalkingFreeze ();
				}
		}

	/**
	 * <para>Resumes movement on all objects that shouldn't move when dialogue is happening</para>
	 * **/
	private void MoveTalkingBehaviours () {
				foreach (StopOnTalk SoT in talkingList) {
						SoT.TalkingMove ();
				}
		}

	/**
	 * <returns>Currently selected spell</returns>
	 * **/
	public Spell getSpell(){
				return selectedSpell;
		}

	/**
	 * <para>Goes to the next spell</para>
	 * **/
	public void NextSpell(){
				if (spellIndex >= KnownSpells.Count - 1) {
						spellIndex = 0;
				} else {
						spellIndex++;
				}

				selectedSpell = KnownSpells [spellIndex];

				HUDControl.setIcon (selectedSpell, MagnetPole);
		}

	/**
	 * <para>Goes to the previous spell</para>
	 * **/
	public void PreviousSpell(){
				if (spellIndex < 1) {
						spellIndex = KnownSpells.Count - 1;
						
				} else {
						spellIndex--;
				}

				selectedSpell = KnownSpells [spellIndex];

				HUDControl.setIcon (selectedSpell, MagnetPole);
		}

	/**
	 * <para>Goes to the quick spell slot</para>
	 * <param name="quickSlot">Quick select slot</param>
	 * **/
	public void QuickSelect(int quickSlot){
				if (quickSlot >= 0 && quickSlot < 5 && QuickSpells [quickSlot] != null) {
						selectedSpell = QuickSpells [quickSlot];
						spellIndex = KnownSpells.IndexOf (selectedSpell);
				}

				HUDControl.setIcon (selectedSpell, MagnetPole);
		}

	/**
	 * Casts the spell selected
	 * **/
	public void CastSpell (float _characterFacing){
				if (MagnetActive || magnetDelay > 0 || playerControl.getMP() < selectedSpell.getCost()) {
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
	 * <para>Sets the current flag of the NPC</para>
	 * <param name="NPCName">Name of NPC to modify</param>
	 * <param name="NPCFlag">New flag</param>
	 * **/
	public void SetNPCFlag(string NPCName, int NPCFlag) {
				characterFlags.Remove (NPCName);
				characterFlags.Add (NPCName, NPCFlag);
		}

	/**
	 * Lets the player advance textboxes by pressing spacebar.
	 * **/
	public void talkingNext() {
				treeControl.NextTextBox ();
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
						HUDControl.changeHead (currentHead);
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
						HUDControl.changeHead (currentHead);
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
				} else if (playerControl.isFlinching ()) {
						return Head.Damaged;
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
						HUDControl.setIcon (selectedSpell, MagnetPole);
						magnetDelay = 20;
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

	/**
	 * <para>Opens/closes the menu</para>
	 * **/
	public void MenuInput() {
				menuOpen = !menuOpen;
				MenuCanvas.SetEnabled (menuOpen);
				Time.timeScale = (menuOpen ? 0 : 1);
		}

	/**
	 * <para>Opens/closes the pause screen</para>
	 * **/
	public void PauseInput() {

		}

	/**
	 * <para>Accessor for the wallet value</para>
	 * <returns>The amount of money the player has</returns>
	 * **/
	public float GetWallet() {
				return wallet;
		}

	/**
	 * <para>Adds the object to the StopOnTalk list</para>
	 * <param name="TalkObject">The MonoBehaviour to add</param>
	 * **/
	public void AddStopOnTalk(MonoBehaviour TalkObject) {
				talkingList.Add ((StopOnTalk)TalkObject);
		}

	/**
	 * <para>Adds the object to the StopOnFreeze list</para>
	 * <param name="FreezeObject">The MonoBehaviour to add</param>
	 * **/
	public void AddStopOnFreeze(MonoBehaviour FreezeObject) {
				talkingList.Add ((StopOnTalk)FreezeObject);
		}

	/**
	 * <para>Stops all objects that shouldn't move when told not to</para>
	 * **/
	private void HaltFreezingBehaviours () {
		foreach (StopOnFreeze SoF in freezingList) {
			SoF.Freeze ();
		}
	}
	
	/**
	 * <para>Resumes movement on all objects that shouldn't move when told not to</para>
	 * **/
	private void MoveFreezingBehaviours () {
		foreach (StopOnFreeze SoF in freezingList) {
			SoF.Move ();
		}
	}
}