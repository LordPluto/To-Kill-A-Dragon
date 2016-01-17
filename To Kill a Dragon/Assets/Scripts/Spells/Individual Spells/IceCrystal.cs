﻿using UnityEngine;
using System.Collections;

public class IceCrystal : MonoBehaviour {

	private float timer = 0.5f;
	private Vector3 direction;

	// Use this for initialization
	void Start () {
	
	}

	void Awake () {
		float angle = (180 - transform.rotation.eulerAngles.y) * Mathf.Deg2Rad;

		direction = new Vector3 (-Mathf.Sin(angle) + Random.Range(-1.0f, 1.0f), 0, -Mathf.Cos(angle) + Random.Range(-1.0f, 1.0f)) / 13;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += direction;
		timer -= Time.deltaTime;
		if (timer <= 0) {
			Destroy (gameObject);
		}
	}
}