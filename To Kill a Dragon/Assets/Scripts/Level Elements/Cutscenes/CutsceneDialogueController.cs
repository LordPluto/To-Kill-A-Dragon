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
	
		}

	override public void Execute() {
		Debug.Log ("Help");
				gameControl.ShowDialogue (NpcName, NpcFlag);
		}
}
