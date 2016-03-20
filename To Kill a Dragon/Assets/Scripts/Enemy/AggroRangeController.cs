using UnityEngine;
using System.Collections;

public class AggroRangeController : MonoBehaviour {

	private EnemyController notifyControl;

	public bool CanAggro;

	// Use this for initialization
	void Start () {
				notifyControl = GetComponentInParent<EnemyController> ();
		}
	
	// Update is called once per frame
	void Update () {
	
		}

	void OnTriggerEnter (Collider c){
				if (CanAggro && c.CompareTag ("Player")) {
						notifyControl.SetTarget (c.gameObject);
				}
		}

	void OnTriggerExit (Collider c){
				if (CanAggro && c.CompareTag ("Player")) {
						notifyControl.LoseTarget ();
				}
		}
}
