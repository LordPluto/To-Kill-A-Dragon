using UnityEngine;
using System.Collections;

public class PlayerMovementController {

	private float movementSpeed = 5;
	private CharacterController _controller;

	public PlayerMovementController(CharacterController _controller){
				this._controller = _controller;
		}

	public void Movement(bool Talking, bool Frozen, bool Casting){
				float ZSpeed = Input.GetAxis ("Vertical");
				float XSpeed = Input.GetAxis ("Horizontal");
		
				Vector3 speed = new Vector3 (XSpeed * movementSpeed, 0, ZSpeed * movementSpeed);
		
				if (Talking || Frozen || Casting) {
						speed = Vector3.zero;
				}

				_controller.SimpleMove (speed);
		}
}
