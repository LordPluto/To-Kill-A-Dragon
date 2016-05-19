using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class StatusQuickControl : MonoBehaviour {

	private GameController gameControl;
	private StatusTriadPanelControl panelControl;
	private StatusQuickTriad currentTriad;

	public GameObject[] unlockedSlots;
	public StatusQuickTriad[] Triads;
	public int numUnlocked=1;

	// Use this for initialization
	void Start () {
		gameControl = GameObject.Find ("_GameController").GetComponent<GameController> ();
	}

	void Awake () {
		panelControl = GetComponentInChildren<StatusTriadPanelControl> ();
	}

	void OnEnable () {
		if (gameControl == null) {
			gameControl = GameObject.Find ("_GameController").GetComponent<GameController> ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Q)) {
			Equip (SpellCast.Q);
		} else if (Input.GetKeyDown (KeyCode.E)) {
			Equip (SpellCast.E);
		} else if (Input.GetKeyDown (KeyCode.Space)) {
			Equip (SpellCast.Space);
		}
	}

	/**
	 * <para>Transfers control over to the spell list for selection</para>
	 * **/
	private void Equip(SpellCast spellId) {
		currentTriad = Triads [(int)spellId];
		panelControl.Enable ();
		this.enabled = false;
	}

	public void TakeControl (SpellCast spellId, SpellNumber newSelection) {
		this.enabled = true;
		currentTriad.SetSelector (spellId, newSelection);
	}

	/**
	 * <para>Unlocks the next slot and fills it with the selected spell</para>
	 * <param name="spell">Spell number to unlock and fill with</param>
	 * **/
	public void UnlockNextSlot (SpellNumber spell) {
		if (numUnlocked >= unlockedSlots.Length)
			return;

		panelControl.UnlockSpell (spell);
		StatusQuickTriad triad = Triads[numUnlocked % 3];
		if (numUnlocked / 3 == 0) {
			triad.SetSelector (SpellCast.Q, spell);
		} else if (numUnlocked / 3 == 1) {
			triad.SetSelector (SpellCast.E, spell);
		} else {
			triad.SetSelector (SpellCast.Space, spell);
		}
		++numUnlocked;
	}
}
