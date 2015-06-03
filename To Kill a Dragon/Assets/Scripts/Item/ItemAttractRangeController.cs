using UnityEngine;
using System.Collections;

public class ItemAttractRangeController : MonoBehaviour {

	private ItemMasterController itemMaster;

	// Use this for initialization
	void Start () {
				itemMaster = GetComponentInParent<ItemMasterController> ();
		}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter (Collider c) {
				if (c.CompareTag ("Player")) {
						itemMaster.AttractMode ();
				}
		}
}
