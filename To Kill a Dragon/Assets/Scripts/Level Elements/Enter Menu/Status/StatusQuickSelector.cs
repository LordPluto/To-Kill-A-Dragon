using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class StatusQuickSelector : Selectable {

	public SpellNumber spellNumber;
	private StatusTriadPanelControl panelControl;

	// Use this for initialization
	protected override void Awake () {
		panelControl = GetComponentInParent<StatusTriadPanelControl> ();
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
