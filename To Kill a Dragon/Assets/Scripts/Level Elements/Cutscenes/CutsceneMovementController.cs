using UnityEngine;
using System.Collections;

public class CutsceneMovementController : CutsceneEvent {

	public CutscenePathManager[] paths;
	public string[] participantNames;

	private int participantCount;

	// Use this for initialization
	void Start () {
		if (paths.Length != participantNames.Length) {
			Debug.LogError ("Error with cutscene movement controller (ID " + this.GetInstanceID () + "): "
			+ "Number of participants does not match number of paths given.");
		}
	}

	void Awake () {
		this.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (participantCount <= 0) {
			MyManager.EndEvent ();
			this.enabled = false;
		}
	}

	override public void Execute(CutsceneManager Manager) {
		participantCount = participantNames.Length;

		for (int i = 0; i < participantCount; i++) {
			paths [i].BeginPath (this, participantNames [i]);
		}

		MyManager = Manager;
		this.enabled = true;
	}

	public void NotifyPathComplete() {
		--participantCount;
	}
}
