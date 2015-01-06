﻿using UnityEngine;
using System.Collections;

public class BasicEnemyController : MonoBehaviour {

	private EnemyController enemyControl;
	private WanderController wanderControl;

	private GameController gameControl;

	#region Stats

	public string Name;
	public float HP;

	public float Atk;
	public float Def;

	public float EXPValue;

	#endregion

	#region Movement Modifiers

	public float damageKnockback;

	#endregion

	// Use this for initialization
	void Start () {
				wanderControl = GetComponentInChildren<WanderController> ();
				enemyControl = GetComponentInChildren<EnemyController> ();

				damageKnockback = Mathf.Max (0, Mathf.Min (1, damageKnockback));
		}

	void Awake () {
				gameControl = GameObject.Find ("_GameController").GetComponent<GameController> ();
		}
	
	// Update is called once per frame
	void Update () {
				if (HP <= 0) {
						gameControl.DestroyMonster (gameObject);
				}
		}

	/**
	 * Gets a new point whenever the moving part of the enemy reaches its position
	 * **/
	public void getNewPoint(){
				enemyControl.setPathPoint (wanderControl.getRandomPoint ());
		}

	/**
	 * Sets the new direction for the animator
	 * **/
	public void setDirection(float direction){
		//_animator.setFloat("Direction", direction);
	}

	/**
	 * Sets the speed for the animator
	 * **/
	public void setSpeed(float speed){
		//_animator.setFloat("Speed", speed);
	}

	/**
	 * Deals damage to player
	 * **/
	public void DealDamage(Vector3 playerDirection, Vector3 myDirection){
				gameControl.DealPlayerDamage (gameObject, playerDirection, myDirection);
		}

	/**
	 * Take damage from spell
	 * **/
	public void TakeDamage(){
				Spell spellCast = gameControl.getSpell ();
				HP -= Mathf.Max (0, spellCast.getSpellDamage () - Def);
		}

	/**
	 * How much EXP is this monster worth?
	 * **/
	public float valueEXP(){
				return EXPValue;
		}

	/**
	 * Tells the model to flinch back
	 * **/
	public void FlinchBack(Vector3 flinch){
				enemyControl.FlinchBack (flinch);
		}

	/**
	 * Gets the knockback of the monster
	 * **/
	public float getKnockback(){
				return damageKnockback;
		}
}
