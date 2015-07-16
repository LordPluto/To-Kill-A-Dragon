using UnityEngine;
using System.Collections;

public class DeathTrigger : MonoBehaviour {

	private int enemyChildren;

	// Use this for initialization
	void Start () {
				enemyChildren = transform.childCount - 1;
		}
	
	// Update is called once per frame
	void Update () {
	
	}

	/**
	 * Notified when an enemy child dies.
	 * **/
	public void NotifyDeath() {
				enemyChildren--;
				if (enemyChildren <= 0) {
						TriggerEvent ();
				}
		}

	/**
	 * Triggers the event.
	 * **/
	private void TriggerEvent() {
				SummonObjectController summonControl;
				DoorTriggerController doorControl;

				if ((summonControl = GetComponentInChildren<SummonObjectController> ()) != null) {
						summonControl.SummonObject ();
				} else if ((doorControl = GetComponent<DoorTriggerController> ()) != null) {
						doorControl.TriggerDoor ();
				}

				Destroy (gameObject);
		}
}
