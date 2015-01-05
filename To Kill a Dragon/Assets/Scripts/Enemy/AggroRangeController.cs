using UnityEngine;
using System.Collections;

public class AggroRangeController : MonoBehaviour {

	private EnemyController notifyControl;

	// Use this for initialization
	void Start () {
				notifyControl = GetComponentInParent<EnemyController> ();
		}
	
	// Update is called once per frame
	void Update () {
	
		}

	void OnTriggerEnter (Collider c){
				if (c.CompareTag ("Player")) {
						notifyControl.SetTarget (c.gameObject);
				}
		}

	void OnTriggerExit (Collider c){
				if (c.CompareTag ("Player")) {
						notifyControl.LoseTarget ();
				}
		}
}
