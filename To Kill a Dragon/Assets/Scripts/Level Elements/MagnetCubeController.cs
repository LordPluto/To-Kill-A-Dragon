using UnityEngine;
using System.Collections;

public class MagnetCubeController : MonoBehaviour {

	private bool magnetActive;
	private Vector3 magnetDirection;

	// Use this for initialization
	void Start () {
		magnetActive = false;
		magnetDirection = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
				if (magnetActive) {
						Vector3 moveVector = magnetDirection * 4 * Time.deltaTime;

						Ray ray = new Ray (transform.position, moveVector);
						RaycastHit hit;

						if (Physics.Raycast (ray, out hit, 0.5f, 1 << 14)) {
								hit.collider.gameObject.GetComponent<EnemyPlayerCollision> ().HitMagnetBlock (this.transform.position);
						}
			
						if (Physics.Raycast (ray, out hit, 0.5f, (1 << 9 | 1 << 12 | 1 << 13))) {
								return;
						}
						transform.position += moveVector;
				}
		}

	/**
	 * Handles movement when the magnet spell is active
	 * **/
	public void MagnetMovement (Vector3 magnetDirection){
				if (!magnetActive) {
						magnetActive = true;
						this.magnetDirection = magnetDirection;
				}
		}

	/**
	 * Stops once magnet is no longer active
	 * **/
	public void MagnetStop () {
				magnetActive = false;
				magnetDirection = Vector3.zero;
		}

	/**
	 * Damages enemies on colision.
	 * **/
	void OnTriggerEnter (Collider c){
		if (magnetActive && c.tag.Substring (0, 5).Equals ("Enemy")) {
			c.gameObject.GetComponent<EnemyPlayerCollision> ().HitMagnetBlock (this.transform.position);
		}
	}
}
