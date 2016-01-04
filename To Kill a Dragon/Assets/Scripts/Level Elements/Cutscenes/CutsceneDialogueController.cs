using UnityEngine;
using System.Collections;

public class CutsceneDialogueController : CutsceneEvent {

	public string NpcName;
	public int NpcFlag;

	private GameController gameControl;

	// Use this for initialization
	void Start () {
				gameControl = GameObject.Find ("_GameController").GetComponent<GameController> ();
		}
	
	// Update is called once per frame
	void Update () {
		if (!gameControl.IsDialogueActive ()) {
			MyManager.EndEvent ();
			this.enabled = false;
		}
	}

	override public void Execute(CutsceneManager Manager) {
		if (gameControl == null) {
			gameControl = GameObject.Find ("_GameController").GetComponent<GameController> ();
		}
		MyManager = Manager;
		gameControl.ShowDialogue (NpcName, NpcFlag);

		this.enabled = true;
	}
}
