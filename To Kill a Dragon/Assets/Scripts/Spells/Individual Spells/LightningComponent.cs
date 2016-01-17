using UnityEngine;
using System.Collections;

public class LightningComponent : AttackSpell {

	public Sprite[] LightningBranches;

	// Use this for initialization
	void Start () {
		
	}

	void Awake () {
		foreach (SpriteRenderer rend in GetComponentsInChildren<SpriteRenderer>()) {
			if (!rend.Equals (GetComponent<SpriteRenderer> ())) {
				rend.sprite = LightningBranches [Random.Range (0, LightningBranches.Length-1)];
				break;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
