using UnityEngine;
using System.Collections;

public class VictorController : MonoBehaviour {

	#region Victor's Flags

	public bool orange;

	#endregion

	private Animator _animator;

	// Use this for initialization
	void Start () {
				_animator = GetComponent<Animator> ();
				if (orange) {
						_animator.SetTrigger ("Orange");
				}
		}
	
	// Update is called once per frame
	void Update () {
	
	}

	/**
	 * Sets Victor to Orange
	 * **/
	public void SetOrange() {
				orange = true;
				_animator.SetTrigger ("Orange");
		}
}
