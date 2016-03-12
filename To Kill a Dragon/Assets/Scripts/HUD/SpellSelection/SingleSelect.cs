using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SingleSelect : Selectable {

	public SpellNumber Spell;

	private GameController gameControl;
	private WheelSelectController selectControl;

	private Image Icon;

	// Use this for initialization
	void Start () {
		gameControl = GameObject.Find ("_GameController").GetComponent<GameController> ();
		selectControl = GetComponentInParent<WheelSelectController> ();
	}

	void Awake () {
		Icon = GetComponent<Image> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/**
	 * <para>Gets the game controller's selection</para>
	 * <returns>The Spell chosen</returns>
	 * */
	public SpellNumber GetSelection () {
		return Spell;
	}

	/**
	 * <para>Sets the Spell number</para>
	 * <param name="s">Spell number</param>
	 * **/
	public void SetSelection(SpellNumber s) {
		Spell = s;
		Icon.sprite = selectControl.GetSprite (s);
	}
}
