using UnityEngine;
using System.Collections;

public class ItemMasterController : MonoBehaviour {

	private ItemAttractMovement attractMode;
	private ItemBurstMovement burstMode;
	private GameController gameControl;

	public float value;
	public bool magnetTarget;

	// Use this for initialization
	void Start () {
		gameControl = GameObject.Find ("_GameController").GetComponent<GameController> ();				
	}

	void Awake () {
		attractMode = GetComponent<ItemAttractMovement> ();
		burstMode = GetComponent<ItemBurstMovement> ();
		GetComponent<ItemBurstMovement> ().enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (magnetTarget) {
			if (!attractMode.enabled && gameControl.MagnetActive) {
				AttractMode ();
			} else if (attractMode.enabled && !gameControl.MagnetActive) {
				StationaryMode ();
			}
		}

		if (attractMode.enabled) {
			attractMode.IsMagnet (gameControl.MagnetActive);
		}
	}

	void OnTriggerEnter(Collider c){
				if (c.CompareTag ("Player")) {
						gameControl.ItemCollected (tag, value);
						Destroy (gameObject);
				}
		}

	/**
	 * Shifts the item's mode to slight magnetism toward the player
	 * **/
	private void AttractMode () {
				attractMode.enabled = true;
		}

	/**
	 * Shifts the item's mode to not moving.
	 * **/
	private void StationaryMode () {
				attractMode.enabled = false;
		}

	/**
	 * Shifts the item's mode into permanent magnetism.
	 * **/
	public void AttractModeTouch () {
		if (burstMode.enabled != true) {
			attractMode.enabled = true;
			magnetTarget = false;
		}
	}
	
	/**
	 * Returns the value of the object
	 * **/
	public float GetValue(){
		return value;
	}
}
