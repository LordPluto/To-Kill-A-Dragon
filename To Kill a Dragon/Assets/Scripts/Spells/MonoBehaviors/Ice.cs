using UnityEngine;
using System.Collections;

public class Ice : MonoBehaviour {

	public Transform IceCrystal;
	private Animator _animator;

	private int degree;

	private int timer;

	// Use this for initialization
	void Start () {
		timer = 5;
	}
	
	// Update is called once per frame
	void Update () {
		bool destroyMe = Input.GetAxis ("CastSpell") < 0.01;

		if (destroyMe) {
			Destroy (gameObject);
		}

		timer--;

		if (timer <= 0) {
			timer = 5;

			int degree = (4 + (180 - (int)transform.rotation.eulerAngles.y)/90) % 4;

			Vector3 random = Vector3.zero;

			switch(degree){
			case 0:
				random = new Vector3(Random.Range(-1,2),
			                             0,
			                             Random.Range(-1,-3));
				break;
			case 1:
				random = new Vector3(Random.Range(1,3),
			                             0,
			                             Random.Range(-1,2));
				break;
			case 2:
				random = new Vector3(Random.Range(-1,2),
			                             0,
			                             Random.Range(1,3));
				break;
			case 3:
				random = new Vector3(Random.Range(-1,-3),
				                     0,
				                     Random.Range(-1,2));
				break;
			}

			Instantiate (IceCrystal, this.transform.position + random, this.transform.rotation);
		}
	}

	void OnTriggerEnter(Collider c){
		if(!c.gameObject.CompareTag("Player") || c.gameObject.CompareTag("SpellIgnore")){

		}
	}
}
