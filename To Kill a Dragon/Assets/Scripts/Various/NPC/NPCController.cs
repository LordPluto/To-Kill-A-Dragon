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

	private GameController _controller;

	private Path movementPath;
	private float stepCount;
	private bool Talking;

	// Use this for initialization
	void Start () {
				_animator = GetComponent<Animator> ();
				_controller = GameObject.Find ("_GameController").GetComponent<GameController> ();

				//movementPath = null;

				//This is for testing only. MARKED FOR DELETION
				_animator.SetTrigger ("Orange");
				movementPath = new LoopedPath ();
				movementPath.InsertLineEnd (new Line (180, 5));
				movementPath.InsertLineEnd (new Line (90, 7));
				movementPath.InsertLineEnd (new Line (0, 5));
				movementPath.InsertLineEnd (new Line (270, 7));
				movementPath.Start ();

				//stepCount = -1;
				stepCount = movementPath.GetCurrent ().getLength () * 15;

				Talking = false;

				if (!_animator) {
						Debug.Log ("Serious problem detected.");
				}
		}
	
	// Update is called once per frame
	void FixedUpdate () {
				Vector3 change = new Vector3 ();
				if (movementPath != null && !Talking) {
						if (stepCount < 0) {
								if (movementPath.NextLine ()) {
										stepCount = movementPath.GetCurrent ().getLength () * 15;
								} else {
										change = Vector3.zero;
										_animator.SetFloat ("Speed", 0);
								}
						} else {
								switch (movementPath.GetCurrent ().getDirection ()) {
								case 0:	
										change = new Vector3 (0, 0, -5);
										break;
								case 1:
										change = new Vector3 (5, 0, 0);
										break;
								case 2:
										change = new Vector3 (0, 0, 5);
										break;
								case 3:
										change = new Vector3 (-5, 0, 0);
										break;
								default:
										Debug.Log ("Somehow an impossible thing has happened.");
										break;
								}
								_animator.SetFloat ("Direction", movementPath.GetCurrent ().getDirection ());
								_animator.SetFloat ("Speed", 1);

								Ray ray = new Ray (transform.position, change);
								RaycastHit hit;

								if (Physics.Raycast (ray, out hit, 1) && hit.collider.CompareTag ("Player")) {
										_animator.SetFloat ("Speed", 0);
										change = Vector3.zero;
								}
								
						}		
				} else {
						change = Vector3.zero;
						_animator.SetFloat ("Speed", 0);
				}

				if (change != Vector3.zero && GetComponent<CharacterController> ().SimpleMove (change)) {
						stepCount--;
				}
		}

	void OnMouseDown(){
				GameObject player = GameObject.Find ("Player");
				if (talkTo && Vector3.Distance (this.transform.position, player.transform.position) < distance) {
						_controller.ShowDialogue (name);
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
		}

	/**
	 * Sets the NPC's movement path.
	 * Parameters: Path p - the path to set
	 * Results: movementPath has been set and started; stepCount is greater than zero.
	 * **/
	public void SetPath (Path p) {
				if (p == null) {
						return;
				}

				movementPath = p;
				movementPath.Start ();
				stepCount = movementPath.GetCurrent ().getLength () * 15;
		}
}
