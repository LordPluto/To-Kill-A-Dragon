using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public enum SpellSelectStyle {
	Wheel = 0,
	Quick = 1
}

public class SelectStyleControl : MonoBehaviour {

	public GameObject[] childPanels;

	// Use this for initialization
	void Start () {
		foreach (Transform child in transform) {
			if (child.gameObject.name == "WheelSelect Panel") {
				childPanels [(int)SpellSelectStyle.Wheel] = child.gameObject;
			} else if (child.gameObject.name == "QuickSelect Panel") {
				childPanels [(int)SpellSelectStyle.Quick] = child.gameObject;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void EnableStyle (SpellSelectStyle style) {
		childPanels [(int)style].SetActive (true);
		childPanels [1 - (int)style].SetActive (true);
	}
}
