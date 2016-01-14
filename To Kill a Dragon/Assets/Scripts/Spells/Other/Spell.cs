using UnityEngine;
using System.Collections;

public class Spell {

	private Transform spellForm;
	
	private int spellNumber;		//Spell's unique number. Uses the SPELL enum.
	private int spellCastTime;		//Time until another spell can be cast
	private int spellDelay;			//Delay between beginning animation and time spell is cast
	private int spellCost;			//Cost in MP
	private int spellDamage;		//Amount of damage each attack spell does.
	private float spellKnockback;	//Multiplier of knockback distance from spell
	private string spellType;		//What type of spell it is, "Attack" or "Support"
	


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

	public Spell(SPELL number, int cast, int delay, int cost, int damage, float knockback, string type){
				spellNumber = (int)number;
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
	public AttackSpell(SPELL number, int cast, int delay, int cost, int damage, float knockback) : base(number, cast, delay, cost, damage, knockback, "Attack"){
		}
}

public class FireSpell : AttackSpell {
	public FireSpell() : base(SPELL.Fire,60,20,10,10,2) {

		}
}

public class Fire2Spell : AttackSpell {
	public Fire2Spell() : base(SPELL.Fire2,60,20,15,20,2) {
		}
}

public class Fire3Spell : AttackSpell {
	public Fire3Spell() : base(SPELL.Fire3,60,20,20,40,2) {
	}
}

public class FireExSpell : AttackSpell {
	public FireExSpell() : base(SPELL.FireEx,60,20,25,160,2) {
	}
}

public class IceSpell : AttackSpell {
	public IceSpell() : base(SPELL.Ice,21,10,10,20,3) {
		
		}
}

public class Ice2Spell : AttackSpell {
	public Ice2Spell() : base(SPELL.Ice2,21,10,20,40,3) {
		
	}
}

public class Ice3Spell : AttackSpell {
	public Ice3Spell() : base(SPELL.Ice3,21,10,30,80,3) {
		
	}
}

public class IceExSpell : AttackSpell {
	public IceExSpell() : base(SPELL.IceEx,21,10,40,320,3) {
		
	}
}

public class LightningSpell : AttackSpell {
	public LightningSpell() : base(SPELL.Lightning,30,20,10,15,0) {
		
		}
}

public class Lightning2Spell : AttackSpell {
	public Lightning2Spell() : base(SPELL.Lightning2,30,20,20,30,0) {
		
	}
}

public class Lightning3Spell : AttackSpell {
	public Lightning3Spell() : base(SPELL.Lightning3,30,20,30,60,0) {
		
	}
}

public class LightningExSpell : AttackSpell {
	public LightningExSpell() : base(SPELL.LightningEx,30,20,40,240,0) {
		
	}
}

public class SupportSpell : Spell {
	public SupportSpell(SPELL number, int cast, int delay, int cost) : base(number, cast, delay, cost, 0, 0, "Support"){
		}
}

public class HealSpell : SupportSpell {
	public HealSpell() : base(SPELL.Heal, 120, 20, 25){
		}
}

public class Heal2Spell : SupportSpell {
	public Heal2Spell() : base(SPELL.Heal2, 120, 20, 30){
	}
}

public class Heal3Spell : SupportSpell {
	public Heal3Spell() : base(SPELL.Heal3, 120, 20, 35){
	}
}

public class HealExSpell : SupportSpell {
	public HealExSpell() : base(SPELL.HealEx, 120, 20, 40){
	}
}

public class WindSpell : SupportSpell {
	public WindSpell() : base(SPELL.Wind, 30, 20, 30){
		}
}

public class MagnetSpell : SupportSpell {
	public MagnetSpell() : base(SPELL.MagnetNorth, 15, 10, 0){
		}
}

public class MirrorSpell : SupportSpell {
	public MirrorSpell() : base(SPELL.Mirror, 120, 20, 5){
	}
}

public class Mirror2Spell : SupportSpell {
	public Mirror2Spell() : base(SPELL.Mirror2, 120, 20, 5){
	}
}

public class Mirror3Spell : SupportSpell {
	public Mirror3Spell() : base(SPELL.Mirror3, 120, 20, 5){
	}
}

public class MirrorExSpell : SupportSpell {
	public MirrorExSpell() : base(SPELL.MirrorEx, 120, 20, 5){
	}
}