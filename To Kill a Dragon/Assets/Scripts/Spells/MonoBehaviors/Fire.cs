using UnityEngine;
using System.Collections;

public class Fire : MonoBehaviour {

	private int degree;
	private Vector3 NewPosition;

	public int Timer;

	private Animator _animator;

	private FireDestroy FD;

	// Use this for initialization
	void Start () {
		degree = (int)transform.rotation.eulerAngles.z/90;
		NewPosition = Vector3.zero;
		switch (degree) {
		case 0:
				NewPosition = new Vector3 (0, 0, -1);
				break;
		case 1:
				NewPosition = new Vector3 (1, 0, 0);
				break;
		case 2:
				NewPosition = new Vector3 (0, 0, 1);
				break;
		case 3:
				NewPosition = new Vector3 (-1, 0, 0);
				break;
		}

		_animator = GetComponent<Animator> ();

		FD = GetComponent<FireDestroy> ();
		FD.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += NewPosition / 3;
		Timer--;
		if (Timer <= 0) {
			_animator.SetTrigger("Destroy");
			FD.enabled = true;
			Destroy (this);
		}
	}

	void OnTriggerEnter(Collider c){
				if (!(c.CompareTag ("Player") || c.name.Equals (name) || c.CompareTag ("SpellIgnore") || c.tag.Substring (0,4).Equals("Item"))) {
						_animator.SetTrigger ("Destroy");
						FD.enabled = true;
						Destroy (this);
				}
		}
}
