using UnityEngine;
using System.Collections;

public class Spell {

	private Transform spellForm;

	private int spellNumber;
	private int spellCastTime;
	private int spellDelay;
	private int spellCost;
	private int spellDamage;
	private float spellKnockback;
	private string spellType;
	


	public Spell(){
				spellNumber = -1;
				spellCastTime = 0;
				spellDelay = 0;
				spellCost = 0;
				spellType = "Null";
				spellForm = null;
				spellDamage = 0;
				spellKnockback = 0;
		}

	public Spell(int number, int cast, int delay, int cost, int damage, float knockback, string type){
				spellNumber = number;
				spellCastTime = cast;
				spellDelay = delay;
				spellCost = cost;
				spellDamage = damage;
				spellKnockback = knockback;
				spellType = type;
				spellForm = null;
		}

	public void setNumber(int number){
				spellNumber = number;
		}
	public int getNumber(){
				return spellNumber;
		}

	public void setCastTime(int cast){
				spellCastTime = cast;
		}
	public int getCastTime(){
				return spellCastTime;
		}

	public void setDelay(int delay){
				spellDelay = delay;
		}
	public int getDelay(){
				return spellDelay;
		}

	public void setCost(int cost){
				spellCost = cost;
		}
	public int getCost(){
				return spellCost;
		}

	public void setSpellType(string type){
				spellType = type;
		}
	public string getSpellType(){
				return spellType;
		}

	public void setSpellForm(Transform prefab){
				spellForm = prefab;
		}
	public Transform getSpellForm(){
				return spellForm;
		}

	public void setSpellDamage(int damage){
				spellDamage = damage;
		}
	public int getSpellDamage(){
				return spellDamage;
		}

	public void setSpellKnockback(float knockback){
				spellKnockback = knockback;
		}
	public float getSpellKnockback(){
				return spellKnockback;
		}
}

public class AttackSpell : Spell {
	public AttackSpell(int number, int cast, int delay, int cost, int damage, float knockback) : base(number, cast, delay, cost, damage, knockback, "Attack"){
		}
}

public class FireSpell : AttackSpell {
	public FireSpell() : base(0,60,20,10,10,2) {

		}
}

public class Fire2Spell : AttackSpell {
	public Fire2Spell() : base(1,60,20,15,20,2) {
		}
}

public class Fire3Spell : AttackSpell {
	public Fire3Spell() : base(2,60,20,20,40,2) {
	}
}

public class FireExSpell : AttackSpell {
	public FireExSpell() : base(3,60,20,25,80,2) {
	}
}

public class IceSpell : AttackSpell {
	public IceSpell() : base(4,21,10,5,5,3) {
		
		}
}

public class Ice2Spell : AttackSpell {
	public Ice2Spell() : base(5,21,10,10,10,3) {
		
	}
}

public class Ice3Spell : AttackSpell {
	public Ice3Spell() : base(6,21,10,15,15,3) {
		
	}
}

public class IceExSpell : AttackSpell {
	public IceExSpell() : base(7,21,10,20,20,3) {
		
	}
}

public class LightningSpell : AttackSpell {
	public LightningSpell() : base(8,30,20,20,20,0) {
		
		}
}

public class Lightning2Spell : AttackSpell {
	public Lightning2Spell() : base(9,30,20,30,40,0) {
		
	}
}

public class Lightning3Spell : AttackSpell {
	public Lightning3Spell() : base(10,30,20,40,80,0) {
		
	}
}

public class LightningExSpell : AttackSpell {
	public LightningExSpell() : base(11,30,20,50,160,0) {
		
	}
}

public class SupportSpell : Spell {
	public SupportSpell(int number, int cast, int delay, int cost) : base(number, cast, delay, cost, 0, 0, "Support"){
		}
}

public class HealSpell : SupportSpell {
	public HealSpell() : base(12, 120, 20, 25){
		}
}

public class Heal2Spell : SupportSpell {
	public Heal2Spell() : base(13, 120, 20, 35){
	}
}

public class Heal3Spell : SupportSpell {
	public Heal3Spell() : base(14, 120, 20, 45){
	}
}

public class HealExSpell : SupportSpell {
	public HealExSpell() : base(15, 120, 20, 55){
	}
}

public class WindSpell : SupportSpell {
	public WindSpell() : base(16, 30, 20, 30){
		}
}