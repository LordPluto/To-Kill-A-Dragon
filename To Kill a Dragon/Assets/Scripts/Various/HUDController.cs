using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUDController : MonoBehaviour {

	private HUDBackgroundController backing;
	private HUDExpController EXP;
	private HUDHeadController heads;
	private HUDHealthController health;
	private HUDManaController mana;
	private HUDMoneyController money;
	private HUDSpellController spells;
	private Text moneyText;

	void Start () {

				backing = GetComponentInChildren<HUDBackgroundController> ();
				EXP = GetComponentInChildren<HUDExpController> ();
				heads = GetComponentInChildren<HUDHeadController> ();
				health = GetComponentInChildren<HUDHealthController> ();
				mana = GetComponentInChildren<HUDManaController> ();
				money = GetComponentInChildren<HUDMoneyController> ();
				spells = GetComponentInChildren<HUDSpellController> ();
				moneyText = GetComponentInChildren<Text> ();

		}

	void Awake () {
				
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
	public void setManaPercentage(float percent){
				if (percent > 100) {
						mana.setPercent (100);
				} else if (percent < 0) {
						mana.setPercent (0);
				} else {
						mana.setPercent (percent);
				}
		}

	/**
	 * Sets the EXP's new percentage
	 * **/
	public void setEXPPercentage(float percent){
				if (percent > 100) {
						EXP.setPercent (100);
				} else if (percent < 0) {
						EXP.setPercent (0);
				} else {
						EXP.setPercent (percent);
				}
		}

	/**
	 * Sets the spell's icon
	 * **/
	public void setIcon(Spell selectedSpell, Pole magnetPole){
				if (spells == null) {
						spells = GetComponentInChildren<HUDSpellController> ();
				}

				if (selectedSpell.getNumber () != (int)SPELL.MagnetNorth || magnetPole == Pole.North)
						spells.SetTexture (selectedSpell.getNumber ());
				else
						spells.SetTexture ((int)SPELL.MagnetSouth);
		}

	/**
	 * Hides the HUD elements
	 * **/
	public void Hide() {
				backing.Hide ();
				EXP.Hide ();
				heads.Hide ();
				health.Hide ();
				mana.Hide ();
				money.Hide ();
				spells.Hide ();
				moneyText.enabled = false;
		}

	/**
	 * Shows the HUD elements
	 * **/
	public void Show() {
				backing.Show ();
				EXP.Show ();
				heads.Show ();
				health.Show ();
				mana.Show ();
				money.Show ();
				spells.Show ();
				moneyText.enabled = true;
		}

	/**
	 * Tells the HUD Head controller to change heads.
	 * **/
	public void changeHead(Head head){
				heads.SetHead (head);
		}

	/**
	 * Set the amount of money displayed.
	 * **/
	public void setWallet(float money){
				moneyText.text = money.ToString();
		}
}
