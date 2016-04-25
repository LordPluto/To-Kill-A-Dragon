using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class StatusWheelPanelControl : MonoBehaviour {

	private GameController gameControl;
	private StatusWheelControl wheelControl;
	private StatusWheelSelector currentSelection;
	public StatusWheelSelector[] allSelections;

	// Use this for initialization
	void Start () {
		wheelControl = GetComponentInParent<StatusWheelControl> ();
		gameControl = GameObject.Find ("_GameController").GetComponent<GameController> ();
	}

	void OnEnable () {
		if (wheelControl == null) {
			wheelControl = GetComponentInParent<StatusWheelControl> ();
		}
		if (gameControl == null) {
			gameControl = GameObject.Find ("_GameController").GetComponent<GameController> ();
		}
		foreach (StatusWheelSelector s in GetComponentsInChildren<StatusWheelSelector>(true)) {
			if (s.partnerSelector != null) {
				s.gameObject.SetActive (gameControl.SpellKnown (s.partnerSelector.Spell));
			}
		}

		if (currentSelection == null) {
			currentSelection = GetComponentInChildren<StatusWheelSelector> ();
		}
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Q)
			|| Input.GetKeyDown (KeyCode.E)
			|| Input.GetKeyDown (KeyCode.Space)) {
			Select ();
		}
	}

	/**
	 * <para>Selects the currently selected spell.</para>
	 * **/
	void Select () {
		wheelControl.TakeControl (currentSelection);
		this.enabled = false;
	}

	/**
	 * <para>Enables this behavior and transfers control to it.</para>
	 * **/
	public void Enable (StatusWheelIcon wheelSelect) {
		this.enabled = true;
		currentSelection = allSelections[(int)wheelSelect.partnerSelector.Spell];
		EventSystem.current.SetSelectedGameObject (currentSelection.gameObject);
	}

	/**
	 * <para>Sets the new selection</para>
	 * <param name="newSelection">New selected spell</param>
	 * **/
	public void SetSelected (StatusWheelSelector newSelection) {
		currentSelection = newSelection;
	}

	/**
	 * <para>Unlocks the numbered spell selector</para>
	 * <param name="spell">Spell number to unlock</param>
	 * **/
	public StatusWheelSelector UnlockSpell (SpellNumber spell) {
		allSelections [(int)spell].interactable = true;
		return allSelections [(int)spell];
	}
}
