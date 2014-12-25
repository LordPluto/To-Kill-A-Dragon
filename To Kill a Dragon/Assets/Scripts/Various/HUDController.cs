using UnityEngine;
using System.Collections;

public class HUDController : MonoBehaviour {

	private int spellIndex;
	private int spellLength;

	private HUDSpellController spells;
	private HUDHeadController heads;

	void Start () {
				spellIndex = 0;
				spellLength = GameObject.Find ("Player").GetComponent<PlayerAnimationController> ().NumSpells ();
		}

	void Awake () {
				spells = GameObject.Find ("HUD Spells").GetComponent<HUDSpellController> ();
				heads = GameObject.Find ("HUD Head").GetComponent<HUDHeadController> ();
		}

	void Update () {
				spellLength = GameObject.Find ("Player").GetComponent<PlayerAnimationController> ().NumSpells ();
		}

	public void NextSpell(){
				spellIndex++;
				if (spellIndex >= spellLength) {
						spellIndex = 0;
				}
				spells.SetTexture (spellIndex);
		}

	public void PreviousSpell(){
				spellIndex--;
				if (spellIndex < 0) {
						spellIndex = spellLength - 1;
				}
				spells.SetTexture (spellIndex);
		}

	public void QuickSpell(int quickSlot){
				if (quickSlot >= 0 && quickSlot < spellLength) {
						spellIndex = quickSlot;
				}
				spells.SetTexture (spellIndex);
		}
}
