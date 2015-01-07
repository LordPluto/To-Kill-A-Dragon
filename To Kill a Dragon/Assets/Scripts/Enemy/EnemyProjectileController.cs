using UnityEngine;
using System.Collections;

public class EnemyProjectileController : MonoBehaviour {

	private Vector3 destination;

	private GameController gameControl;

	public float moveSpeed;
	public float damage;

	// Use this for initialization
	void Start () {
				destination = GameObject.Find ("Player").transform.position;
				destination.y = transform.position.y;

				if (damage < 0) {
						damage = 0;
				}
				if (moveSpeed < 10) {
						moveSpeed = 10;
				} else if (moveSpeed > 20) {
						moveSpeed = 20;
				}
		}

	void Awake () {
				gameControl = GameObject.Find ("_GameController").GetComponent<GameController> ();
		}
	
	// Update is called once per frame
	void Update () {
				MoveTowardDestination ();

				if ((destination - transform.position).sqrMagnitude < Mathf.Pow ((float).5, 2)) {
						Destroy (gameObject);
				}
		}

	/**
	 * Moves toward the set destination.
	 * **/
	private void MoveTowardDestination () {
				Vector3 direction = destination - transform.position;
				Vector3 moveVector = direction.normalized * moveSpeed * Time.deltaTime;
		
				Ray ray = new Ray (transform.position, direction);
				RaycastHit hit;
		
				if (Physics.Raycast (ray, out hit, moveVector.magnitude) && (hit.collider.CompareTag ("Level") || (hit.collider.tag.Substring (0, 5).Equals ("Spell") &&
						!hit.collider.CompareTag ("SpellIgnore")))) {
						Destroy (gameObject);
				} else {
						transform.position += moveVector;
				}
		}

	void OnTriggerEnter (Collider c) {
				if (c.CompareTag ("Player")) {
						gameControl.DealPlayerBulletDamage (damage, c.transform.position - transform.position);
						Destroy (gameObject);
				}
		}
}
