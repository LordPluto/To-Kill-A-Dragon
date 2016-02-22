using UnityEngine;
using System.Collections;

public class Magnet : SupportSpell {

	private GameController gameControl;
	private MagnetCubeController cubeTarget;

	private Vector3 direction;

	// Use this for initialization
	void Start () {
		transform.parent = GameObject.Find ("Player").transform;

		gameControl = GameObject.Find ("_GameController").GetComponent<GameController> ();
		gameControl.SetMagnet (true);

		cubeTarget = null;

		float degree = transform.rotation.eulerAngles.y * Mathf.Deg2Rad;
		direction = new Vector3 (-Mathf.Sin (degree), 0, -Mathf.Cos (degree));

		Vector3 point1 = transform.position + new Vector3 (0, 0.5f, 0), point2 = transform.position - new Vector3 (0, 0.5f, 0);
		float radius = 0.5f;
		RaycastHit hit;

		if (Physics.CapsuleCast (point1, point2, radius, direction, out hit, 24, (1 << 12 | 1 << 13 | 1 << 15))) {
			if (hit.collider.gameObject.CompareTag ("MagnetPillar")) {
				gameControl.SetMagnetDirection (direction);
			} else if (hit.collider.gameObject.CompareTag ("MagnetCube")) {
				gameControl.SetMagnetDirection (hit.collider.gameObject, -direction);
				cubeTarget = hit.collider.gameObject.GetComponent<MagnetCubeController> ();
			}
		}
	}

	void Awake () {
		GameObject[] magnetSpells = GameObject.FindGameObjectsWithTag ("SpellMagnet");

		if (magnetSpells.Length > 1) {
			foreach (GameObject magnet in magnetSpells) {
				if (magnet.GetInstanceID () == this.gameObject.GetInstanceID ()) {
					Destroy (magnet);
				}
			}
		}
	}

	// Update is called once per frame
	void Update () {
		bool destroyMe = Input.GetAxis ("CastSpellSpace") < 0.01 && Input.GetAxis ("CastSpellQ") < 0.01 && Input.GetAxis ("CastSpellE") < 0.01;

		if (destroyMe) {
			if (cubeTarget != null) {
				cubeTarget.MagnetToggle (false, Vector3.zero);
				cubeTarget = null;
			}

			gameControl.SetMagnet (false);
			Destroy (gameObject);
		} else if (cubeTarget == null) {
			Vector3 point1 = transform.position + new Vector3 (0, 0.5f, 0), point2 = transform.position - new Vector3 (0, 0.5f, 0);
			float radius = 0.5f;
			RaycastHit hit;

			if (Physics.CapsuleCast (point1, point2, radius, direction, out hit, 24, (1 << 12 | 1 << 13 | 1 << 15))) {
				if (hit.collider.gameObject.CompareTag ("MagnetPillar")) {
					gameControl.SetMagnetDirection (direction);
				} else if (hit.collider.gameObject.CompareTag ("MagnetCube")) {
					gameControl.SetMagnetDirection (hit.collider.gameObject, -direction);
					cubeTarget = hit.collider.gameObject.GetComponent<MagnetCubeController> ();
				}
			}
		}
	}
}
