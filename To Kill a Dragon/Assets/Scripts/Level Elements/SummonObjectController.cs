using UnityEngine;
using System.Collections;

public class SummonObjectController : MonoBehaviour {

	public GameObject summon;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/**
	 * Summons the object.
	 * **/
	public void SummonObject () {
				summon.SetActive (true);
		}
}
