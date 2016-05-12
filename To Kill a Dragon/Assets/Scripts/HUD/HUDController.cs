using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUDController : MonoBehaviour {

	private Image HealthBar;
	private Image ManaBar;
	private Image ExpBar;

	private Image SpellQ;
	private Image SpellE;
	private Image SpellSpace;
	public Sprite[] Spells;

	private Image HeadIcon;
	public Sprite[] Heads;

	private Text MoneyText;

	private Canvas HudCanvas;

	// Use this for initialization
	void Start () {
	
	}

	void Awake () {
		HudCanvas = GetComponent<Canvas> ();
		Image[] images = GetComponentsInChildren<Image> ();

		foreach (Image i in images) {
			if (i.gameObject.name.Equals ("HUD HP Bar")) {
				HealthBar = i;
			} else if (i.gameObject.name.Equals ("HUD MP Bar")) {
				ManaBar = i;
			} else if (i.gameObject.name.Equals ("HUD EXP")) {
				ExpBar = i;
			} else if (i.gameObject.name.Equals ("HUD Head")) {
				HeadIcon = i;
			} else if (i.gameObject.name.Equals ("HUD Spell Q")) {
				SpellQ = i;
			} else if (i.gameObject.name.Equals ("HUD Spell E")) {
				SpellE = i;
			} else if (i.gameObject.name.Equals ("HUD Spell Space")) {
				SpellSpace = i;
			}
		}

		MoneyText = GetComponentInChildren<Text> ();

		DontDestroyOnLoad (gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Enable() {
		HudCanvas.enabled = true;
	}

	public void Disable() {
		HudCanvas.enabled = false;
	}

	/**
	 * <para>Updates the health bar percentage</para>
	 * <param name="percentage">Percentage of HP remaining</param>
	 * **/
	public void UpdateHealthBar (float percentage) {
		HealthBar.fillAmount = percentage;
	}

	/**
	 * <para>Updates the mana bar percentage</para>
	 * <param name="percentage">Percentage of MP remaining</param>
	 * **/
	public void UpdateManaBar (float percentage) {
		ManaBar.fillAmount = percentage;
	}

	/**
	 * <para>Updates the EXP percentage</para>
	 * <param name="percentage">Percentage of EXP until level up</param>
	 * **/
	public void UpdateExpBar (float percentage) {
		ExpBar.fillAmount = percentage;
	}

	/**
	 * <para>Updates the Head icon</para>
	 * <param name="NewHead">The new head</param>
	 * **/
	public void UpdateHead (Head NewHead) {
		HeadIcon.sprite = Heads [(int)NewHead];
	}

	/**
	 * <para>Updates the money counter</para>
	 * <param name="NewAmount">The new amount</param>
	 * **/
	public void UpdateMoney (float NewAmount) {
		MoneyText.text = NewAmount.ToString();
	}

	/**
	 * <para>Toggles all spell icons</para>
	 * <param name="iconToggle">Toggles the icons on or off</param>
	 * **/
	public void ToggleSpellIcons (bool iconToggle) {
		SpellQ.enabled = iconToggle;
		SpellE.enabled = iconToggle;
		SpellSpace.enabled = iconToggle;
	}

	/**
	 * <para>Toggles the associated spell icon at the location.</para>
	 * <param name="spellId">Location of the spell</param>
	 * <param name="iconToggle">Toggles the icons on or off</param>
	 * **/
	public void ToggleSpellIcon (SpellCast spellId, bool iconToggle) {
		switch (spellId) {
		case SpellCast.Q:
			SpellQ.enabled = iconToggle;
			break;
		case SpellCast.E:
			SpellE.enabled = iconToggle;
			break;
		case SpellCast.Space:
			SpellSpace.enabled = iconToggle;
			break;
		}
	}

	/**
	 * <para>Updates the associated Spell icon</para>
	 * <param name="spellId">Location of the associated spell</param>
	 * <param name="NewSpell">The new spell</param>
	 * <param name="MagnetPole">The pole for magnet spell. Default is North</param>
	 * **/
	public void UpdateSpell (SpellCast spellId, SpellNumber NewSpell, Pole MagnetPole = Pole.North) {
		ToggleSpellIcon (spellId, true);
		switch (spellId) {
		case SpellCast.Q:
			if (NewSpell == SpellNumber.Magnet && MagnetPole == Pole.South) {
				SpellQ.sprite = Spells [Spells.Length - 1];
			} else {
				SpellQ.sprite = Spells [(int)NewSpell];
			}
			break;
		case SpellCast.E:
			if (NewSpell == SpellNumber.Magnet && MagnetPole == Pole.South) {
				SpellE.sprite = Spells [Spells.Length - 1];
			} else {
				SpellE.sprite = Spells [(int)NewSpell];
			}
			break;
		case SpellCast.Space:
			if (NewSpell == SpellNumber.Magnet && MagnetPole == Pole.South) {
				SpellSpace.sprite = Spells [Spells.Length - 1];
			} else {
				SpellSpace.sprite = Spells [(int)NewSpell];
			}
			break;
		}
	}
}
