using UnityEngine;
using System.Collections;

public class MagnetCubeController : MonoBehaviour {

	private bool magnetActive = false;
	private Vector3 magnetDirection = Vector3.zero;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (magnetActive) {
			Vector3 moveVector = magnetDirection * 4 * Time.deltaTime;

			Vector3 CapsuleVector = new Vector3 (magnetDirection.z * transform.localScale.x, 0, magnetDirection.x * transform.localScale.z);
			Vector3 Point1 = transform.position - CapsuleVector/2, Point2 = transform.position + CapsuleVector/2;
			float Radius = transform.localScale.y / 2;
			RaycastHit hit;

			Debug.DrawLine (transform.position, transform.position + magnetDirection, Color.red, 2);

			if (Physics.CapsuleCast (Point1, Point2, Radius, moveVector, out hit, 0.1f, 1 << 14)) {
				//hit.collider.gameObject.GetComponent<EnemyPlayerCollision> ().HitMagnetBlock (this.transform.position);
			}

			if (Physics.CapsuleCast (Point1, Point2, Radius, moveVector, out hit, 0.2f, (1 << 9 | 1 << 12 | 1 << 13 | 1 << 15))) {
				return;
			}
			transform.position += moveVector;
		}
	}

	/**
	 * <para>Toggles the active state of the Magnet spell.</para>
	 * <param name="activeToggle">Toggles the state of magnet spell</param>
	 * <param name="MagnetDirection">Dictates the direction the cube moves</param>
	 * **/
	public void MagnetToggle(bool activeToggle, Vector3 MagnetDirection) {
		magnetActive = activeToggle;
		magnetDirection = MagnetDirection;
		GetComponent<Rigidbody>().useGravity = !activeToggle;
	}

	/**
	 * Damages enemies on colision.
	 * **/
	void OnTriggerEnter (Collider c){
		if (magnetActive && c.tag.Substring (0, 5).Equals ("Enemy")) {
			//c.gameObject.GetComponent<EnemyPlayerCollision> ().HitMagnetBlock (this.transform.position);
		}
	}
}
