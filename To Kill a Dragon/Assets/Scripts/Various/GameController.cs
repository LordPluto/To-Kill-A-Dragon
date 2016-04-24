using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public enum Head{
	Fine = 0,
	HPLow = 1,
	Damaged = 2,
	Dead = 3,
	MPLow = 4,
	Sleepy = 5
}

public enum SpellNumber{
	Fire = 0,
	Ice = 1,
	Lightning = 2,
	Heal = 3,
	Wind = 4,
	Magnet = 5,
	Mirror = 6,
	Death = 7,
	Illuminate = 8,
	Melee = 9
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

	private NPCController storedNPC;

	private MenuController MenuCanvas;

	#endregion

	#region Bools

	private bool InCutscene;
	private bool menuOpen;
	private bool SwitchingActive;

	#endregion

	#region Holders
	
	private List<StopOnTalk> talkingList;
	private List<StopOnFreeze> freezingList;
	private List<StopOnCutscene> cutsceneList;
	
	#endregion

	#region Game Settings

	private SpellSelectStyle currentSelectionStyle;

	#endregion

	#region Consistent Across Screens

		#region HUD

		private HUDController HudControl;
		private Head currentHead;

		private float MPTimer = 0;

		#endregion

		#region Spells

		private SpellBook spellBook;

		private SpellNumber SpellQ;
		private SpellNumber SpellE;
		private SpellNumber SpellSpace;
		private bool[] KnownSpells;

		public bool MagnetActive = false;
		private Pole MagnetPole;

		#endregion

		#region Player Wallet

		private float wallet;
		private const float WalletMax = 999999999999;

		#endregion

	#endregion

	/**
	 * Use this for initialization
	 * **/
	void Start () {
		treeControl = GameObject.Find ("DialogueTree").GetComponent<DialogueTreeController> ();
		playerControl = GameObject.Find ("Player").GetComponent<PlayerMasterController> ();
		dialogueDump = GameObject.Find ("_DialogueText").GetComponent<DialogueDump> ();
		HudControl = GameObject.Find ("HUD Canvas").GetComponent<HUDController> ();
		MenuCanvas = GameObject.Find ("Enter Menu").GetComponent<MenuController> ();
		MenuCanvas.SetEnabled (false);
		spellBook = GameObject.Find ("SpellBook").GetComponent<SpellBook> ();

		
		//Testing for Dialogue logic. MARKED FOR DELETION
		dialogueDump.AddLines ("Victor", 1, (TextAsset)Resources.Load ("Test/Chapter One"));
		SetNPCFlag ("Victor", 1);
		dialogueDump.AddLines ("Victor2", 1, (TextAsset)Resources.Load ("Test/Chapter Two"));
		SetNPCFlag ("Victor2", 1);
		dialogueDump.AddLines ("new Sarah Sprite", 1, (TextAsset)Resources.Load ("Test/Sarah Time Shenanigans"));
		SetNPCFlag ("new Sarah Sprite", 1);
		//END TEST

		//Testing for Spells. MARKED FOR DELETION
		KnownSpells[(int)SpellNumber.Fire] = true;
		KnownSpells[(int)SpellNumber.Ice] = true;
		KnownSpells[(int)SpellNumber.Lightning] = true;
		KnownSpells [(int)SpellNumber.Heal] = true;
		KnownSpells [(int)SpellNumber.Wind] = true;
		KnownSpells [(int)SpellNumber.Magnet] = true;
		SetSpellQ (SpellNumber.Fire);
		SetSpellE (SpellNumber.Ice);
		SetSpellSpace (SpellNumber.Lightning);
		//END TEST

		DontDestroyOnLoad (GameObject.Find ("EventSystem"));

		InvokeRepeating ("UpdateHead", 0.1f, 0.5f);
	}

	void Awake () {
		Cursor.visible = false;
		//Screen.SetResolution (1280, 720, false);
		
		currentHead = Head.Fine;
		menuOpen = false;
		SwitchingActive = false;
		InCutscene = false;
		
		wallet = 0;
		
		characterFlags = new Dictionary<string, int> ();
		characterLines = new Dictionary<int, TextAsset> ();

		talkingList = new List<StopOnTalk> ();
		freezingList = new List<StopOnFreeze> ();
		cutsceneList = new List<StopOnCutscene> ();

		KnownSpells = new bool[10];
		KnownSpells [(int)SpellNumber.Melee] = true;
		MagnetPole = Pole.North;

		DontDestroyOnLoad (transform.gameObject);

		currentSelectionStyle = SpellSelectStyle.Wheel;
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

		InvokeRepeating ("UpdateHead", 0.1f, 0.5f);
	}
	
