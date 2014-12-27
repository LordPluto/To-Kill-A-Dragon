using UnityEngine;
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
		}

	void Awake () {
				gameControl = GameObject.Find ("_GameController").GetComponent<GameController> ();
		}
	
	// Update is called once per frame
	void Update () {
				playerMovement.Movement (Talking, _animator.GetCurrentAnimatorStateInfo (0).IsTag ("Frozen"), Casting);

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

	private void CastSpell () {
				if (playerSpells.StartCastTime (gameControl.getSpell ())) {
						Casting = true;
						playerAnimation.SpellAnimation (gameControl.getSpell ());
				}
		}

	public void TalkingFreeze () {
		Talking = true;
	}
	
	public void TalkingMove () {
		Talking = false;
	}

	public void CutsceneFreeze() {
			Frozen = true;
	}

	public void CutsceneMove() {
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
}
