using UnityEngine;
using System.Collections;

public class PlayerMovementController {

	private float movementSpeed = 5;
	private CharacterController _controller;

	public PlayerMovementController(CharacterController _controller){
				this._controller = _controller;
		}

	/**
	 * Handles movement when the player is in control
	 * **/
	public void PlayerMovement(bool Talking, bool Frozen, bool Casting, bool WindBoost){
				float ZSpeed = Input.GetAxis ("Vertical");
				float XSpeed = Input.GetAxis ("Horizontal");

				float speedMultiplier = movementSpeed * (WindBoost ? 2 : 1);
		
				Vector3 speed = new Vector3 (XSpeed * speedMultiplier, 0, ZSpeed * speedMultiplier);
		
				if (Talking || Frozen || Casting) {
						speed = Vector3.zero;
				}

				_controller.SimpleMove (speed);
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
}
