using UnityEngine;
using System.Collections;

/**
 * NPCController class inherits MonoBehaviour
 * Class that handles the logic for NPC - movement and talking triggers.
 * 
 * private Animator _animator: stores the Animator component for ease of use
 * private GameController _controller: stores the GameController component for efficiency
 * 
 * public bool talkTo: whether this NPC can be talked to or not
 * public float distance: distance from which this NPC can be talked to
 * private bool savedTalkState: the state of talking the NPC was in before talking. To stop the player from stacking the text boxes.
 * private bool Talking: Locks down movement during a talking cutscene
 * 
 * public bool npcMovement: whether the NPC moves.
 * public Transform[] pathPoints: the points the NPC moves to.
 * private Transform currentPathPoint: what point the NPC is moving to.
 * private int pointIndex: stores the number of the point the NPC is moving to.
 * private float moveSpeed: move speed constant.
 * private double pointReached: distance check constant.
 * public bool loopPath: whether the NPC loops their path
 * **/
public class NPCController : MonoBehaviour {

	#region Components

	private Animator _animator;
	private GameController _controller;

	#endregion

	#region Talking

	public bool talkTo;
	public float distance;
	private bool savedTalkState;

	private bool Talking;

	#endregion

	#region Movement

	public bool npcMovement = true;

	public Transform[] pathPoints;
	private Transform currentPathPoint;
	private int pointIndex;

	private float moveSpeed = 3;
	private double pointReached = .1;

	public bool loopPath;

	#endregion

	#region Cutscene

	private bool Cutscene;

	private Transform[] cutscenePathPoints;
	private Transform cutscenePathPoint;
	private int cutsceneIndex;

	#endregion

	// Use this for initialization
	void Start () {
				savedTalkState = talkTo;
				_animator = GetComponent<Animator> ();
				_controller = GameObject.Find ("_GameController").GetComponent<GameController> ();

				if (pathPoints.Length <= 0) {
						npcMovement = false;
				}

				if (npcMovement) {

						currentPathPoint = pathPoints [0];
						pointIndex = 0;
				}

				Talking = false;
				Cutscene = false;

				if (!_animator) {
						Debug.Log ("Serious problem detected.");
				}
		}
	
	// Update is called once per frame
	void FixedUpdate () {
				if (Cutscene) {
						CutsceneUpdate ();
				} else {
						PathUpdate ();
				}
		}

	/**
	 * Handles the regular movement.
	 * **/
	private void PathUpdate() {
				if (npcMovement && !Talking) {
						MoveTowardPoint ();
		
						if (Vector3.Distance (currentPathPoint.transform.position, transform.position) < (float) pointReached) {
								++pointIndex;
								if (pointIndex > pathPoints.Length - 1) {
										if (loopPath) {					
												pointIndex = 0;
										} else {
												npcMovement = false;
										}
								}

								if (npcMovement) {
										currentPathPoint = pathPoints [pointIndex];
								}
						}
				}
		}

	/**
	 * Handles the cutscene movement.
	 * **/
	private void CutsceneUpdate() {
				MoveTowardCutscenePoint ();
			
				if (Vector3.Distance (cutscenePathPoint.transform.position, transform.position) < (float) pointReached) {
						++cutsceneIndex;
						if (cutsceneIndex > cutscenePathPoints.Length - 1) {
								_controller.NPCFinishedCutscene ();
								Cutscene = false;
						} else {
								currentPathPoint = pathPoints [pointIndex];
						}
				}
		}

	/**
	 * When the NPC is clicked, check to see if you can talk to him
	 * **/
	void OnMouseDown(){
				GameObject player = GameObject.Find ("Player");
				if (talkTo && (this.transform.position - player.transform.position).sqrMagnitude < Mathf.Pow (distance, 2)) {
						Collider[] hitColliders = Physics.OverlapSphere (player.transform.position, distance + 2, 1 << 14);
						if (hitColliders.Length == 0) {
								_controller.ShowDialogue (name);
								talkTo = false;
						}
				}
		}

	/**
	 * Tells the NPC to stop moving
	 * Results: Talking = false;
	 * **/
	public void TalkingFreeze () {
				Talking = true;
		}

	/**
	 * Tells the NPC to start moving
	 * Results: Talking = true;
	 * **/
	public void TalkingMove () {
				Talking = false;
				talkTo = savedTalkState;
		}

	/**
	 * Moves the NPC toward the point; if there's something in the way (a player or a wall) it stops.
	 * **/
	private void MoveTowardPoint () {
				Vector3 direction = currentPathPoint.transform.position - transform.position;
				Vector3 moveVector = direction.normalized * moveSpeed * Time.deltaTime;

				float directionAngle = (Mathf.Atan2 (direction.z, direction.x) * Mathf.Rad2Deg + 360) % 360;
		
				if (directionAngle > 45 && directionAngle <= 135) {
						_animator.SetFloat ("Direction", 2);
				} else if (directionAngle > 135 && directionAngle <= 225) {
						_animator.SetFloat ("Direction", 3);
				} else if (directionAngle > 225 && directionAngle <= 315) {
						_animator.SetFloat ("Direction", 0);
				} else if (directionAngle > 315 || directionAngle <= 45) {
						_animator.SetFloat ("Direction", 1);
				}

				Ray ray = new Ray (transform.position, direction);
				RaycastHit hit;

				if (Physics.Raycast (ray, out hit, moveVector.magnitude) && (hit.collider.CompareTag ("Player") || hit.collider.CompareTag ("Level"))) {
						_animator.SetFloat ("Speed", 0);
						return;
				} else {
						_animator.SetFloat ("Speed", 1);
						transform.position += moveVector;
				}
		}

	/**
	 * Moves the NPC toward the cutscene point; if there's something in the way (a player or a wall) it stops.
	 * **/
	private void MoveTowardCutscenePoint () {
				Vector3 direction = cutscenePathPoint.transform.position - transform.position;
				Vector3 moveVector = direction.normalized * moveSpeed * Time.deltaTime;
			
				float directionAngle = (Mathf.Atan2 (direction.z, direction.x) * Mathf.Rad2Deg + 360) % 360;
			
				if (directionAngle > 45 && directionAngle <= 135) {
						_animator.SetFloat ("Direction", 2);
				} else if (directionAngle > 135 && directionAngle <= 225) {
						_animator.SetFloat ("Direction", 3);
				} else if (directionAngle > 225 && directionAngle <= 315) {
						_animator.SetFloat ("Direction", 0);
				} else if (directionAngle > 315 || directionAngle <= 45) {
						_animator.SetFloat ("Direction", 1);
				}
			
				Ray ray = new Ray (transform.position, direction);
				RaycastHit hit;
			
				if (Physics.Raycast (ray, out hit, moveVector.magnitude) && (hit.collider.CompareTag ("Player") || hit.collider.CompareTag ("Level"))) {
						_animator.SetFloat ("Speed", 0);
						return;
				} else {
						_animator.SetFloat ("Speed", 1);
						transform.position += moveVector;
				}
		}

	/**
	 * Enters the cutscene. Basically tells the system to use a different set of points.
	 * **/
	public void EnterCutscene(Transform[] CutscenePoints){
				Cutscene = true;
				cutscenePathPoints = CutscenePoints;
				cutscenePathPoint = cutscenePathPoints [0];
				cutsceneIndex = 0;
		}
}
