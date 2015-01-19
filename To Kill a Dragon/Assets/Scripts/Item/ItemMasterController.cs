using UnityEngine;
using System.Collections;

public class ItemMasterController : MonoBehaviour {

	private ItemAttractMovement attractMode;

	// Use this for initialization
	void Start () {
				attractMode = GetComponent<ItemAttractMovement> ();
		}
	
	// Update is called once per frame
	void Update () {
	
		}

	void OnTriggerEnter(Collider c){
				if (c.CompareTag ("Player")) {
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
