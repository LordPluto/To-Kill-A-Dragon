﻿using UnityEngine;
using System.Collections;

public class Spell {

	private Transform spellForm;

	private int spellNumber;
	private int spellCastTime;
	private int spellDelay;
	private int spellCost;
	private string spellType;

	public Spell(){
				spellNumber = -1;
				spellCastTime = 0;
				spellDelay = 0;
				spellCost = 0;
				spellType = "Null";
				spellForm = null;
		}

	public Spell(int number, int cast, int delay, int cost, string type){
				spellNumber = number;
				spellCastTime = cast;
				spellDelay = delay;
				spellCost = cost;
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
}

public class AttackSpell : Spell {
	public AttackSpell(int number, int cast, int delay, int cost) : base(number, cast, delay, cost, "Attack"){
		}
}

public class FireSpell : AttackSpell {
	public FireSpell() : base(0,60,20,10) {

		}
}

public class IceSpell : AttackSpell {
	public IceSpell() : base(1,11,10,5) {
		
		}
}

public class LightningSpell : AttackSpell {
	public LightningSpell() : base(2,60,20,20) {
		
		}
}