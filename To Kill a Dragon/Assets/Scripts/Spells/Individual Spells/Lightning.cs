using UnityEngine;
using System.Collections;

public class Lightning : AttackSpell {

	public Transform LightningBranch;

	private float LIGHTNING_LENGTH = 5.04f;
	private float timer = 0.3f;

	// Use this for initialization
	void Start () {
		float angle = (180 - transform.rotation.eulerAngles.y) * Mathf.Deg2Rad;

		Vector3 direction = new Vector3 (-Mathf.Sin (angle), 0, -Mathf.Cos (angle));

		Ray ray = new Ray (transform.position, direction);
		RaycastHit hit;

		float distance = 10.0f * LIGHTNING_LENGTH;
		if (Physics.Raycast (ray, out hit, distance, ~(1 << 2 | 1 << 11))) {
			distance = hit.distance;
		}

		float i = 0;
		if (distance > LIGHTNING_LENGTH) {
			for (i=0; i < distance - LIGHTNING_LENGTH; i += LIGHTNING_LENGTH) {
				Instantiate (LightningBranch,
					transform.position + i * direction,
					transform.rotation);
			}
		}

		//Last segment
		distance -= i;
		Vector3 SavedScale = LightningBranch.localScale;
		LightningBranch.localScale = new Vector3(SavedScale.x, SavedScale.y * distance / LIGHTNING_LENGTH + 0.05f, SavedScale.z);

		Instantiate (LightningBranch,
			transform.position + i * direction,
			transform.rotation);

		LightningBranch.localScale = SavedScale;
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		if (timer <= 0) {
			DestroyLightning ();
		}
	}

	void DestroyLightning () {
		GameObject[] lightning = GameObject.FindGameObjectsWithTag ("SpellLightning");

		foreach (GameObject gO in lightning) {
			Destroy (gO);
		}

		Destroy (gameObject);
	}
}
