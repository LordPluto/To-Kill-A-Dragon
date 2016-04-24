using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class StatusWheelSelector : Selectable {

	public SpellNumber spellNumber;
	public SingleSelect partnerSelector;
	private StatusWheelPanelControl panelControl;

	// Use this for initialization
	protected override void Awake () {
		panelControl = GetComponentInParent<StatusWheelPanelControl> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void OnSelect(BaseEventData eventData)
	{
		panelControl.SetSelected (this);
		base.OnSelect (eventData);
	}
}
