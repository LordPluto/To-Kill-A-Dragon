using UnityEngine;
using System.Collections;

public class FireDestroy : MonoBehaviour {

	private Animator _animator;
	private Animator playerAnimator;

	// Use this for initialization
	void Start () {
		_animator = GetComponent<Animator> ();
		playerAnimator = GameObject.Find ("Player").GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Dead")) {
			playerAnimator.SetBool ("AttackSpell", false);
			Destroy (gameObject);
		}
	}
}
