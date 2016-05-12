using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class StatusTriadPanelControl : MonoBehaviour {

	private GameController gameControl;
	private StatusQuickControl quickControl;
	private StatusQuickSelector currentSelection;
	public StatusQuickSelector[] allSelections;

	// Use this for initialization
	void Start () {
		quickControl = GetComponentInParent<StatusQuickControl> ();
		gameControl = GameObject.Find ("_GameController").GetComponent<GameController> ();
	}

	void OnEnable () {
		if (quickControl == null) {
			quickControl = GetComponentInParent<StatusQuickControl> ();
		}
		if (gameControl == null) {
			gameControl = GameObject.Find ("_GameController").GetComponent<GameController> ();
		}
		foreach (StatusQuickSelector s in GetComponentsInChildren<StatusQuickSelector>(true)) {
			s.interactable = gameControl.SpellKnown (s.spellNumber);
		}

		if (currentSelection == null) {
			currentSelection = GetComponentInChildren<StatusQuickSelector> ();
		}
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Q)) {
			Select (SpellCast.Q);
		} else if (Input.GetKeyDown (KeyCode.E)) {
			Select (SpellCast.E);
		} else if (Input.GetKeyDown (KeyCode.Space)) {
			Select (SpellCast.Space);
		}
	}

	/**
	 * <para>Selects the currently selected spell and assigns to the designated slot.</para>
	 * <param name="spellId">Location of spell
	 * **/
	void Select (SpellCast spellId) {
		quickControl.TakeControl (spellId, currentSelection.spellNumber);
		EventSystem.current.SetSelectedGameObject (null);
		this.enabled = false;
	}

	/**
	 * <para>Enables this behavior and transfers control to it.</para>
	 * **/
	public void Enable () {
		this.enabled = true;
		if (currentSelection == null) {
			currentSelection = GetComponentInChildren<StatusQuickSelector> ();
		}
		EventSystem.current.SetSelectedGameObject (currentSelection.gameObject);
	}

	/**
	 * <para>Sets the new selection</para>
	 * <param name="newSelection">New selected spell</param>
	 * **/
	public void SetSelected (StatusQuickSelector newSelection) {
		currentSelection = newSelection;
	}

	/**
	 * <para>Unlocks the numbered spell selector</para>
	 * <param name="spell">Spell number to unlock</param>
	 * **/
	public void UnlockSpell (SpellNumber spell) {
		allSelections [(int)spell].interactable = true;
	}
}
