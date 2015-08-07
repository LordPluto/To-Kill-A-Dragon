using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DialogueImageController : MonoBehaviour {

	private Image image;
	private Animator animator;

	// Use this for initialization
	void Awake () {
				image = GetComponent<Image> ();
				animator = GetComponent<Animator> ();
		}
	
	// Update is called once per frame
	void Update () {
		}

	public void Wipe () {
				image.enabled = false;
				animator.enabled = false;
		}

	public void SetHead (string imageName) {
				image.enabled = true;
				animator.enabled = true;
				animator.Play (imageName);
		}
}
