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
				EquipQ (currentSelection.GetSelection());
			} else if (Input.GetKeyDown (KeyCode.E)) {
				EquipE (currentSelection.GetSelection());
			} else if (Input.GetKeyDown (KeyCode.Space)) {
				EquipSpace (currentSelection.GetSelection());
			}
		}
	}

	void ToggleCanvas () {
		parentCanvas.enabled = !parentCanvas.enabled;
		IsActive = parentCanvas.enabled;
		if (currentSelection == null) {
			currentSelection = GetComponentInChildren<SingleSelect> ();
		}

		EventSystem.current.SetSelectedGameObject(currentSelection.gameObject);

		gameControl.SwitchingSpells (SpellSelectStyle.Wheel, IsActive);
	}

	/**
	 * <para>Equips the selected spell in the Q slot</para>
	 * <param name="selection">Spell selected</param>
	 * **/
	private void EquipQ (SpellNumber selection) {
		gameControl.SetSpellQ (selection);
		IconQ.sprite = imageControl.Spells [(int)selection];
	}

	/**
	 * <para>Equips the selected spell in the E slot</para>
	 * <param name="selection">Spell selected</param>
	 * **/
	private void EquipE (SpellNumber selection) {
		gameControl.SetSpellE (selection);
		IconE.sprite = imageControl.Spells [(int)selection];
	}

	/**
	 * <para>Equips the selected spell in the Space slot</para>
	 * <param name="selection">Spell selected</param>
	 * **/
	private void EquipSpace (SpellNumber selection) {
		gameControl.SetSpellSpace (selection);
		IconSpace.sprite = imageControl.Spells [(int)selection];
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
