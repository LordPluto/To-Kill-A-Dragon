using UnityEngine;
using System.Collections;

public class Fire : AttackSpell {
	
	private Vector3 direction;

	private Animator _animator;
	private float timer = 1.5f;
	private bool destroyed = false;

	// Use this for initialization
	void Start () {
		float angle = transform.rotation.eulerAngles.y * Mathf.Deg2Rad;

		direction = new Vector3 (-Mathf.Sin (angle), 0, -Mathf.Cos (angle)) / 3;
	}

	void Awake () {
		_animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!destroyed) {
			transform.position += direction;

			timer -= Time.deltaTime;
			if (timer <= 0) {
				EndFireball ();
			}
		} else if (_animator.GetCurrentAnimatorStateInfo (0).IsName ("Dead")) {
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter (Collider c) {
		if (!c.tag.Contains ("SpellIgnore") && !c.tag.Equals("Player")) {
			EndFireball();
		}
	}

	void EndFireball () {
		destroyed = true;
		_animator.SetBool ("Destroy", true);
		GetComponent<CapsuleCollider> ().enabled = false;
	}
}
