using UnityEngine;
using System.Collections;

public abstract class CutsceneEvent : MonoBehaviour {

	private CutsceneManager myManager;
	public CutsceneManager MyManager {
		get {
			return myManager;
		}
		set {
			myManager = value;
		}
	}

	abstract public void Execute (CutsceneManager Manager);
}
