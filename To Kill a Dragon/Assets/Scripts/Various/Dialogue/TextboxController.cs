using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextboxController : MonoBehaviour {

	private RawImage image;
	
	// Use this for initialization
	void Awake () {
				image = GetComponent<RawImage> ();
		}
	
	// Update is called once per frame
	void Update () {
		}

	public void Activate () {
				image.enabled = true;
		}

	public void Deactivate () {
				image.enabled = false;
		}
}