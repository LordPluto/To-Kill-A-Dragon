﻿using UnityEngine;
using System.Collections;

public class PlayerMasterController : MonoBehaviour {

	private Animator _animator;
	private CharacterController _controller;

	private PlayerMovementController playerMovement;
	private PlayerAnimationController playerAnimation;
	private PlayerSpellController playerSpells;

	private GameController gameControl;

	private bool Talking;
	private bool Casting;
	private bool Frozen;
	private bool shiftOnce;

	#region Cutscene

	private bool Cutscene;

	private Transform[] pathPoints;
	private Transform currentPathPoint;
	private int pointIndex;
	
	private float moveSpeed = 4;
	private float pointReached = 10;

	#endregion

	#region Stats

	private float currentHP;
	private float maxHP;

	private float currentMP;
	private float maxMP;

	#endregion

	// Use this for initialization
	void Start () {
				_animator = GetComponent<Animator> ();
				_controller = GetComponent<CharacterController> ();
		
				shiftOnce = false;
				Talking = false;
				Casting = false;

				playerMovement = new PlayerMovementController (_controller);
				playerAnimation = new PlayerAnimationController (_animator);
				playerSpells = new PlayerSpellController ();

				if (!_animator)
						Debug.Log ("Sarah doesn't have animations. Moving her might look weird.");

				currentHP = maxHP = 100;
				currentMP = maxMP = 100;

				Cutscene = false;
		}

	void Awake () {
				gameControl = GameObject.Find ("_GameController").GetComponent<GameController> ();
		}

	void Update () {
				if (Cutscene) {
						CutsceneUpdate ();
				} else {
						PlayerUpdate ();
				}
		}

	/**
	 * The Update function used when the system has control
	 * **/
	private void CutsceneUpdate () {
		Vector3 oldPosition = transform.position;

		Vector3 direction = currentPathPoint.transform.position - transform.position;
		Vector3 moveVector = direction.normalized * moveSpeed * Time.deltaTime;

		transform.position = playerMovement.CutsceneMovement (transform.position, direction, moveVector);

		playerAnimation.setSpeed (transform.position != oldPosition);

		float directionAngle = (Mathf.Atan2 (direction.z, direction.x) * Mathf.Rad2Deg + 360) % 360;

		playerAnimation.setDirectionFromAngle (directionAngle);
		
				if ((currentPathPoint.transform.position - transform.position).sqrMagnitude < Mathf.Pow(pointReached / 10, 2)) {
						++pointIndex;
						if (pointIndex > pathPoints.Length - 1) {
								gameControl.PlayerFinishedCutscene ();
						} else {
								currentPathPoint = pathPoints [pointIndex];
						}
				}
		}
	
	/**
	 * The Update function used when the player has control
	 * **/
	private void PlayerUpdate () {
				playerMovement.PlayerMovement (Talking, Frozen, Casting);

				float changeSpell = Input.GetAxis ("SpellChange");
				float[] quickSelect = new float[] { Input.GetAxis ("Quick1"),Input.GetAxis ("Quick2"),
													Input.GetAxis ("Quick3"), Input.GetAxis ("Quick4"),
													Input.GetAxis ("Quick5")};

				float castSpell = Input.GetAxis ("CastSpell");
		
				ChangeSpell (changeSpell, quickSelect, castSpell);

				playerAnimation.Animation (_controller.velocity);

				if (Casting && playerSpells.CheckCastTime () <= -2) {
						StopCasting ();
				}
				if (playerSpells.CheckCastDelay () == 0) {
						gameControl.CastSpell (playerAnimation.getDirection ());
				}

				if (castSpell > 0.01 && !Talking && !Frozen) {
						CastSpell ();
				}
		}

