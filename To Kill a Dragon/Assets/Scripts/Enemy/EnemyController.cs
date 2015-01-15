﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyController : MonoBehaviour {

	#region Movement

	private Vector3 currentPathPoint;
	
	private float moveSpeed = 2;
	private double pointReached = .1;

	private GameObject TrackingTarget;

	private bool Tracking;
	private bool Backtrace;

	private List<Vector3> BacktracePoints;
	private int backtraceIndex;

	private bool InCutscene;

	#endregion

	private BasicEnemyController parentControl;
	
	private Animator _animator;

	// Use this for initialization
	void Start () {
				_animator = GetComponent<Animator> ();

				parentControl = GetComponentInParent<BasicEnemyController> ();
				currentPathPoint = Vector3.zero;

				TrackingTarget = null;
				Tracking = false;
				Backtrace = false;

				InCutscene = false;

				BacktracePoints = new List<Vector3> ();
				backtraceIndex = 0;
		}
	
	// Update is called once per frame
	void Update () {
				if (!InCutscene) {
						if (Tracking) {
								TrackingMove ();
						} else if (Backtrace) {
								BacktraceMove ();
						} else {
								WanderMove ();
						}
				}
		}

	/**
	 * Handles the normal movement
	 * **/
	private void WanderMove(){
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
	 * Handles the movement when tracking the player
	 * **/
	private void TrackingMove() {
				if (currentPathPoint == Vector3.zero) {
						currentPathPoint = new Vector3 (TrackingTarget.transform.position.x, transform.position.y, TrackingTarget.transform.position.z);
				} else {
						MoveTowardPoint ();
			
						if (Vector3.Distance (currentPathPoint, transform.position) < (float)pointReached) {
								BacktracePoints.Add (currentPathPoint);
								currentPathPoint = Vector3.zero;
								currentPathPoint = new Vector3 (TrackingTarget.transform.position.x, transform.position.y, TrackingTarget.transform.position.z);
						}
				}
		}

	/**
	 * Handles the movement when backtracking
	 * **/
	private void BacktraceMove() {
				if (currentPathPoint == Vector3.zero) {
						backtraceIndex--;
						if (backtraceIndex >= 0) {
								currentPathPoint = BacktracePoints [backtraceIndex];
						} else {
								BacktracePoints.Clear ();
								Backtrace = false;
						}
				} else {
						MoveTowardPoint ();
			
						if (Vector3.Distance (currentPathPoint, transform.position) < (float)pointReached) {
								currentPathPoint = Vector3.zero;
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
						parentControl.setDirection (_animator, 2);
				} else if (directionAngle > 135 && directionAngle <= 225) {
						parentControl.setDirection (_animator, 3);
				} else if (directionAngle > 225 && directionAngle <= 315) {
						parentControl.setDirection (_animator, 0);
				} else if (directionAngle > 315 || directionAngle <= 45) {
						parentControl.setDirection (_animator, 1);
				}
		
				Ray ray = new Ray (transform.position, direction);
				RaycastHit hit;
		
				if (Physics.Raycast (ray, out hit, moveVector.magnitude) && hit.collider.CompareTag ("Level")) {
						currentPathPoint = BacktracePoints [BacktracePoints.Count - 1];
						return;
				} else {
						parentControl.setSpeed (_animator, 1);
						transform.position += moveVector;
				}
		}

	/**
	 * Sets the new destination
	 * **/
	public void setPathPoint (Vector3 newPoint){
				currentPathPoint = newPoint;
		}

	/**
	 * Sets the new target (for tracking)
	 * **/
	public void SetTarget (GameObject target){
				Tracking = true;
				TrackingTarget = target;

				BacktracePoints.Add (new Vector3 (transform.position.x, transform.position.y, transform.position.z));
				currentPathPoint = Vector3.zero;
		}

	/**
	 * Loses the tracking target
	 * **/
	public void LoseTarget () {
				Tracking = false;
				Backtrace = true;
				backtraceIndex = BacktracePoints.Count - 1;
		}

	/**
	 * The monster flinches, moving back.
	 * **/
	public void FlinchBack (Vector3 flinchDirection) {
			flinchDirection = new Vector3 (flinchDirection.x, 0, flinchDirection.z);
						
			Vector3 tempDestination = transform.position + (flinchDirection.normalized * parentControl.getKnockback());
			Vector3 direction = tempDestination - transform.position;
			
			Ray ray = new Ray (transform.position, direction);
			RaycastHit hit;
			
			if (!(Physics.Raycast (ray, out hit, direction.magnitude) && (hit.collider.CompareTag ("NPC") || hit.collider.CompareTag ("Level")))) {
				currentPathPoint = tempDestination;
			} else {
				currentPathPoint = hit.point - direction.normalized/2;
			}
		}

	/**
	 * The enemy ran into the player
	 * **/
	public void HitPlayer (Collider c){
				Vector3 myDirection = c.transform.position - transform.position;
				Vector3 otherDirection = -myDirection;

				parentControl.DealDamage (otherDirection, myDirection);
		}

	/**
	 * The enemy ran into a spell
	 * **/
	public void HitSpell (Collider c){
				Vector3 otherDirection = transform.position - c.transform.position;

				parentControl.TakeDamage (otherDirection);
		}

	/**
	 * Tells the shooting radius whether it can pay attention
	 * **/
	public void CanShoot(bool canShoot){
				GetComponentInChildren<EnemyShootingController> ().CanShoot (canShoot);
		}

	/**
	 * Tells the parent to fire the projectile
	 * **/
	public void FireProjectile(Vector3 targetPosition){
				parentControl.FireProjectile (targetPosition);
		}

	/**
	 * Enter cutscene
	 * **/
	public void EnterCutscene () {
				InCutscene = true;
		}

	/**
	 * Exits the cutscene
	 * **/
	public void ExitCutscene () {
				InCutscene = false;
		}
}
