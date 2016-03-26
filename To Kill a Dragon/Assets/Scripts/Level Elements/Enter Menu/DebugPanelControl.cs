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
		spellToggles [1] = GameObject.Find ("IceToggle").GetComponent<Toggle> ();
		spellToggles [2] = GameObject.Find ("LightningToggle").GetComponent<Toggle> ();
		spellToggles [3] = GameObject.Find ("HealToggle").GetComponent<Toggle> ();
		spellToggles [4] = GameObject.Find ("WindToggle").GetComponent<Toggle> ();
		spellToggles [5] = GameObject.Find ("MagnetToggle").GetComponent<Toggle> ();
	}

	void Awake () {
		spellToggles = new Toggle[10];
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void OnActivate () {
		if (gameControl == null) {
			gameControl = GameObject.Find ("_GameController").GetComponent<GameController> ();
		}

		for (int i = 0; i < 10; ++i) {
			if (spellToggles [i] != null) {
				spellToggles [i].isOn = gameControl.SpellKnown (i);
			}
		}
	}

	public override void Init () {
		gameControl = GameObject.Find ("_GameController").GetComponent<GameController> ();

		spellToggles [0] = GameObject.Find ("FireToggle").GetComponent<Toggle> ();
		spellToggles [1] = GameObject.Find ("IceToggle").GetComponent<Toggle> ();
		spellToggles [2] = GameObject.Find ("LightningToggle").GetComponent<Toggle> ();
		spellToggles [3] = GameObject.Find ("HealToggle").GetComponent<Toggle> ();
		spellToggles [4] = GameObject.Find ("WindToggle").GetComponent<Toggle> ();
		spellToggles [5] = GameObject.Find ("MagnetToggle").GetComponent<Toggle> ();
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
		gameControl.ToggleSpell (SpellNumber.Fire, ToggleState);
	}
	/**
	 * <param name="ToggleState">Toggle state - if true add, if false remove</param>
	 * **/
	public void ToggleIceSpell (bool ToggleState) {
		gameControl.ToggleSpell (SpellNumber.Ice, ToggleState);
	}
	/**
	 * <param name="ToggleState">Toggle state - if true add, if false remove</param>
	 * **/
	public void ToggleLightningSpell (bool ToggleState) {
		gameControl.ToggleSpell (SpellNumber.Lightning, ToggleState);
	}
	/**
	 * <param name="ToggleState">Toggle state - if true add, if false remove</param>
	 * **/
	public void ToggleHealSpell (bool ToggleState) {
		gameControl.ToggleSpell (SpellNumber.Heal, ToggleState);
	}
	/**
	 * <param name="ToggleState">Toggle state - if true add, if false remove</param>
	 * **/
	public void ToggleWindSpell (bool ToggleState) {
		gameControl.ToggleSpell (SpellNumber.Wind, ToggleState);
	}

	/**
	 * <param name="ToggleState">Toggle state - if true add, if false remove</param>
	 * **/
	public void ToggleMagnetSpell (bool ToggleState) {
		gameControl.ToggleSpell (SpellNumber.Magnet, ToggleState);
	}

}
