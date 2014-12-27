﻿using UnityEngine;
using System.Collections;

public class LightningChain : MonoBehaviour {

	public Transform Chain;

	private int degree;
	private int Timer;

	private float distanceModifier;

	private PlayerMasterController playerControl;
	
	// Use this for initialization
	void Start () {
				degree = (4 + ((int)transform.rotation.eulerAngles.y) / -90) % 4;

				Timer = 1;

				distanceModifier = GetComponent<SpriteRenderer> ().sprite.rect.height / 100;

				playerControl = GameObject.Find ("Player").GetComponent<PlayerMasterController> ();
		}
	
	// Update is called once per frame
	void Update () {
		if (Timer > 0) {
			Timer--;
		}
		else if (Timer == 0){
			Vector3 newPosition = transform.position;
			
			switch (degree) {
			case 0:
				newPosition.z -= distanceModifier;
				
				break;
			case 1:
				newPosition.x += distanceModifier;
				
				break;
			case 2:
				newPosition.z += distanceModifier;
				
				break;
			case 3:
				newPosition.x -= distanceModifier;
				
				break;
			}
			
			Instantiate (Chain,
			             newPosition,
			             transform.rotation);

			Timer--;
		}
	}

	void OnTriggerEnter (Collider c) {
		if(!(c.gameObject.CompareTag("Player") || c.gameObject.tag.Equals("LightningChain"))){
			tag = "Untagged";
			GameObject[] links = GameObject.FindGameObjectsWithTag("LightningChain");
			for(int i = links.Length - 1;i>=0;i--){
				Destroy (links[i]);
			}

			playerControl.LightningReset();

			Destroy (gameObject);
		}
	}
}