using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerAnimationController {

	#region Animations

	private Animator _animator;
	
	enum Facing {
		Down = 0,
		Right = 1,
		Up = 2,
		Left = 3,
	}

	private Facing _characterFacing;

	#endregion Animations

	public PlayerAnimationController(Animator _animator) {
				this._animator = _animator;
		}

	/**
	 * Handles animations. Once per frame.
	 * **/
	public void Animation(Vector3 position, Vector3 velocity){

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


		if (velocity.y < -1) {
			Vector3 point1 = position + new Vector3 (0, 1.51f, 0), point2 = position + new Vector3 (0, 0.51f, 0);
			float radius = 0.5f;
			RaycastHit hit;

			if (!Physics.CapsuleCast (point1, point2, radius, Vector3.down, out hit, 1.5f, (1 << 13))) {
				Debug.DrawLine (point1, point2);
				_animator.SetBool ("Falling", true);
			}
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

		Vector3 modifiedVel = new Vector3 (velocity.x, 0, velocity.z);

		_animator.SetFloat ("Speed", Mathf.Abs (modifiedVel.sqrMagnitude / 25));
	}

	/**
	 * Starts the casting animation depending on the type of spell
	 * **/
	public void SpellAnimation(Spell selectedSpell){
				if (selectedSpell.getSpellType ().Equals ("Attack")) {
						_animator.SetBool ("AttackSpell", true);
				} else if (selectedSpell.getSpellType ().Equals ("Support")) {
						_animator.SetBool ("SupportSpell", true);
				}
		}

	/**
	 * Stops the casting animations
	 * **/
	public void StopCasting(){
				_animator.SetBool ("AttackSpell", false);
				_animator.SetBool ("SupportSpell", false);
		}

	/**
	 * Return the direction the player is facing.
	 * **/
	public float getDirection(){
				switch (_characterFacing) {
				case Facing.Down:
						return 0;
				case Facing.Right:
						return 1;
				case Facing.Up:
						return 2;
				case Facing.Left:
						return 3;
				default:
						return 0;
				}
		}

	/**
	 * Given an angle in degrees, set the direction
	 * **/
	public void setDirectionFromAngle(float directionAngle) {		
				if (directionAngle > 45 && directionAngle <= 135) {
						_characterFacing = Facing.Up;
						_animator.SetFloat ("Direction", 2);
				} else if (directionAngle > 135 && directionAngle <= 225) {
						_characterFacing = Facing.Left;
						_animator.SetFloat ("Direction", 3);
				} else if (directionAngle > 225 && directionAngle <= 315) {
						_characterFacing = Facing.Down;
						_animator.SetFloat ("Direction", 0);
				} else if (directionAngle > 315 || directionAngle <= 45) {
						_characterFacing = Facing.Right;
						_animator.SetFloat ("Direction", 1);
				}
		}

	/**
	 * Given a boolean, set the speed
	 * **/
	public void setSpeed(bool moving){
				if (moving) {
						_animator.SetFloat ("Speed", 1);
				} else {
						_animator.SetFloat ("Speed", 0);
				}
		}
}
