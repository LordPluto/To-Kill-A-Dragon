using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerAnimationController : MonoBehaviour {

	#region Animations

	private Animator _animator;
	
	enum Facing {
		Down = 0,
		Right = 1,
		Up = 2,
		Left = 3,
	}

	private Facing _characterFacing;
	private Facing lastFacing;

	#endregion Animations

	#region Spells

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
	
	enum Spell{
		Fire = 0,
		Ice = 1,
		Lightning = 2,
		Heal = 3,
		Wind = 4,
		Magnet = 5,
		Mirror = 6,
		Heavy = 7,
		Death = 8,
		Illuminate = 9,
	}

	private Spell selectedSpell;

	enum CastTimes{
		Fire = 60,
		Ice = 11,
		Lightning = 60,
	}

	private int lastCast;
	private int timeToCast;

	private List<Spell> KnownSpells;
	private Spell[] QuickSpells;

	#endregion

	private bool Talking;

	void Start() {
				
		}
	
	void Awake () {	
				Talking = false;

				_animator = GetComponent<Animator> ();
				KnownSpells = new List<Spell> ();
				QuickSpells = new Spell[5];

				selectedSpell = Spell.Fire;

				lastCast = 0;
				timeToCast = -1;

				if (!_animator)
						Debug.Log ("Sarah doesn't have animations. Moving her might look weird.");

				KnownSpells.Add (Spell.Fire);
				KnownSpells.Add (Spell.Ice);
				KnownSpells.Add (Spell.Lightning);
		
		}
	
	void Update () {
				// ANIMATION sector
				if (_animator) {
				
						CharacterController controller = GetComponent<CharacterController> ();

						float horizontalMovement = Input.GetAxis ("Horizontal");
						float verticalMovement = Input.GetAxis ("Vertical");

						float degree = Mathf.Atan2 (verticalMovement, horizontalMovement);
			
						if (horizontalMovement == 0 && verticalMovement != 0) {
								if (verticalMovement > 0) {
										_characterFacing = Facing.Up;
								} else {
										_characterFacing = Facing.Down;
								}
						} else if (verticalMovement == 0 && horizontalMovement != 0) {
								if (horizontalMovement > 0) {
										_characterFacing = Facing.Right;
								} else {
										_characterFacing = Facing.Left;
								}
						} else if (horizontalMovement > 0 && verticalMovement > 0) {
								if (degree > Mathf.PI / 4) {
										_characterFacing = Facing.Up;
								} else {
										_characterFacing = Facing.Right;
								}
						} else if (horizontalMovement > 0 && verticalMovement < 0) {
								if (degree < Mathf.PI / -4) {
										_characterFacing = Facing.Down;
								} else {
										_characterFacing = Facing.Right;
								}
						} else if (horizontalMovement < 0 && verticalMovement > 0) {
								if (degree >= 3 * Mathf.PI / 4) {
										_characterFacing = Facing.Left;
								} else {
										_characterFacing = Facing.Up;
								}
						} else if (horizontalMovement < 0 && verticalMovement < 0) {
								if (degree <= 3 * Mathf.PI / -4) {
										_characterFacing = Facing.Left;
								} else {
										_characterFacing = Facing.Down;
								}
						}

						if (controller.velocity.y < -1) {
								_animator.SetBool ("Falling", true);
						} else {
								_animator.SetBool ("Falling", false);
						}
			
						switch (_characterFacing) {
						case Facing.Down:
								_animator.SetFloat ("Direction", 0);
								break;
						case Facing.Right:
								_animator.SetFloat ("Direction", 1);
								break;
						case Facing.Up:
								_animator.SetFloat ("Direction", 2);
								break;
						case Facing.Left:
								_animator.SetFloat ("Direction", 3);
								break;
						}

						if (timeToCast == 0) {
								switch (selectedSpell) {
								case Spell.Fire:
										Instantiate (Fire, 
					            this.transform.position + new Vector3 (((int)_characterFacing % 2 == 1 ?
					                                       (_characterFacing == Facing.Right ? 1 : -1) : 0),
					                                      (_characterFacing == Facing.Up ? 2 : 1),
					                                      ((int)_characterFacing % 2 == 0 ?
					 											(_characterFacing == Facing.Down ? -1 : 1) : 0)),						 															
					            Quaternion.Euler (0,
					                  0,
					                  (int)_characterFacing * 90));

										break;
								case Spell.Ice:
										if (!GameObject.Find ("Ice(Clone)")) {
												Instantiate (Ice,
						             this.transform.position + new Vector3 (((int)_characterFacing % 2 == 1 ?
						                                       				(_characterFacing == Facing.Right ? 1 : -1) : 0),
						                                     			   2,
						                                      			   ((int)_characterFacing % 2 == 0 ?
						 													(_characterFacing == Facing.Down ? -1 : 1) : 0)) / 3,
						             Quaternion.Euler (90,
						                  			   180 - (int)_characterFacing * 90,
						                  				0));
										}

										break;
								case Spell.Lightning:
										Instantiate (Lightning,
					             this.transform.position + new Vector3 (((int)_characterFacing % 2 == 1 ?
					                                       (_characterFacing == Facing.Right ? 1 : -1) : 0),
					                                      2,
					                                      ((int)_characterFacing % 2 == 0 ?
					 										(_characterFacing == Facing.Down ? -1 : 1) : 0)) / 3,
					             Quaternion.Euler (90,
					                  (int)_characterFacing * -90,
					                  0));
					
										break;
								}
						}
			
						if (lastCast <= 0 && Input.GetAxis ("CastSpell") > 0.01 && !Talking) {
								switch (selectedSpell) {
								case Spell.Fire:
										_animator.SetBool ("AttackSpell", true);

										lastCast = (int)CastTimes.Fire;

										timeToCast = 20;

										break;
								case Spell.Ice:
										GameObject iceClone = GameObject.Find ("Ice(Clone)");

										if (!iceClone) {
												_animator.SetBool ("AttackSpell", true);

												lastCast = (int)CastTimes.Ice;

												timeToCast = 10;
										} else if (lastFacing != _characterFacing) {
												Destroy (iceClone);

												timeToCast = 1;
												lastCast = 1;
										} else {
												lastCast = 1;
										}

										break;
								case Spell.Lightning:
										_animator.SetBool ("AttackSpell", true);

										lastCast = (int)CastTimes.Lightning;

										timeToCast = 20;

										break;
								}
						}

						if (lastCast > 0) {
								lastCast--;
						} else {
								_animator.SetBool ("AttackSpell", false);
								_animator.SetBool ("SupportSpell", false);
						}

						if (timeToCast >= 0) {
								timeToCast--;
						} else {
								timeToCast = -1;
						}

						_animator.SetFloat ("Speed", Mathf.Abs (controller.velocity.sqrMagnitude / 25));

						lastFacing = _characterFacing;
				}
		}

	public void NextSpell () {
				int spellChosen = KnownSpells.IndexOf (selectedSpell) + 1;

				if (spellChosen >= KnownSpells.Count) {
						spellChosen = 0;
				}

				selectedSpell = KnownSpells [spellChosen];
		}

	public void PreviousSpell () {
				int spellChosen = KnownSpells.IndexOf (selectedSpell) - 1;
		
				if (spellChosen <= -1) {
						spellChosen = KnownSpells.Count - 1;
				}
		
				selectedSpell = KnownSpells [spellChosen];
		}

	public void QuickSpell (int quickSlot) {
				if (quickSlot >= 0 && quickSlot < QuickSpells.Length) {
						selectedSpell = QuickSpells [quickSlot];
				}
		}

	public int NumSpells () {
				return KnownSpells.Count;
		}

	public void TalkingFreeze () {
				Talking = true;
		}

	public void TalkingMove () {
				Talking = false;
		}
}
