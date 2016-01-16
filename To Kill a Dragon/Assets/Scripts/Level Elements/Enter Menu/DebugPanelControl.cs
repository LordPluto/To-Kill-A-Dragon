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
			
		} else {
			
		}
	}
	/**
	 * <param name="ToggleState">Toggle state - if true add, if false remove</param>
	 * **/
	public void ToggleIceSpell (bool ToggleState) {
		if (ToggleState) {
			
		} else {
			
		}
	}
	/**
	 * <param name="ToggleState">Toggle state - if true add, if false remove</param>
	 * **/
	public void ToggleLightningSpell (bool ToggleState) {
		if (ToggleState) {

		} else {

		}
	}
	/**
	 * <param name="ToggleState">Toggle state - if true add, if false remove</param>
	 * **/
	public void ToggleHealSpell (bool ToggleState) {
		if (ToggleState) {
			
		} else {

		}
	}
	/**
	 * <param name="ToggleState">Toggle state - if true add, if false remove</param>
	 * **/
	public void ToggleWindSpell (bool ToggleState) {
		if (ToggleState) {

		} else {

		}
	}

	/**
	 * <param name="ToggleState">Toggle state - if true add, if false remove</param>
	 * **/
	public void ToggleMagnetSpell (bool ToggleState) {
		if (ToggleState) {

		} else {

		}
	}

}
