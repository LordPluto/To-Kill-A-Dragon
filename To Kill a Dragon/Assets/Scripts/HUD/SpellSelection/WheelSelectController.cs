using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class WheelSelectController : MonoBehaviour {

	private Canvas parentCanvas;
	private HUDController imageControl;

	private bool IsActive = false;

	private GameController gameControl;

	public Image IconQ;
	public Image IconE;
	public Image IconSpace;

	private SingleSelect currentSelection;

	// Use this for initialization
	void Start () {
		imageControl = GameObject.Find ("HUD Canvas").GetComponent<HUDController> ();
		gameControl = GameObject.Find ("_GameController").GetComponent<GameController> ();
	}

	void Awake () {
		parentCanvas = GetComponentInParent<Canvas> ();
		currentSelection = null;
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.LeftShift)) {
			ToggleCanvas ();
		}

		if (IsActive) {
			if (Input.GetKeyDown (KeyCode.Q)) {
				Equip (SpellCast.Q, currentSelection.GetSelection());
			} else if (Input.GetKeyDown (KeyCode.E)) {
				Equip (SpellCast.E, currentSelection.GetSelection());
			} else if (Input.GetKeyDown (KeyCode.Space)) {
				Equip (SpellCast.Space, currentSelection.GetSelection());
			}

			if (EventSystem.current.currentSelectedGameObject == null && currentSelection != null) {
				EventSystem.current.SetSelectedGameObject (currentSelection.gameObject);
			}
		}
	}

	void ToggleCanvas () {
		if (gameControl.IsPaused ()) {
			return;
		}

		parentCanvas.enabled = !parentCanvas.enabled;
		IsActive = parentCanvas.enabled;

		foreach (SingleSelect s in GetComponentsInChildren<SingleSelect>(true)) {
			s.gameObject.SetActive (gameControl.SpellKnown (s.Spell));
		}

		if (currentSelection == null) {
			currentSelection = GetComponentInChildren<SingleSelect> ();
		}

		EventSystem.current.SetSelectedGameObject(currentSelection.gameObject);

		gameControl.SwitchingSpells (IsActive);
	}

	/**
	 * <para>Equips the selected spell in the associated spellId slot</para>
	 * <param name="spellId">Location of the spell</param>
	 * <param name="selection">Spell selected</param>
	 * **/
	private void Equip (SpellCast spellId, SpellNumber selection) {
		gameControl.SetSpell (spellId, selection);
		IconQ.sprite = imageControl.Spells [(int)selection];
	}

	public Sprite GetSprite (SpellNumber s) {
		return imageControl.Spells [(int)s];
	}

	/**
	 * <para>Sets the new selection</para>
	 * <param name="newSelection">New selected spell</param>
	 * **/
	public void SetSelected (SingleSelect newSelection) {
		currentSelection = newSelection;
	}
}
