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
		gameControl.SetSpellQ (SpellQ);
		gameControl.SetSpellE (SpellE);
		gameControl.SetSpellSpace (SpellSpace);
	}

	/**
	 * <para>Sets the Q spell number</para>
	 * <param name="q">Spell number</param>
	 * **/
	public void SetQNumber(SpellNumber q) {
		SpellQ = q;
		IconQ.sprite = selectControl.GetSprite (q);
	}

	/**
	 * <para>Sets the E spell number</para>
	 * <param name="e">Spell number</param>
	 * **/
	public void SetENumber(SpellNumber e) {
		SpellE = e;
		IconE.sprite = selectControl.GetSprite (e);
	}

	/**
	 * <para>Sets the Space spell number</para>
	 * <param name="space">Spell number</param>
	 * **/
	public void SetSpaceNumber(SpellNumber space) {
		SpellSpace = space;
		IconSpace.sprite = selectControl.GetSprite (space);
	}
}
