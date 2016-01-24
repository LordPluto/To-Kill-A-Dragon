using UnityEngine;
using System.Collections;

public class Ice : AttackSpell {

	public Transform IceCrystal;

	private Animator _animator;

	// Use this for initialization
	void Start () {
		int NumCrystals = Random.Range (5, 8);

		for (int i = 0; i < NumCrystals; i++) {
			Instantiate (IceCrystal,
				transform.position,
				transform.rotation);
		}
	}

	void Awake () {
		_animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (_animator.GetCurrentAnimatorStateInfo (0).IsName ("Dead")) {
			Destroy (gameObject);
		}
	}
}
