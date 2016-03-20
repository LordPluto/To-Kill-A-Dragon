using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class SingleSelect : Selectable {

	public SpellNumber Spell;

	private WheelSelectController selectControl;

	private Image Icon;

	// Use this for initialization
	protected override void Start () {
		selectControl = GetComponentInParent<WheelSelectController> ();
	}

	protected override void Awake () {
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

	public override void OnSelect(BaseEventData eventData)
	{
		selectControl.SetSelected (this);
		base.OnSelect (eventData);
	}

	public override void OnPointerUp (PointerEventData eventData) 
	{
		Debug.Log (gameObject.name + " says the mouse click was released");
	}
}
