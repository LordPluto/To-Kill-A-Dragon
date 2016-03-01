using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyController : MonoBehaviour, StopOnCutscene {

	#region Movement

	private Vector3 currentPathPoint;
	
	private float moveSpeed = 2;
	private double pointReached = .1;

	private GameObject TrackingTarget;

	private bool Tracking;
	private bool Backtrace;
	private bool Flinching;

	private List<Vector3> BacktracePoints;
	private int backtraceIndex;

	private bool Cutscene;

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
		Flinching = false;

		Cutscene = false;

		BacktracePoints = new List<Vector3> ();
		backtraceIndex = 0;

		BacktracePoints.Add (transform.position);

		NotifyControllerOnCutscene ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
				if (!Cutscene) {
						if (Flinching) {
								FlinchMove ();
						} else if (Tracking) {
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
				if (parentControl.doesWander) {
						if (currentPathPoint == Vector3.zero) {
								parentControl.getNewPoint ();
						} else {
								MoveTowardPoint ();
			
								if (Vector3.Distance (currentPathPoint, transform.position) < (float)pointReached) {
										currentPathPoint = Vector3.zero;
										parentControl.getNewPoint ();
										moveSpeed = 2;
								}
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
								moveSpeed = 2;
						}
				}
		}

	/**
	 * Handles the movement when backtracking
	 * **/
	private void BacktraceMove() {
				if (currentPathPoint == Vector3.zero) {
						backtraceIndex--;
						if (backtraceIndex > 0) {
								currentPathPoint = BacktracePoints [backtraceIndex];
						} else {
								BacktracePoints.Clear ();
								Backtrace = false;
								BacktracePoints.Add (transform.position);
						}
				} else {
						MoveTowardPoint ();
			
						if (Vector3.Distance (currentPathPoint, transform.position) < (float)pointReached) {
								currentPathPoint = Vector3.zero;
								moveSpeed = 2;
						}
				}
		}

	/**
	 * Handles the movement when flinching
	 * **/
	private void FlinchMove() {
				if (currentPathPoint == Vector3.zero) {
						Flinching = false;
						StartBacktrace ();
				} else {
						MoveTowardPoint ();
			
						if (Vector3.Distance (currentPathPoint, transform.position) < (float)pointReached) {
								currentPathPoint = Vector3.zero;
								moveSpeed = 2;
						}
				}
		}
	
	/**
	 * Moves the enemy toward the new point
	 * **/
	private void MoveTowardPoint () {
				Vector3 direction = currentPathPoint - transform.position;
				Vector3 moveVector = direction.normalized * moveSpeed * Time.deltaTime;
		
				if (moveVector != Vector3.zero) {
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
		
						Ray ray = new Ray (transform.position + new Vector3 (0, 0.5f, 0), direction);
						RaycastHit hit;
		
						if (Physics.Raycast (ray, out hit, 0.5f, ~(1 << 11 | 1 << 14))) {
								if (hit.collider.tag.Equals ("Level")) {
										currentPathPoint = BacktracePoints [BacktracePoints.Count - 1];
										return;
								} else {
										parentControl.setSpeed (_animator, 0);
										parentControl.getNewPoint ();
								}
								
						} else {
								parentControl.setSpeed (_animator, 1);
								transform.position += moveVector;
						}
				}
		}

	/**
	 * Sets the new destination
	 * **/
	public void setPathPoint (Vector3 newPoint){
				currentPathPoint = newPoint;
		}

	/**
	 * Shifts to flinch mode.
	 * **/
	private void FlinchMode (){
				Flinching = true;
				BacktracePoints.Add (new Vector3 (transform.position.x, transform.position.y, transform.position.z));
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
				StartBacktrace ();
		}

	/**
	 * Turns on backtrace
	 * **/
	private void StartBacktrace(){
				Backtrace = true;
				backtraceIndex = BacktracePoints.Count - 1;
		}

	/**
	 * <para>Deals Spell damage to the enemy</para>
	 * <param name="SpellDamage">Damage of the spell</param>
	 * <param name="SpellKnockback">Bonus knockback of spell</param>
	 * <param name="SpellPosition">Position of spell that hit</param>
	 * **/
	public void TakeDamage(float SpellDamage, float SpellKnockback, Vector3 SpellPosition) {
		parentControl.HP -= SpellDamage;
		KnockBack (transform.position - SpellPosition, SpellKnockback);
	}

	/**
	 * The monster flinches, moving back.
	 * **/
	public void FlinchBack (Vector3 flinchDirection) {
				flinchDirection = new Vector3 (flinchDirection.x, 0, flinchDirection.z);
						
				SetFlinchPoint (flinchDirection, parentControl.getKnockback ());
		}

	/**
	 * The enemy is knocked back from a spell.
	 * **/
	public void KnockBack (Vector3 flinchDirection, float knockback) {
				flinchDirection = new Vector3 (flinchDirection.x, 0, flinchDirection.z);
		
				SetFlinchPoint (flinchDirection, knockback);

				moveSpeed *= 3;
		}

	/**
	 * The enemy is knocked back from a collision with a magnet.
	 * **/
	public void MagnetKnockBack (Vector3 flinchDirection, float knockback){
				flinchDirection = new Vector3 (flinchDirection.x, 0, flinchDirection.z);
		
				SetMagnetFlinch (flinchDirection, knockback);
		
				moveSpeed *= 3;
		}

	/**
	 * Sets the point for flinching back.
	 * **/
	private void SetFlinchPoint(Vector3 flinchDirection, float knockback){
				Vector3 tempDestination = transform.position + (flinchDirection.normalized * knockback);
				Vector3 direction = tempDestination - transform.position;
		
				Ray ray = new Ray (transform.position, direction);
				RaycastHit hit;
		
				if (!(Physics.Raycast (ray, out hit, direction.magnitude) && (hit.collider.CompareTag ("NPC") || hit.collider.CompareTag ("Level")))) {
						currentPathPoint = tempDestination;
				} else {
						currentPathPoint = hit.point - direction.normalized / 2;
				}

				BacktracePoints.Add (new Vector3 (transform.position.x, transform.position.y, transform.position.z));

				FlinchMode ();
		}

	/**
	 * Set magnet flinch
	 * **/
	private void SetMagnetFlinch(Vector3 flinchDirection, float knockback){
				Vector3 tempDestination = transform.position + (flinchDirection.normalized * knockback);
				Vector3 direction = tempDestination - transform.position;
		
				Ray ray = new Ray (transform.position, direction);
				RaycastHit hit;
		
				if (!(Physics.Raycast (ray, out hit, direction.magnitude) && (hit.collider.CompareTag ("NPC") || hit.collider.CompareTag ("Level")))) {
						currentPathPoint = tempDestination;
				} else {
						currentPathPoint = transform.position;
						parentControl.Die ();
				}

				FlinchMode ();
		}

	/**
	 * The enemy hit a moving magnet block
	 * **/
	public void HitMagnetBlock (Vector3 blockPosition){
		Vector3 otherDirection = transform.position - blockPosition;

		parentControl.MagnetDamage (otherDirection);
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
	 * <para>Tells the object not to move during a cutscene</para>
	 * **/
	public void CutsceneFreeze () {
		Cutscene = true;
	}

	/**
	 * <para>Tells the object it can move, cutscene is over</para>
	 * **/
	public void CutsceneMove () {
		Cutscene = false;
	}

	/**
	 * <para>Notifies the controller that a new StopOnCutscene object exists</para>
	 * **/
	public void NotifyControllerOnCutscene() {
		parentControl.GetGameController().AddStopOnCutscene (this);
	}
}