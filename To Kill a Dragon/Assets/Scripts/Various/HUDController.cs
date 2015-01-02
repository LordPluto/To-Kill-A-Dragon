using UnityEngine;
using System.Collections;

public class HUDController : MonoBehaviour {

	private HUDBackgroundController backing;
	private HUDDetailController details;
	private HUDExpController EXP;
	private HUDHeadController heads;
	private HUDBackController bars;
	private HUDHealthController health;
	private HUDManaController mana;
	private HUDSpellController spells;

	void Start () {
		}

	void Awake () {
				
				backing = GameObject.Find ("HUD Backing").GetComponent<HUDBackgroundController> ();
				details = GameObject.Find ("HUD Details").GetComponent<HUDDetailController> ();
				EXP = GameObject.Find ("HUD EXP").GetComponent<HUDExpController> ();
				heads = GameObject.Find ("HUD Head").GetComponent<HUDHeadController> ();
				bars = GameObject.Find ("HUD Bar Backs").GetComponent<HUDBackController> ();
				health = GameObject.Find ("HUD HP Bar").GetComponent<HUDHealthController> ();
				mana = GameObject.Find ("HUD MP Bar").GetComponent<HUDManaController> ();
				spells = GameObject.Find ("HUD Spells").GetComponent<HUDSpellController> ();
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

	/**
	 * Hides the HUD elements
	 * **/
	public void Hide() {
				backing.Hide ();
				details.Hide ();
				EXP.Hide ();
				heads.Hide ();
				bars.Hide ();
				health.Hide ();
				mana.Hide ();
				spells.Hide ();
		}

	/**
	 * Shows the HUD elements
	 * **/
	public void Show() {
				backing.Show ();
				details.Show ();
				EXP.Show ();
				heads.Show ();
				bars.Show ();
				health.Show ();
				mana.Show ();
				spells.Show ();
		}

	/**
	 * Tells the HUD Head controller to change heads.
	 * **/
	public void changeHead(Head head){
				heads.SetHead (head);
		}
}
