using UnityEngine;
using System.Collections;

public class StatusPanelControl : MenuPanelControl {

	private GameController gameControl;
	public GameObject[] childPanels;

	// Use this for initialization
	void Start () {
		gameControl = GameObject.Find ("_GameController").GetComponent<GameController> ();
	}

	// Update is called once per frame
	void Update () {

	}

	public override void OnActivate ()	{
		if (gameControl == null) {
			gameControl = GameObject.Find ("_GameController").GetComponent<GameController> ();
		}

		SpellSelectStyle curStyle = gameControl.GetCurrentSelectStyle ();
		childPanels [(int)curStyle].SetActive (true);
		childPanels [1 - (int)curStyle].SetActive (false);
	}

	public override void Init () {
		gameControl = GameObject.Find ("_GameController").GetComponent<GameController> ();
	}

	public void UnlockSpell (SpellNumber spell) {
		SpellSelectStyle curStyle = gameControl.GetCurrentSelectStyle ();
		childPanels [1 - (int)curStyle].SetActive (true);

		childPanels [0].GetComponent<StatusWheelControl> ().UnlockNextSlot (spell);
		childPanels [1].GetComponent<StatusQuickControl> ().UnlockNextSlot (spell);

		childPanels [1 - (int)curStyle].SetActive (false);
	}
}
