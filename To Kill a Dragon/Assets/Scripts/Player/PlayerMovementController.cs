using UnityEngine;
using System.Collections;

public class PlayerMovementController {

	private float movementSpeed = 5;
	private CharacterController _controller;

	private Vector3 speed;
	private const float gravity = 9.82f;
	private float vSpeed = 0;

	private bool deactivated = false;

	public PlayerMovementController(CharacterController _controller){
				speed = new Vector3 ();
				this._controller = _controller;
		}

	private void Move(Vector3 displacement){
		vSpeed -= gravity * Time.deltaTime;
		displacement.y = vSpeed;

		_controller.Move (displacement * Time.deltaTime);

		if (_controller.isGrounded) {
			vSpeed = 0;
		}

		deactivated = !_controller.isGrounded;
	}

	/**
	 * Handles movement when the player is in control
	 * **/
	public void PlayerMovement(bool Talking, bool Frozen, Quaternion PlayerRotation){
				float ZSpeed = Input.GetAxis ("Vertical");
				float XSpeed = Input.GetAxis ("Horizontal");

				float speedMultiplier = movementSpeed;

				if (deactivated) {
						Move (speed);
						return;
				}

				speed = PlayerRotation * new Vector3 (XSpeed * speedMultiplier, 0, ZSpeed * speedMultiplier);

				if (Talking || Frozen) {
						speed = Vector3.zero;
				}

				Move (speed);
		}

	/**
	 * Handles movement when the system is in control
	 * **/
	public Vector3 CutsceneMovement (Vector3 position, Vector3 direction, Vector3 moveVector) {
				Ray ray = new Ray (position, direction);
				RaycastHit hit;
		
				if (!(Physics.Raycast (ray, out hit, moveVector.magnitude) && (hit.collider.CompareTag ("NPC") || hit.collider.CompareTag ("Level")))) {
						position += moveVector;
				}

				return position;
		}

	/**
	 * Handles movement when the player is flinching - the destination should alreaedy have been cleared.
	 * **/
	public Vector3 FlinchMovement (Vector3 position, Vector3 moveVector) {
				position += moveVector;
		
				return position;
		}

	/**
	 * Prevents the player from controlling the character until they've landed.
	 * Only used when the player needs to fall straight down.
	 * **/
	public void DeactivateUntilGrounded() {
		deactivated = true;
		speed = Vector3.zero;
	}

	/**
	 * <para>Checks to see if the character is falling.</para>
	 * <returns>True if falling, false if not</returns>
	 * **/
	public bool isFalling() {
		return !_controller.isGrounded;
	}
}
