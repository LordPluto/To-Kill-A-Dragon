using UnityEngine;
using System.Collections;

public class IceShotgun : MonoBehaviour {

	public Transform IceCrystal;
	private Animator _animator;
	
	// Use this for initialization
	void Start () {
				_animator = GetComponent<Animator> ();

				int flakes = Random.Range (5, 10);
				for (int i = 0; i<flakes; i++) {
						Instantiate (IceCrystal, this.transform.position, this.transform.rotation);
				}
		}
	
	// Update is called once per frame
	void Update () {
		if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Dead")) {
			Destroy (gameObject);
		}
	}


}
