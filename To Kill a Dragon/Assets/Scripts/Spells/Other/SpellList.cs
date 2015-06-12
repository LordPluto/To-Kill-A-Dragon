using UnityEngine;
using System.Collections;

public class SpellList : MonoBehaviour {

	public Transform Fire;
	public Transform Ice;
	public Transform Lightning;
	public Transform Heal;
	public Transform Wind;
	public Transform Magnet;
	public Transform Mirror;
	public Transform Heavy;
	public Transform Death;
	public Transform Illuminate;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/**
	 * Returns the prefab corresponding to the Spell.
	 * **/
	public Transform getPrefab(Spell selectedSpell){
				switch (selectedSpell.getNumber()) {
				case 0:
						return this.Fire;
				case 1:
						return this.Ice;
				case 2:
						return this.Lightning;
				case 3:
						return this.Heal;
				case 4:
						return this.Wind;
				case 5:
						return this.Magnet;
				case 6:
						return this.Mirror;
				case 7:
						return this.Heavy;
				case 8:
						return this.Death;
				case 9:
						return this.Illuminate;
				}
				return null;
		}

	public bool spellUpgrade(Transform newSpell, int spellNum){
				switch (spellNum) {
				case 0:
						if (newSpell.name.Substring (0, 4).Equals ("Fire"))
								Fire = newSpell;
						return true;
						break;
				case 1:
						if (newSpell.name.Substring (0, 3).Equals ("Ice"))
								Ice = newSpell;
						return true;
						break;
				case 2:
						if (newSpell.name.Substring (0, 9).Equals ("Lightning"))
								Lightning = newSpell;
						return true;
						break;
				case 3:
						if (newSpell.name.Substring (0, 4).Equals ("Heal"))
								Heal = newSpell;
						return true;
						break;
				case 4:
						if (newSpell.name.Substring (0, 4).Equals ("Wind"))
								Wind = newSpell;
						return true;
						break;
				case 5:
						if (newSpell.name.Substring (0, 6).Equals ("Magnet"))
								Magnet = newSpell;
						return true;
						break;
				case 6:
						if (newSpell.name.Substring (0, 6).Equals ("Mirror"))
								Mirror = newSpell;
						return true;
						break;
				case 7:
						if (newSpell.name.Substring (0, 5).Equals ("Heavy"))
								Heavy = newSpell;
						return true;
						break;
				case 8:
						if (newSpell.name.Substring (0, 5).Equals ("Death"))
								Death = newSpell;
						return true;
						break;
				case 9:
						if (newSpell.name.Substring (0, 9).Equals ("Illuminate"))
								Illuminate = newSpell;
						return true;
						break;
				default:
						break;
				}
				return false;
		}
}
