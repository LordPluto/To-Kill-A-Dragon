using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	#region Movement
	
	public bool npcMovement = true;

	private Vector3 currentPathPoint;
	
	private float moveSpeed = 2;
	private double pointReached = .1;
	
	#endregion

	private BasicEnemyController parentControl;

	// Use this for initialization
	void Start () {
				parentControl = GetComponentInParent<BasicEnemyController> ();
		currentPathPoint = Vector3.zero;
		}
	
	// Update is called once per frame
	void Update () {
				if (currentPathPoint == Vector3.zero) {
						parentControl.getNewPoint ();
				} else {
						MoveTowardPoint ();
		
						if (Vector3.Distance (currentPathPoint, transform.position) < (float)pointReached) {
								currentPathPoint = Vector3.zero;
								parentControl.getNewPoint ();
						}
				}
		}

	/**
	 * Moves the enemy toward the new point
	 * **/
	private void MoveTowardPoint () {
		Vector3 direction = currentPathPoint - transform.position;
		Vector3 moveVector = direction.normalized * moveSpeed * Time.deltaTime;
		
		float directionAngle = (Mathf.Atan2 (direction.z, direction.x) * Mathf.Rad2Deg + 360) % 360;
		
		if (directionAngle > 45 && directionAngle <= 135) {
			parentControl.setDirection(2);
		} else if (directionAngle > 135 && directionAngle <= 225) {
			parentControl.setDirection(3);
		} else if (directionAngle > 225 && directionAngle <= 315) {
			parentControl.setDirection(0);
		} else if (directionAngle > 315 || directionAngle <= 45) {
			parentControl.setDirection(1);
		}
		
		Ray ray = new Ray (transform.position, direction);
		RaycastHit hit;
		
		if (Physics.Raycast (ray, out hit, moveVector.magnitude) && hit.collider.CompareTag ("Level")) {
			parentControl.setSpeed(0);
			return;
		} else {
			parentControl.setSpeed(1);
			transform.position += moveVector;
		}
	}

	/**
	 * Sets the new destination
	 * **/
	public void setPathPoint (Vector3 newPoint){
				currentPathPoint = newPoint;
		}
}
