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
	private GameController gameControl;

	// Use this for initialization
	void Start () {
		gameControl = GameObject.Find ("_GameController").GetComponent<GameController> ();
		EnableStyle (gameControl.GetCurrentSelectStyle ());
	}

	void Awake () {
		DontDestroyOnLoad (transform.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void EnableStyle (SpellSelectStyle style) {
		childPanels [(int)style].SetActive (true);
		childPanels [1 - (int)style].SetActive (false);
		gameControl.UpdateSpellSwitching (style);
	}
}
