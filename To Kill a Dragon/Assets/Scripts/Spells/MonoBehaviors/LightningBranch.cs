using UnityEngine;
using System.Collections;

public class LightningBranch : MonoBehaviour {

	public Sprite[] Branches;

	// Use this for initialization
	void Start () {
		GetComponent<SpriteRenderer> ().sprite = Branches [Random.Range (0, Branches.Length)];
	}
	
	// Update is called once per frame
	void Update () {
				
		}
}