	// Update is called once per frame
	void Update () {
	}

	void UpdateHead () {
		if (!(currentHead == Head.Damaged)) {
			currentHead = SelectHead ();
			HudControl.UpdateHead (currentHead);
		} else if (!playerControl.isFlinching ()) {
			currentHead = SelectHead ();
			HudControl.UpdateHead (currentHead);
		}
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
	 * <para>Entering cutscene. Takes control from the player.</para>
	 * **/
	public void EnterCutscene() {
		HaltCutsceneBehaviours ();
		InCutscene = true;
	}

	/**
	 * <para>Ending cutscene. Gives control back to the player</para>
	 * **/
	public void EndCutscene() {
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
			HudControl.UpdateHealthBar (playerControl.getPercentHP () / 100);

			monsterControl.FlinchBack (-monsterAngle);

			currentHead = Head.Damaged;
			HudControl.UpdateHead (currentHead);
		}
	}

	/**
	 * Destroys the enemy and gives the EXP to the player
	 * **/
	public void DestroyMonster(GameObject monster){
		BasicEnemyController monsterControl = monster.GetComponent<BasicEnemyController> ();
		monsterControl.DropItems ();

		playerControl.increaseEXP (monsterControl.valueEXP ());
		HudControl.UpdateExpBar (playerControl.getPercentEXP () / 100);
		Destroy (monster);
	}

	/**
	 * Deals the player damage from getting hit by a bullet
	 * **/
	public void DealPlayerBulletDamage(float damage, Vector3 bulletDirection){
		if (!InCutscene) {
			playerControl.TakeMonsterDamage (damage, bulletDirection);
			HudControl.UpdateHealthBar (playerControl.getPercentHP () / 100);

			currentHead = Head.Damaged;
			HudControl.UpdateHead (currentHead);
		}
	}

	/**
	 * Heal the player the amount
	 * **/
	public void HealPlayer(float healAmount) {
				playerControl.changeHP (healAmount);

		HudControl.UpdateHealthBar (playerControl.getPercentHP () / 100);
		}

	/**
	 * <para>Restores the player's HP by the percentage</para>
	 * <param name="healPercent">Percentage to heal. Can be from (0, 1]</param>
	 * **/
	public void HealPlayerHealthPercent (float healPercent) {
		playerControl.PercentChangeHP (healPercent);
		HudControl.UpdateHealthBar (playerControl.getPercentHP () / 100);
	}

