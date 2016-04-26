using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class StatusWheelControl : MonoBehaviour {

	private GameController gameControl;
	private StatusWheelIcon currentSelection;
	private StatusWheelPanelControl panelControl;

	public StatusWheelIcon[] unlockedSlots;
	public int numUnlocked=1;

	// Use this for initialization
	void Start () {
		gameControl = GameObject.Find ("_GameController").GetComponent<GameController> ();
	}

	void Awake () {
		panelControl = GetComponentInChildren<StatusWheelPanelControl> ();
	}

	void OnEnable () {
		if (gameControl == null) {
			gameControl = GameObject.Find ("_GameController").GetComponent<GameController> ();
		}
		for (int i = 0; i < numUnlocked; ++i) {
			unlockedSlots[i].gameObject.SetActive (true);
		}
		for (int j = numUnlocked; j < unlockedSlots.Length; ++j) {
			unlockedSlots [j].gameObject.SetActive (false);
		}

		if (currentSelection == null) {
			currentSelection = GetComponentInChildren<StatusWheelIcon> ();
		}

		if (currentSelection != null) {
			EventSystem.current.SetSelectedGameObject (currentSelection.gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Q)
			|| Input.GetKeyDown (KeyCode.E)
			|| Input.GetKeyDown (KeyCode.Space)) {
			Equip ();
		}

		if (EventSystem.current.currentSelectedGameObject == null && currentSelection != null) {
			EventSystem.current.SetSelectedGameObject (currentSelection.gameObject);
		}
	}

	/**
	 * <para>Transfers control over to the spell list for selection</para>
	 * **/
	private void Equip() {
		panelControl.Enable (currentSelection);
		this.enabled = false;
	}

	public void TakeControl (StatusWheelSelector newSelection) {
		this.enabled = true;
		currentSelection.SetSelector (newSelection.spellNumber);
		currentSelection.partnerSelector = newSelection.partnerSelector;
		EventSystem.current.SetSelectedGameObject (currentSelection.gameObject);
	}

	/**
	 * <para>Sets the new selection</para>
	 * <param name="newSelection">New selected spell</param>
	 * **/
	public void SetSelected (StatusWheelIcon newSelection) {
		currentSelection = newSelection;
	}

	/**
	 * <para>Unlocks the next slot and fills it with the selected spell</para>
	 * <param name="spell">Spell number to unlock and fill with</param>
	 * **/
	public void UnlockNextSlot (SpellNumber spell) {
		if (numUnlocked >= unlockedSlots.Length)
			return;

		unlockedSlots [numUnlocked].gameObject.SetActive (true);
		StatusWheelSelector newSpell = panelControl.UnlockSpell (spell);
		unlockedSlots[numUnlocked].SetSelector(newSpell.spellNumber);
		unlockedSlots[numUnlocked++].partnerSelector = newSpell.partnerSelector;
	}
}
