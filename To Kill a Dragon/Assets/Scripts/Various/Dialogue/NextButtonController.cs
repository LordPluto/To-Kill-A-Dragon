using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NextButtonController : MonoBehaviour {

	private Image image;
	private Animator animator;
	private Button button;

	// Use this for initialization
	void Start () {
		image = GetComponent<Image> ();
		animator = GetComponent<Animator> ();
		button = GetComponent<Button> ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	/**
	 * Turns the button on.
	 * **/
	public void Activate() {
				image.enabled = true;
				animator.enabled = true;
				button.enabled = true;
		}

	/**
	 * Turns the button off.
	 * **/
	public void Deactivate() {
				image.enabled = false;
				animator.enabled = false;
				button.enabled = false;
		}
}