	/**
	 * Restores the player's MP by the percentage
	 * **/
	public void HealPlayerManaPercent (float healPercent) {
		playerControl.PercentChangeMP (healPercent);
		HudControl.UpdateManaBar (playerControl.getPercentMP () / 100);
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
				HudControl.UpdateMoney (wallet);
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
	 * <para>Opens/closes the menu</para>
	 * **/
	public void MenuInput() {
		if (IsSwitchingActive ()) {
			return;
		}
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
	 * <para>Casts the spell on the Q slot</para>
	 * <param name="_characterFacing">The direction the character is facing.</param>
	 * **/
	public void CastSpellQ(Facing _characterFacing) {
		Transform SpellCast = spellBook.GetSpellTransform (SpellQ);
		Spell SpellDetails = SpellCast.GetComponent<Spell> ();

		if (playerControl.getMP () < SpellDetails.SpellCost) {
			MPTimer = 1;
			return;
		} else {
			float FacingDegrees = (int)_characterFacing * 90;
			float PlayerRotation = playerControl.GetCameraRotation ();
			Vector3 SpellRotation = SpellCast.rotation.eulerAngles - new Vector3 (0, FacingDegrees - PlayerRotation, 0);

			Instantiate (SpellCast,
				playerControl.getPosition () + (SpellDetails is SelfSpell ? 
					Vector3.zero : new Vector3 (Mathf.Sin ((FacingDegrees - PlayerRotation) * Mathf.Deg2Rad),
					2,
					-Mathf.Cos ((FacingDegrees - PlayerRotation) * Mathf.Deg2Rad)) / 3),
				Quaternion.Euler (SpellRotation));

			playerControl.changeMP (-SpellDetails.SpellCost);
			HudControl.UpdateManaBar (playerControl.getPercentMP () / 100);

			playerControl.SetSpellTimer (SpellDetails.CastDuration);
		}
	}

	/**
	 * <para>Casts the spell on the E slot</para>
	 * <param name="_characterFacing">The direction the character is facing.</param>
	 * **/
	public void CastSpellE(Facing _characterFacing) {
		Transform SpellCast = spellBook.GetSpellTransform (SpellE);
		Spell SpellDetails = SpellCast.GetComponent<Spell> ();

		if (playerControl.getMP () < SpellDetails.SpellCost) {
			MPTimer = 1;
			return;
		} else {
			float FacingDegrees = (int)_characterFacing * 90;
			float PlayerRotation = playerControl.GetCameraRotation ();
			Vector3 SpellRotation = SpellCast.rotation.eulerAngles - new Vector3 (0, FacingDegrees - PlayerRotation, 0);

			Instantiate (SpellCast,
				playerControl.getPosition () + (SpellDetails is SelfSpell ? 
					Vector3.zero : new Vector3 (Mathf.Sin ((FacingDegrees - PlayerRotation) * Mathf.Deg2Rad),
						2,
						-Mathf.Cos ((FacingDegrees - PlayerRotation) * Mathf.Deg2Rad)) / 3),
				Quaternion.Euler (SpellRotation));

			playerControl.changeMP (-SpellDetails.SpellCost);
			HudControl.UpdateManaBar (playerControl.getPercentMP () / 100);

			playerControl.SetSpellTimer (SpellDetails.CastDuration);
		}
	}

	/**
	 * <para>Casts the spell on the Space slot</para>
	 * <param name="_characterFacing">The direction the character is facing.</param>
	 * **/
	public void CastSpellSpace(Facing _characterFacing) {
		Transform SpellCast = spellBook.GetSpellTransform (SpellSpace);
		Spell SpellDetails = SpellCast.GetComponent<Spell> ();

		if (playerControl.getMP () < SpellDetails.SpellCost) {
			MPTimer = 1;
			return;
		} else {
			float FacingDegrees = (int)_characterFacing * 90;
			float PlayerRotation = playerControl.GetCameraRotation ();
			Vector3 SpellRotation = SpellCast.rotation.eulerAngles - new Vector3 (0, FacingDegrees - PlayerRotation, 0);

			Instantiate (SpellCast,
				playerControl.getPosition () + (SpellDetails is SelfSpell ? 
					Vector3.zero : new Vector3 (Mathf.Sin ((FacingDegrees - PlayerRotation) * Mathf.Deg2Rad),
					2,
					-Mathf.Cos ((FacingDegrees - PlayerRotation) * Mathf.Deg2Rad)) / 3),
				Quaternion.Euler (SpellRotation));

			playerControl.changeMP (-SpellDetails.SpellCost);
			HudControl.UpdateManaBar (playerControl.getPercentMP () / 100);

			playerControl.SetSpellTimer (SpellDetails.CastDuration);
		}
	}

	/**
	 * <para>Sets the spell on the Q slot</para>
	 * <param name="spellNumber">Number of spell to assign</param>
	 * **/
	public void SetSpellQ(SpellNumber spellNumber) {
		SpellQ = spellNumber;

		HudControl.UpdateSpellQ (SpellQ, MagnetPole);
	}

	/**
	 * <para>Sets the spell on the E slot</para>
	 * <param name="spellNumber">Number of spell to assign</param>
	 * **/
	public void SetSpellE(SpellNumber spellNumber) {
		SpellE = spellNumber;

		HudControl.UpdateSpellE (SpellE, MagnetPole);
	}

	/**
	 * <para>Sets the spell on the Space slot</para>
	 * <param name="spellNumber">Number of spell to assign</param>
	 * **/
	public void SetSpellSpace(SpellNumber spellNumber) {
		SpellSpace = spellNumber;

		HudControl.UpdateSpellSpace (SpellSpace, MagnetPole);
	}

	/**
	 * <para>Gets the spell on the Q slot</para>
	 * <returns>The spell on the Q slot</returns>
	 * **/
	public Spell GetSpellQ () {
		Transform SpellCast = spellBook.GetSpellTransform (SpellQ);
		Spell SpellDetails = SpellCast.GetComponent<Spell> ();

		return SpellDetails;
	}

	/**
	 * <para>Gets the spell on the E slot</para>
	 * <returns>The spell on the E slot</returns>
	 * **/
	public Spell GetSpellE () {
		Transform SpellCast = spellBook.GetSpellTransform (SpellE);
		Spell SpellDetails = SpellCast.GetComponent<Spell> ();

		return SpellDetails;
	}

	/**
	 * <para>Gets the spell on the Space slot</para>
	 * <returns>The spell on the Space slot</returns>
	 * **/
	public Spell GetSpellSpace () {
		Transform SpellCast = spellBook.GetSpellTransform (SpellSpace);
		Spell SpellDetails = SpellCast.GetComponent<Spell> ();

		return SpellDetails;
	}

	/**
	 * <para>Toggles whether a given spell is known or not. Melee 'spell' is always known.</para>
	 * <param name="spellNumber">Number of spell to modify</param>
	 * <param name="SpellToggle">True if the spell is known, false if not</param>
	 * **/
	public void ToggleSpell (SpellNumber spellNumber, bool SpellToggle) {
		if (spellNumber == SpellNumber.Melee) {
			return;
		}

		KnownSpells [(int)spellNumber] = SpellToggle;

		if (!SpellToggle) {
			if (SpellQ == spellNumber) {
				SetSpellQ (SpellNumber.Melee);
			}
			if (SpellE == spellNumber) {
				SetSpellE (SpellNumber.Melee);
			}
			if (SpellSpace == spellNumber) {
				SetSpellSpace (SpellNumber.Melee);
			}
		}
	}

	/**
	 * <para>Returns whether a spell is known or not.</para>
	 * <param name="spell">The SpellNumber to check.</param>
	 * **/
	public bool SpellKnown (SpellNumber spell) {
		return KnownSpells [(int)spell];
	}

	/**
	 * <para>Returns whether a spell is known or not.</para>
	 * <param name="spell">The integer to check.</param>
	 * **/
	public bool SpellKnown (int spell) {
		return KnownSpells [spell];
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
	 * <para>Activates/Deactivates Magnet as necessary</para>
	 * **/
	public void SetMagnet(bool activate){
		MagnetActive = activate;
		if (!MagnetActive) {
			playerControl.MagnetToggle (false, Vector3.zero);
			MagnetPole = (MagnetPole == Pole.North ? Pole.South : Pole.North);
			SetSpellE (SpellE);
			SetSpellQ (SpellQ);
			SetSpellSpace (SpellSpace);
		}
	}

	/**
	 * <para>Activates when a magnet Pole is found</para>
	 * <param name="magnetDirection">The direction player is moving</param>
	 * **/
	public void SetMagnetDirection(Vector3 magnetDirection) {
		playerControl.MagnetToggle (true, (MagnetPole == Pole.North ? magnetDirection : -1 * magnetDirection));
	}

	/**
	 * <para>Activates when a magnet Cube is found</para>
	 * <param name="magnetCube">The cube found</param>
	 * <param name="magnetDirection">The direction the cube is moving</param>
	 * **/
	public void SetMagnetDirection(GameObject magnetCube, Vector3 magnetDirection) {
		magnetCube.GetComponent<MagnetCubeController>().MagnetToggle(true, (MagnetPole == Pole.North ? magnetDirection : -1 * magnetDirection));
		playerControl.MagnetToggle (true, Vector3.zero);
	}

	/**
	 * <para>Toggles the state of spell switching.</para>
	 * <param name="SelectState">The state of the selector</param>
	 * <param name="switchToggle">The toggle</param>
	 * **/
	public void SwitchingSpells(bool switchToggle) {
		if (currentSelectionStyle == SpellSelectStyle.Wheel) {
			Time.timeScale = (switchToggle ? 0 : 1);
		}

		playerControl.SwitchingToggle (switchToggle);

		SwitchingActive = switchToggle;
	}

	/**
	 * <para>Updates the current spell switching style.</para>
	 * <param name="currentState">The current state of spell switching</param>
	 * **/
	public void UpdateSpellSwitching(SpellSelectStyle currentState) {
		currentSelectionStyle = currentState;
	}

	/**
	 * <para>Gets the current spell switching style</para>
	 * <returns>Current spell switching style</returns>
	 * **/
	public SpellSelectStyle GetCurrentSelectStyle() {
		return currentSelectionStyle;
	}

	/**
	 * <para>Returns whether the player is switching spells or not</para>
	 * <returns>True if player is switching spells with LShift, false otherwise</returns>
	 * **/
	public bool IsSwitchingActive() {
		return SwitchingActive;
	}

	/**
	 * <para>Determines if the game should respond to player trying to switch spells.</para>
	 * <returns>True if menu is open, dialogue is active, or game is paused, false otherwise</returns>
	 * **/
	public bool IsPaused() {
		return menuOpen || CutsceneActive() || IsDialogueActive();
	}
}