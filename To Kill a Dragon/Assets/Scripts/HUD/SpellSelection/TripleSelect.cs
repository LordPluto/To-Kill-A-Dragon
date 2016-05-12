using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TripleSelect : MonoBehaviour {

	public SpellNumber SpellQ;
	public SpellNumber SpellE;
	public SpellNumber SpellSpace;

	private GameController gameControl;
	private QuickSelectController selectControl;

	public Image IconQ;
	public Image IconE;
	public Image IconSpace;

	// Use this for initialization
	void Start () {
		gameControl = GameObject.Find ("_GameController").GetComponent<GameController> ();
		selectControl = GetComponentInParent<QuickSelectController> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/**
	 * <para>Set the game controller's selections</para>
	 * */
	public void EquipSelections () {
		gameControl.SetSpell (SpellCast.Q, SpellQ);
		gameControl.SetSpell (SpellCast.E, SpellE);
		gameControl.SetSpell (SpellCast.Space, SpellSpace);
	}

	/**
	 * <para>Gets the game controller's selection of the associated spell ID</para>
	 * <param name="spellId">Location of the spell</param>
	 * <returns>The Spell chosen</returns>
	 * */
	public SpellNumber GetSelection (SpellCast spellId) {
		switch (spellId) {
		case SpellCast.Q:
			return SpellQ;
		case SpellCast.E:
			return SpellE;
		case SpellCast.Space:
			return SpellSpace;
		}
		return SpellQ;
	}

	/**
	 * <para>Sets the Spell number of the associated spell ID</para>
	 * <param name="spellId">Location of the spell</param>
	 * <param name="s">Spell number</param>
	 * **/
	public void SetSelection(SpellCast spellId, SpellNumber s) {
		switch (spellId) {
		case SpellCast.Q:
			SpellQ = s;
			IconQ.sprite = selectControl.GetSprite (s);
			break;
		case SpellCast.E:
			SpellE = s;
			IconE.sprite = selectControl.GetSprite (s);
			break;
		case SpellCast.Space:
			SpellSpace = s;
			IconSpace.sprite = selectControl.GetSprite (s);
			break;
		}
	}

	/**
	 * <para>Gets the image associated with the spell location</para>
	 * <param name="spellId">Location of the spell</param>
	 * **/
	public Image GetIcon (SpellCast spellId) {
		switch (spellId) {
		case SpellCast.Q:
			return IconQ;
		case SpellCast.E:
			return IconE;
		case SpellCast.Space:
			return IconSpace;
		}
		return IconQ;
	}
}
