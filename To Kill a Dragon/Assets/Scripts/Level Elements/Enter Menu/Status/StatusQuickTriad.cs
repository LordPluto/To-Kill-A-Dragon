using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StatusQuickTriad : MonoBehaviour {

	public TripleSelect partnerSelector;
	public Image[] slots;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/**
	 * <para>Sets the spell number of the associated spell ID in the associated selector</para>
	 * <param name="spellId">Location of spell</param>
	 * <param name="spell">Spell number of new spell</param>
	 * **/
	public void SetSelector (SpellCast spellId, SpellNumber spell) {
		partnerSelector.SetSelection (spellId, spell);
		slots[(int)spellId].sprite = partnerSelector.GetIcon(spellId).sprite;
	}
}
