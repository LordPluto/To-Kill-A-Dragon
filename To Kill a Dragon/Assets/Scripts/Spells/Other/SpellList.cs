using UnityEngine;
using System.Collections;

public enum SPELL {
	Fire = 0,
	Fire2 = 1,
	Fire3 = 2,
	FireEx = 3,
	Ice = 4,
	Ice2 = 5,
	Ice3 = 6,
	IceEx = 7,
	Lightning = 8,
	Lightning2 = 9,
	Lightning3 = 10,
	LightningEX = 11,
	Heal = 12,
	Heal2 = 13,
	Heal3 = 14,
	HealEx = 15,
	Wind = 16,
	MagnetNorth = 17,
	Mirror = 18,
	Mirror2 = 19,
	Mirror3 = 20,
	MirrorEx = 21,
	Death = 22,
	Illuminate = 23,
	MagnetSouth = 24,
}

public class SpellList : MonoBehaviour {

	public Transform Fire;
	public Transform Fire2;
	public Transform Fire3;
	public Transform FireEx;
	public Transform Ice;
	public Transform Ice2;
	public Transform Ice3;
	public Transform IceEx;
	public Transform Lightning;
	public Transform Lightning2;
	public Transform Lightning3;
	public Transform LightningEx;
	public Transform Heal;
	public Transform Heal2;
	public Transform Heal3;
	public Transform HealEx;
	public Transform Wind;
	public Transform Magnet;
	public Transform Mirror;
	public Transform Mirror2;
	public Transform Mirror3;
	public Transform MirrorEx;
	public Transform Death;
	public Transform Illuminate;

	// Use this for initialization
	void Start () {
		}

	void Awake () {
		DontDestroyOnLoad (transform.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/**
	 * Returns the prefab corresponding to the Spell.
	 * **/
	public Transform getPrefab(Spell selectedSpell){
				switch (selectedSpell.getNumber ()) {
				case 0:
						return this.Fire;
				case 1:
						return this.Fire2;
				case 2:
						return this.Fire3;
				case 3:
						return this.FireEx;
				case 4:
						return this.Ice;
				case 5:
						return this.Ice2;
				case 6:
						return this.Ice3;
				case 7:
						return this.IceEx;
				case 8:
						return this.Lightning;
				case 9:
						return this.Lightning2;
				case 10:
						return this.Lightning3;
				case 11:
						return this.LightningEx;
				case 12:
						return this.Heal;
				case 13:
						return this.Heal2;
				case 14:
						return this.Heal3;
				case 15:
						return this.HealEx;
				case 16:
						return this.Wind;
				case 17:
						return this.Magnet;
				case 18:
						return this.Mirror;
				case 19:
						return this.Mirror2;
				case 20:
						return this.Mirror3;
				case 21:
						return this.MirrorEx;
				case 22:
						return this.Death;
				case 23:
						return this.Illuminate;
				}
				return null;
		}
}
