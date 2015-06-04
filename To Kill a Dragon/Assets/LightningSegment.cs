using UnityEngine;
using System.Collections;

public class LightningSegment : MonoBehaviour {
	
	private float timer = 10;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
				timer--;
				if (timer <= 0) {
						Destroy (gameObject);
				}
		}
}
