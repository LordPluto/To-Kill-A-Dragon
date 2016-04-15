using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class StatusWheelIcon : Selectable {

	private StatusWheelControl selectControl;
	public SingleSelect partnerSelector;

	private Image Icon;

	protected override void Start () {
	}

	protected override void Awake () {
		Icon = GetComponent<Image> ();
		selectControl = GetComponentInParent<StatusWheelControl> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetSelector (SpellNumber spell) {
		partnerSelector.SetSelection (spell);
		Icon.sprite = partnerSelector.GetComponent<Image>().sprite;
	}

	public override void OnSelect(BaseEventData eventData)
	{
		selectControl.SetSelected (this);
		base.OnSelect (eventData);
	}
}
