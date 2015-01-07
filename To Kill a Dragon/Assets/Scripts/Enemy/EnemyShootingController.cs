using UnityEngine;
using System.Collections;

public class EnemyShootingController : MonoBehaviour {

	private bool canShoot;

	private EnemyController parentControl;

	// Use this for initialization
	void Start () {
		parentControl = GetComponentInParent<EnemyController> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider c){
				if (canShoot && c.CompareTag ("Player")) {
						parentControl.FireProjectile (c.transform.position);
				}
		}

	/**
	 * Sets the shooting boolean
	 * **/
	public void CanShoot(bool fireControl){
				canShoot = fireControl;
		}
}
