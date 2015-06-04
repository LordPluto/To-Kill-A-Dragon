﻿using UnityEngine;
using System.Collections;

public class ItemAttractMovement : MonoBehaviour {

	private PlayerMasterController playerMaster;

	// Use this for initialization
	void Start () {
				playerMaster = GameObject.Find ("Player").GetComponent<PlayerMasterController> ();
				this.enabled = false;
		}
	
	// Update is called once per frame
	void Update () {
				Vector3 direction = playerMaster.transform.position - transform.position;
				Vector3 moveVector = direction.normalized * Time.deltaTime * 3;
		
				Ray ray = new Ray (transform.position, direction);
				RaycastHit hit;
		
				if (Physics.Raycast (ray, out hit, moveVector.magnitude) && hit.collider.CompareTag ("Level")) {
						return;
				} else {
						transform.position += moveVector;
				}
		}
}
