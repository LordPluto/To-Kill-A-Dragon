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
}
