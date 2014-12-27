using UnityEngine;
using System.Collections;

public class HUDController : MonoBehaviour {

	private HUDSpellController spells;
	private HUDHeadController heads;
	private HUDManaController mana;
	private HUDHealthController health;

	void Start () {
		}

	void Awake () {
				spells = GameObject.Find ("HUD Spells").GetComponent<HUDSpellController> ();
				heads = GameObject.Find ("HUD Head").GetComponent<HUDHeadController> ();
				mana = GameObject.Find ("HUD MP Bar").GetComponent<HUDManaController> ();
				health = GameObject.Find ("HUD HP Bar").GetComponent<HUDHealthController> ();
		}

	void Update () {
		}

	/**
	 * Sets the health's new percentage
	 * **/
	public void setHealthPercentage(float percent){
				if (percent > 100) {
						health.setPercent (100);
				} else if (percent < 0) {
						health.setPercent (0);
				} else {
						health.setPercent (percent);
				}
		}

	/**
	 * Sets the mana's new percentage
	 * **/
	public void setMana(float percent){
		if (percent > 100) {
			mana.setPercent (100);
		} else if (percent < 0) {
			mana.setPercent (0);
		} else {
			mana.setPercent (percent);
		}
	}

	/**
	 * Sets the spell's icon
	 * **/
	public void setIcon(Spell selectedSpell){
				spells.SetTexture (selectedSpell.getNumber());
		}
}
