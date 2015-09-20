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

				if (_controller.isGrounded) {
						vSpeed = 0;
				}

				_controller.Move (displacement * Time.deltaTime);

				deactivated = !_controller.isGrounded;
		}

	/**
	 * Handles movement when the player is in control
	 * **/
	public void PlayerMovement(bool Talking, bool Frozen, bool Casting, bool WindBoost){
				float ZSpeed = Input.GetAxis ("Vertical");
				float XSpeed = Input.GetAxis ("Horizontal");

				float speedMultiplier = movementSpeed * (WindBoost ? 2 : 1);

				if (deactivated) {
						Move (speed);
						return;
				}

				speed = new Vector3 (XSpeed * speedMultiplier, 0, ZSpeed * speedMultiplier);

				if (Talking || Frozen || Casting) {
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
	 * Handles movement when the player is being magnetized.
	 * **/
	public Vector3 MagnetMovement (Vector3 position, Vector3 moveVector) {
				Vector3 point1 = position + new Vector3 (0, 1.51f, 0), point2 = position + new Vector3 (0, .51f, 0);
				float radius = 0.5f;
				RaycastHit hit;
		
				Physics.CapsuleCast (point1, point2, radius, moveVector, out hit, 0.5f, (1 << 12 | 1 << 13));
		
				if (hit.collider == null) {
						position += moveVector;
				}

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
}
