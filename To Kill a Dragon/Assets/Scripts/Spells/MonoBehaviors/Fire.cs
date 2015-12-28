using UnityEngine;
using System.Collections;

public class Fire : MonoBehaviour {

	private float degree;
	private Vector3 NewPosition;

	public int Timer;

	private Animator _animator;

	private FireDestroy FD;

	// Use this for initialization
	void Start () {
		degree = (transform.rotation.eulerAngles.z-transform.rotation.eulerAngles.y) * Mathf.Deg2Rad;
		NewPosition = new Vector3 (Mathf.Sin (degree), 0, -Mathf.Cos (degree));

		_animator = GetComponent<Animator> ();

		FD = GetComponent<FireDestroy> ();
		FD.enabled = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.position += NewPosition / 3;
		Timer--;
		if (Timer <= 0) {
			_animator.SetTrigger("Destroy");
			FD.enabled = true;
			Destroy (this);
		}
	}

	void OnTriggerEnter(Collider c){
				if (c.CompareTag ("NPC") || 
						!(c.CompareTag ("Player") || c.name.Equals (name) || c.tag.Contains ("SpellIgnore") || c.tag.Substring (0, 4).Equals ("Item"))) {
						if (_animator == null) {
								GetComponent<Animator> ().SetTrigger ("Destroy");
						} else {
								_animator.SetTrigger ("Destroy");
						}

						if (FD == null) {
								GetComponent<FireDestroy> ().enabled = true;
						} else {
								FD.enabled = true;
						}
						Destroy (this);
				}
		}
}
