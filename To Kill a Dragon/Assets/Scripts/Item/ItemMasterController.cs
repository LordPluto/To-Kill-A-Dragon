using UnityEngine;
using System.Collections;

public class ItemMasterController : MonoBehaviour {

	private ItemAttractMovement attractMode;
	private GameController gameControl;

	public float value;

	// Use this for initialization
	void Start () {
				attractMode = GetComponent<ItemAttractMovement> ();
				gameControl = GameObject.Find ("_GameController").GetComponent<GameController> ();

				GetComponent<ItemBurstMovement> ().enabled = true;
		}
	
	// Update is called once per frame
	void Update () {
	
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
	public void AttractMode () {
				attractMode.enabled = true;
		}
}
