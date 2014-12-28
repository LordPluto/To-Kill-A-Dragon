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
	public void PlayerMovement(bool Talking, bool Frozen, bool Casting){
				float ZSpeed = Input.GetAxis ("Vertical");
				float XSpeed = Input.GetAxis ("Horizontal");
		
				Vector3 speed = new Vector3 (XSpeed * movementSpeed, 0, ZSpeed * movementSpeed);
		
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
		
				if (Physics.Raycast (ray, out hit, moveVector.magnitude) && (hit.collider.CompareTag ("NPC") || hit.collider.CompareTag ("Level"))) {
						Debug.Log ("Derp.");
						Debug.DrawLine (ray.origin, hit.point, Color.red);
				} else {
						position += moveVector;
				}

				return position;
		}
}
