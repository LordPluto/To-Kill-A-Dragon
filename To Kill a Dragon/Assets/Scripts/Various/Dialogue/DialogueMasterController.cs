﻿using UnityEngine;
using System.Collections;

public class DialogueMasterController : MonoBehaviour {

	private DialogueController text;
	private DialogueImageController image;

	private string textString;
	private Texture tex;

	// Use this for initialization
	void Start () {
				text = GetComponent<DialogueController> ();
				image = GetComponent<DialogueImageController> ();
		}
	
	// Update is called once per frame
	void Update () {
	}

	public void Activate () {
				text.enabled = true;
				text.SetText (textString);
				image.enabled = true;
				image.SetTexture (tex);
		}

	public void Deactivate () {
				text.enabled = false;
				text.SetText ("");
				image.enabled = false;
				image.Wipe ();
		}

	public void SetTexture (Texture t){
				tex = t;
		}

	public void SetText (string tS){
				textString = tS;
		}
}
