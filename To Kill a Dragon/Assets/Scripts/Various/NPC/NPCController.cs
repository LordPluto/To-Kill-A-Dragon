using UnityEngine;
using System.Collections;

/**
 * NPCController class inherits MonoBehaviour
 * Class that handles the logic for NPC - movement and talking triggers.
 * private Animator _animator: stores the Animator component for ease of use
 * public bool talkTo: whether this NPC can be talked to or not
 * public float distance: distance from which this NPC can be talked to
 * private GameController _controller: Stores the GameController component for ease of use
 * private LoopedPath movementPath: Holds the movement path for the character. Can be null.
 * private float stepCount: Counts the number of steps on the current path.
 * private bool Talking: Locks down movement during a talking cutscene
 * **/
public class NPCController : MonoBehaviour {

	private Animator _animator;

	public bool talkTo;
	public float distance;
	private bool savedTalkState;

	private GameController _controller;

	private bool Talking;

	public bool npcMovement = true;

	public Transform[] pathPoints;
	private Transform currentPathPoint;
	private int pointIndex;

	private float moveSpeed = 3;
	private float pointReached = 1;

	public bool loopPath;

	// Use this for initialization
	void Start () {
				savedTalkState = talkTo;
				_animator = GetComponent<Animator> ();
				_controller = GameObject.Find ("_GameController").GetComponent<GameController> ();

				_animator.SetTrigger ("Orange");

				if (pathPoints.Length <= 0) {
						npcMovement = false;
				}

				if (npcMovement) {

						currentPathPoint = pathPoints [0];
						pointIndex = 0;
				}

				Talking = false;

				if (!_animator) {
						Debug.Log ("Serious problem detected.");
				}
		}
	
	// Update is called once per frame
	void FixedUpdate () {
				if (npcMovement && !Talking) {
						MoveTowardPoint ();
		
						if (Vector3.Distance (currentPathPoint.transform.position, transform.position) < pointReached / 10) {
								++pointIndex;
								if (pointIndex > pathPoints.Length - 1) {
										if (loopPath) {					
												pointIndex = 0;
										} else {
												npcMovement = false;
										}
								}
								currentPathPoint = pathPoints [pointIndex];
						}
				}
		}

	void OnMouseDown(){
				GameObject player = GameObject.Find ("Player");
				if (talkTo && (this.transform.position - player.transform.position).sqrMagnitude < Mathf.Pow(distance,2)) {
						_controller.ShowDialogue (name);
						talkTo = false;
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

				if (direction.z < 0) {
						_animator.SetFloat ("Direction", 0);
				} else if (direction.x > 0) {
						_animator.SetFloat ("Direction", 1);
				} else if (direction.z > 0) {
						_animator.SetFloat ("Direction", 2);
				} else if (direction.x < 0) {
						_animator.SetFloat ("Direction", 3);
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
}
