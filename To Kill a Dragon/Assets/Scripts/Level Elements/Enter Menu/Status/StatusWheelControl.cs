using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class StatusWheelControl : MonoBehaviour {

	private GameController gameControl;
	private StatusWheelIcon currentSelection;

	// Use this for initialization
	void Start () {
		gameControl = GameObject.Find ("_GameController").GetComponent<GameController> ();
	}

	void OnEnable () {
		if (gameControl == null) {
			gameControl = GameObject.Find ("_GameController").GetComponent<GameController> ();
		}
		foreach (StatusWheelIcon s in GetComponentsInChildren<StatusWheelIcon>(true)) {
			s.gameObject.SetActive (gameControl.SpellKnown (s.partnerSelector.Spell));
		}

		if (currentSelection == null) {
			currentSelection = GetComponentInChildren<StatusWheelIcon> ();
		}

		if (currentSelection != null) {
			EventSystem.current.SetSelectedGameObject (currentSelection.gameObject);
		}
	}

	void OnDisable () {
		currentSelection = null;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Q)
			|| Input.GetKeyDown (KeyCode.E)
			|| Input.GetKeyDown (KeyCode.Space)) {
			Equip ();
		}

		if (EventSystem.current.currentSelectedGameObject == null && currentSelection != null) {
			EventSystem.current.SetSelectedGameObject (currentSelection.gameObject);
		}
	}

	/**
	 * <para>Transfers control over to the spell list for selection</para>
	 * **/
	private void Equip() {

	}

	public void TakeControl () {
		EventSystem.current.SetSelectedGameObject (currentSelection.gameObject);
	}

	/**
	 * <para>Sets the new selection</para>
	 * <param name="newSelection">New selected spell</param>
	 * **/
	public void SetSelected (StatusWheelIcon newSelection) {
		currentSelection = newSelection;
	}
}
