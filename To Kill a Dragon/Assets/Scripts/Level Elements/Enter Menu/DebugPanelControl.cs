using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class DebugPanelControl : MenuPanelControl {

	private GameController gameControl;
	private Toggle[] spellToggles;

	// Use this for initialization
	void Start () {
		gameControl = GameObject.Find ("_GameController").GetComponent<GameController> ();

		spellToggles [0] = GameObject.Find ("FireToggle").GetComponent<Toggle> ();
		spellToggles [4] = GameObject.Find ("IceToggle").GetComponent<Toggle> ();
		spellToggles [8] = GameObject.Find ("LightningToggle").GetComponent<Toggle> ();
		spellToggles [12] = GameObject.Find ("HealToggle").GetComponent<Toggle> ();
		spellToggles [16] = GameObject.Find ("WindToggle").GetComponent<Toggle> ();
		spellToggles [17] = GameObject.Find ("MagnetToggle").GetComponent<Toggle> ();
	}

	void Awake () {
		spellToggles = new Toggle[25];
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void OnActivate () {
		if (gameControl == null) {
			gameControl = GameObject.Find ("_GameController").GetComponent<GameController> ();
		}

		List<Spell> KnownSpells = gameControl.GetKnownSpells ();
		DisableToggles ();

		foreach (Spell spell in KnownSpells) {
			spellToggles [spell.getNumber ()].isOn = true;
		}
	}

	public override void Init () {
		gameControl = GameObject.Find ("_GameController").GetComponent<GameController> ();

		spellToggles [0] = GameObject.Find ("FireToggle").GetComponent<Toggle> ();
		spellToggles [4] = GameObject.Find ("IceToggle").GetComponent<Toggle> ();
		spellToggles [8] = GameObject.Find ("LightningToggle").GetComponent<Toggle> ();
		spellToggles [12] = GameObject.Find ("HealToggle").GetComponent<Toggle> ();
		spellToggles [16] = GameObject.Find ("WindToggle").GetComponent<Toggle> ();
		spellToggles [17] = GameObject.Find ("MagnetToggle").GetComponent<Toggle> ();
	}

	/**
	 * <para>Turns off all spell toggles.</para>
	 * **/
	void DisableToggles () {
		foreach (Toggle t in spellToggles) {
			if (t != null) {
				t.isOn = false;
			}
		}
	}

	/**
	 * <param name="ToggleState">Toggle state - if true add, if false remove</param>
	 * **/
	public void ToggleFireSpell (bool ToggleState) {
		if (ToggleState) {
			gameControl.AddSpell (SPELL.Fire);
		} else {
			gameControl.RemoveSpell (SPELL.Fire);
		}
	}
	/**
	 * <param name="ToggleState">Toggle state - if true add, if false remove</param>
	 * **/
	public void ToggleIceSpell (bool ToggleState) {
		if (ToggleState) {
			gameControl.AddSpell (SPELL.Ice);
		} else {
			gameControl.RemoveSpell (SPELL.Ice);
		}
	}
	/**
	 * <param name="ToggleState">Toggle state - if true add, if false remove</param>
	 * **/
	public void ToggleLightningSpell (bool ToggleState) {
		if (ToggleState) {
			gameControl.AddSpell (SPELL.Lightning);
		} else {
			gameControl.RemoveSpell (SPELL.Lightning);
		}
	}
	/**
	 * <param name="ToggleState">Toggle state - if true add, if false remove</param>
	 * **/
	public void ToggleHealSpell (bool ToggleState) {
		if (ToggleState) {
			gameControl.AddSpell (SPELL.Heal);
		} else {
			gameControl.RemoveSpell (SPELL.Heal);
		}
	}
	/**
	 * <param name="ToggleState">Toggle state - if true add, if false remove</param>
	 * **/
	public void ToggleWindSpell (bool ToggleState) {
		if (ToggleState) {
			gameControl.AddSpell (SPELL.Wind);
		} else {
			gameControl.RemoveSpell (SPELL.Wind);
		}
	}

	/**
	 * <param name="ToggleState">Toggle state - if true add, if false remove</param>
	 * **/
	public void ToggleMagnetSpell (bool ToggleState) {
		if (ToggleState) {
			gameControl.AddSpell (SPELL.MagnetNorth);
		} else {
			gameControl.RemoveSpell (SPELL.MagnetNorth);
		}
	}

}