	/**
	 * Checks to see if the player is changing what spell they have active
	 * **/
	private void ChangeSpell(float spellChange, float[] quickSelect, float castSpell){
				bool reset = (spellChange == 0 && quickSelect [0] == 0 &&
						quickSelect [1] == 0 && quickSelect [2] == 0 &&
						quickSelect [3] == 0 && quickSelect [4] == 0);

				if (!shiftOnce && castSpell < 0.01) {
						if (spellChange > 0.01) {			// The player hit E
								gameControl.NextSpell ();
								shiftOnce = true;
						} else if (spellChange < -0.01) {	// The player hit Q
								gameControl.PreviousSpell ();
								shiftOnce = true;
						} else {
								for (int i = 0; i<quickSelect.Length; i++) {
										if (quickSelect [i] > 0.01) {
												gameControl.QuickSelect (i + 1);
												shiftOnce = true;
												break;
										}
								}
						}
				} else if (reset) {
						shiftOnce = false;
				}
		}

	/**
	 * Checks to see if the player isn't already casting a spell. If they aren't, then
	 * cast the spell.
	 * **/
	private void CastSpell () {
				if (playerSpells.StartCastTime (gameControl.getSpell ())) {
						Casting = true;
						playerAnimation.SpellAnimation (gameControl.getSpell ());
				}
		}

	/**
	 * Freezes the player in place - used in dialogue
	 * **/
	public void TalkingFreeze () {
		Talking = true;
	}

	/**
	 * Frees the player - used in dialogue
	 * **/
	public void TalkingMove () {
		Talking = false;
	}

	/**
	 * Prevents the player from moving - used for not dialogue
	 * **/
	public void Freeze() {
			Frozen = true;
	}

	/**
	 * Allows the player to move - used for not dialogue
	 * **/
	public void Move() {
			Frozen = false;
	}

	/**
	 * Returns the player's position
	 * **/
	public Vector3 getPosition(){
				return transform.position;
		}

	/**
	 * Stops the player from casting.
	 * **/
	private void StopCasting(){
				Casting = false;
				playerAnimation.StopCasting ();
		}

	/**
	 * Used when the Lightning spell destroys itself.
	 * **/
	public void LightningReset() {
				playerSpells.Reset ();
				StopCasting ();
		}

	/**
	 * Gets current HP
	 * **/
	public float getHP(){
				return currentHP;
		}

	/**
	 * Sets new HP
	 * Parameter - float change: the amount to change the HP by. Can be negative.
	 * **/
	public void changeHP(float change){
				if (currentHP + change > maxHP) {
						currentHP = maxHP;
				} else if (currentHP + change < 0) {
						currentHP = 0;
				} else {
						currentHP += change;
				}
		}

	/**
	 * Gets percentage of HP
	 * **/
	public float getPercentHP () {
				return 100 * currentHP / maxHP;
		}

	/**
	 * Gets current MP
	 * **/
	public float getMP(){
				return currentMP;
		}
	
	/**
	 * Sets new MP
	 * Parameter - float change: the amount to change the MP by. Can be negative.
	 * **/
	public void changeMP(float change){
				if (currentMP + change > maxMP) {
						currentMP = maxMP;
				} else if (currentMP + change < 0) {
						currentMP = 0;
				} else {
						currentMP += change;
				}
		}

	/**
	 * Gets percentage of MP
	 * **/
	public float getPercentMP () {
				return 100 * currentMP / maxMP;
		}

	/**
	 * Jumps directly to a given destination. If the destination is blocked by something, does nothing.
	 * **/
	public void JumpPosition(Vector3 destination){
				//NOTE: WE DON'T HAVE COLLISION CHECKING YET
				transform.position = destination;
				_controller.SimpleMove (Vector3.zero);
		}

	/**
	 * Triggers a cutscene.
	 * **/
	public void EnterCutscene(Transform[] points){
				pathPoints = null;		//Clear out whatever's in there.

				pathPoints = (Transform[])points.Clone ();
				if (pathPoints.Length > 0) {
						currentPathPoint = pathPoints [0];
						pointIndex = 0;
				}

				Cutscene = true;
		}

	/**
	 * Ends the cutscene.
	 * **/
	public void ExitCutscene() {
				pathPoints = null;

				Cutscene = false;
				_animator.SetFloat ("Speed", 0);
				_animator.SetFloat ("Direction", 0);
		}
}
