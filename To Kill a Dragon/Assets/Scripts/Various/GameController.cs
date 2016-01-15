using UnityEngine;
using UnityEngine.SceneManagement;
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

	private bool InCutscene;

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
	private List<StopOnCutscene> cutsceneList;
	
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
				dialogueDump.AddLines ("new Sarah Sprite", 1, (TextAsset)Resources.Load ("Test/Sarah Time Shenanigans"));
				SetNPCFlag ("new Sarah Sprite", 1);
				//END TEST
		
				spellBook = GameObject.Find ("_SpellBook").GetComponent<SpellList> ();
		
				//Testing for Spells. MARKED FOR DELETION
		AddSpell (SPELL.Fire);
		AddSpell (SPELL.Ice);
		AddSpell (SPELL.Lightning);
		AddSpell (SPELL.Heal);
		AddSpell (SPELL.Wind);
		AddSpell (SPELL.MagnetNorth);
		
				spellIndex = 0;
		
				selectedSpell = KnownSpells [spellIndex];
				//END TEST

				DontDestroyOnLoad (GameObject.Find ("EventSystem"));
		}

	void Awake () {
		Cursor.visible = false;
		//Screen.SetResolution (1280, 720, false);
		
		currentHead = Head.Fine;
		menuOpen = false;
		
		wallet = 0;
		
		characterFlags = new Dictionary<string, int> ();
		characterLines = new Dictionary<int, TextAsset> ();

		talkingList = new List<StopOnTalk> ();
		freezingList = new List<StopOnFreeze> ();
		cutsceneList = new List<StopOnCutscene> ();

		KnownSpells = new List<Spell> ();

		DontDestroyOnLoad (transform.gameObject);
	}

	void OnLevelWasLoaded (int level) {
		talkingList.Clear ();
		freezingList.Clear ();
		cutsceneList.Clear ();

		playerControl = GameObject.Find ("Player").GetComponent<PlayerMasterController> ();
		dialogueDump = GameObject.Find ("_DialogueText").GetComponent<DialogueDump> ();
		treeControl = GameObject.Find ("DialogueTree").GetComponent<DialogueTreeController> ();

		if (SceneManager.GetActiveScene ().name.Equals ("Loading Screen")) {
			this.enabled = false;
		} else {
			this.enabled = true;
		}
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
	 * <param name="NPCFlag">Flag to look for. Ignored by default.</param>
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
	 * <para>Is the dialogue box active or not</para>
	 * <returns>True if active, false if not</returns>
	 * **/
	public bool IsDialogueActive () {
		return treeControl.IsActive ();
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
		Debug.Log ("FUCK redo this");
		}

	/**
	 * Casts the spell selected
	 * **/
	public void CastSpell (float _characterFacing){
		if (MagnetActive || magnetDelay > 0 || playerControl.getMP () < selectedSpell.getCost ()) {
			return;
		}

		float PlayerRotation = playerControl.GetCameraRotation ();
		float FacingDegrees = _characterFacing * 90;

		if (selectedSpell is FireSpell) {
			Instantiate (selectedSpell.getSpellForm (), 
				playerControl.getPosition () + new Vector3 (Mathf.Sin ((FacingDegrees - PlayerRotation) * Mathf.Deg2Rad),
					(_characterFacing == 2 ? 2 : 1),
					-Mathf.Cos ((FacingDegrees - PlayerRotation) * Mathf.Deg2Rad)),						 															
				Quaternion.Euler (0,
					PlayerRotation,
					FacingDegrees));
		} else if (selectedSpell is IceSpell) {
			GameObject iceClone = GameObject.Find ("Ice(Clone)");
			if (iceClone) {
				if (Quaternion.Angle (iceClone.transform.rotation, Quaternion.Euler (90,
					180 - ((int)FacingDegrees - PlayerRotation),
					    0)) > 10) {
					Destroy (iceClone);
					Instantiate (selectedSpell.getSpellForm (),
						playerControl.getPosition () + new Vector3 (Mathf.Sin ((FacingDegrees - PlayerRotation) * Mathf.Deg2Rad),
							2,
							-Mathf.Cos ((FacingDegrees - PlayerRotation) * Mathf.Deg2Rad)) / 3,
						Quaternion.Euler (90,
							180 - ((int)FacingDegrees - PlayerRotation),
							0));
				}
			} else {
				Instantiate (selectedSpell.getSpellForm (),
					playerControl.getPosition () + new Vector3 (Mathf.Sin ((FacingDegrees - PlayerRotation) * Mathf.Deg2Rad),
						2,
						-Mathf.Cos ((FacingDegrees - PlayerRotation) * Mathf.Deg2Rad)) / 3,
					Quaternion.Euler (90,
						180 - ((int)FacingDegrees - PlayerRotation),
						0));
			}
		} else if (selectedSpell is LightningSpell) {
			Instantiate (selectedSpell.getSpellForm (),
				playerControl.getPosition () + new Vector3 (Mathf.Sin ((FacingDegrees - PlayerRotation) * Mathf.Deg2Rad),
					2,
					-Mathf.Cos ((FacingDegrees - PlayerRotation) * Mathf.Deg2Rad)) / 3,
				Quaternion.Euler (90,
					180 - ((int)FacingDegrees - PlayerRotation),
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
			Instantiate (selectedSpell.getSpellForm (), playerControl.getPosition () + new Vector3 (0, 1.01f, 0),
				Quaternion.Euler (90,
					FacingDegrees - PlayerRotation, 
					0));
		}

		playerControl.changeMP (-(selectedSpell.getCost ()));
		HUDControl.setManaPercentage (playerControl.getPercentMP ());

	}

	/**
	 * <para>Entering cutscene. Takes control from the player.</para>
	 * **/
	public void EnterCutscene() {
		HUDControl.Hide ();
		HaltCutsceneBehaviours ();
		InCutscene = true;
	}

	/**
	 * <para>Ending cutscene. Gives control back to the player</para>
	 * **/
	public void EndCutscene() {
		HUDControl.Show ();
		MoveCutsceneBehaviours ();
		InCutscene = false;
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
				if (!InCutscene) {
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
				if (!InCutscene) {
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
	 * <para>Restores the player's HP by the percentage</para>
	 * <param name="healPercent">Percentage to heal</param>
	 * **/
	public void HealPlayerHealthPercent (float healPercent) {
		playerControl.PercentChangeHP (healPercent);

		HUDControl.setHealthPercentage (playerControl.getPercentHP ());
	}

	/**
	 * Restores the player's MP by the percentage
	 * **/
	public void HealPlayerManaPercent (float healPercent) {
		playerControl.PercentChangeMP (healPercent);

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
						HealPlayerHealthPercent (value);
						break;
				case "Mana":
						HealPlayerManaPercent (value);
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
		freezingList.Add ((StopOnFreeze)FreezeObject);
		}

	/**
	 * <para>Adds the object to the StopOnCutscene list</para>
	 * <param name="CutsceneObject">The MonoBehaviour to add</param>
	 * **/
	public void AddStopOnCutscene(MonoBehaviour CutsceneObject) {
		cutsceneList.Add ((StopOnCutscene)CutsceneObject);
	}

	/**
	 * <para>Stops all objects that won't move when told not to</para>
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

	/**
	 * <para>Stops all objects that won't move during a cutscene</para>
	 * **/
	private void HaltCutsceneBehaviours () {
		foreach (StopOnCutscene SoC in cutsceneList) {
			SoC.CutsceneFreeze ();
		}
	}

	/**
	 * <para>Resumes movement on all objects that shouldn't move during a cutscene</para>
	 * **/
	private void MoveCutsceneBehaviours () {
		foreach (StopOnCutscene SoC in freezingList) {
			SoC.CutsceneMove ();
		}
	}

	/**
	 * <para>Returns whether the game is in a cutscene or not</para>
	 * <returns>True if in cutscene, false otherwise</returns>
	 * **/
	public bool CutsceneActive () {
		return InCutscene;
	}

	/**
	 * <para>Gets a list of known spells</para>
	 * <returns>A List of known spells</para>
	 * **/
	public List<Spell> GetKnownSpells () {
		return KnownSpells;
	}

	/**
	 * <para>Removes a known spell.</para>
	 * <param name="TargetSpell">Spell to remove</param>
	 * **/
	public void RemoveSpell (SPELL TargetSpell) {
		KnownSpells.Remove (KnownSpells.Find (x => x.getNumber () == (int)TargetSpell));
		if (KnownSpells.Count > 0) {
			selectedSpell = KnownSpells [spellIndex];
			HUDControl.setIcon (selectedSpell, MagnetPole);
		}
	}

	/**
	 * <para>Adds a given spell to the known list. List is kept ordered</para>
	 * <param name="newSpell">Spell to add</param>
	 * **/
	public void AddSpell (SPELL newSpell){
		// Not great, but the best I can do. Find is a slow function.
		if (KnownSpells.Find (x => x.getNumber () == (int)newSpell) == null) {
			Spell TargetSpell;

			switch (newSpell) {
			case SPELL.Fire:
				TargetSpell = new FireSpell ();
				break;
			case SPELL.Fire2:
				TargetSpell = new Fire2Spell ();
				break;
			case SPELL.Fire3:
				TargetSpell = new Fire3Spell ();
				break;
			case SPELL.FireEx:
				TargetSpell = new FireExSpell ();
				break;
			case SPELL.Ice:
				TargetSpell = new IceSpell ();
				break;
			case SPELL.Ice2:
				TargetSpell = new Ice2Spell ();
				break;
			case SPELL.Ice3:
				TargetSpell = new Ice3Spell ();
				break;
			case SPELL.IceEx:
				TargetSpell = new IceExSpell ();
				break;
			case SPELL.Lightning:
				TargetSpell = new LightningSpell ();
				break;
			case SPELL.Lightning2:
				TargetSpell = new Lightning2Spell ();
				break;
			case SPELL.Lightning3:
				TargetSpell = new Lightning3Spell ();
				break;
			case SPELL.LightningEx:
				TargetSpell = new LightningExSpell ();
				break;
			case SPELL.Heal:
				TargetSpell = new HealSpell ();
				break;
			case SPELL.Heal2:
				TargetSpell = new Heal2Spell ();
				break;
			case SPELL.Heal3:
				TargetSpell = new Heal3Spell ();
				break;
			case SPELL.HealEx:
				TargetSpell = new HealExSpell ();
				break;
			case SPELL.Wind:
				TargetSpell = new WindSpell ();
				break;
			case SPELL.MagnetNorth:
				TargetSpell = new MagnetSpell ();
				break;
			case SPELL.Mirror:
				TargetSpell = new MirrorSpell ();
				break;
			case SPELL.Mirror2:
				TargetSpell = new Mirror2Spell ();
				break;
			case SPELL.Mirror3:
				TargetSpell = new Mirror3Spell ();
				break;
			case SPELL.MirrorEx:
				TargetSpell = new MirrorExSpell ();
				break;
			/*case SPELL.Death:
				TargetSpell = new DeathSpell ();
				break;
			case SPELL.Illuminate:
				TargetSpell = new IlluminateSpell ();
				break;*/
			default:
				TargetSpell = new FireSpell ();
				Debug.Log ("Adding a spell that doesn't exist yet! Attempted spell number: " + newSpell);
				break;
			}
			TargetSpell.setSpellForm (spellBook.getPrefab (TargetSpell));
			for (int i = 0; i < KnownSpells.Count; ++i) {
				if (KnownSpells [i].getNumber () > (int)newSpell) {
					KnownSpells.Insert (i, TargetSpell);
					return;
				}
			}
			//No spell exists with a lower number, so...
			KnownSpells.Add(TargetSpell);
		}
	}
}