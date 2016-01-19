using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUDController : MonoBehaviour {

	private Image HealthBar;
	private Image ManaBar;
	private Image ExpBar;

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
			}
		}

		MoneyText = GetComponentInChildren<Text> ();
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
}
