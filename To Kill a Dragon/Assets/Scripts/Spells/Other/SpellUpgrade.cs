using UnityEngine;
using System.Collections;

public class SpellUpgrade : MonoBehaviour {

	public Transform UpgradeForm;
	public int UpgradeNum;
	private GameController gameControl;

	enum UpgradeEquivalent{
		Fire2 = 0,
		Fire3 = 1,
		FireEx = 2,
		Ice2 = 3,
		Ice3 = 4,
		IceEx = 5,
		Lightning2 = 6,
		Lightning3 = 7,
		LightningEx = 8,
		Heal2 = 9,
		Heal3 = 10,
		HealEx = 11
	}

	// Use this for initialization
	void Start () {
		gameControl = GameObject.Find ("_GameController").GetComponent<GameController> ();
	}
	
	// Update is called once per frame
	void Update () {
		}

	public void OnTriggerEnter(Collider c){
				if (c.CompareTag ("Player")) {
			switch(UpgradeNum){
			case 0:
				gameControl.SpellUpgrade (UpgradeForm, new Fire2Spell());
				break;
			case 1:
				gameControl.SpellUpgrade (UpgradeForm, new Fire3Spell());
				break;
			case 2:
				gameControl.SpellUpgrade (UpgradeForm, new FireExSpell());
				break;
			case 3:
				gameControl.SpellUpgrade (UpgradeForm, new Ice2Spell());
				break;
			case 4:
				gameControl.SpellUpgrade (UpgradeForm, new Ice3Spell());
				break;
			case 5:
				gameControl.SpellUpgrade (UpgradeForm, new IceExSpell());
				break;
			case 6:
				gameControl.SpellUpgrade (UpgradeForm, new Lightning2Spell());
				break;
			case 7:
				gameControl.SpellUpgrade (UpgradeForm, new Lightning3Spell());
				break;
			case 8:
				gameControl.SpellUpgrade (UpgradeForm, new LightningExSpell());
				break;
			case 9:
				gameControl.SpellUpgrade (UpgradeForm, new Heal2Spell());
				break;
			case 10:
				gameControl.SpellUpgrade (UpgradeForm, new Heal3Spell());
				break;
			case 11:
				gameControl.SpellUpgrade (UpgradeForm, new HealExSpell());
				break;
				}
		}
	}
}
