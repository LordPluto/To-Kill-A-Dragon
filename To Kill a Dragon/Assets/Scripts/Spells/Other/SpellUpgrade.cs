using UnityEngine;
using System.Collections;

public class SpellUpgrade : MonoBehaviour {

	public Transform Upgrade;
	public int UpgradeNum;
	private GameController gameControl;

	/**
	 * Please note:
	 * 0 - Fire
	 * 1 - Ice
	 * 2 - Lightning
	 * 3 - Heal
	 * 4 - Wind
	 * 5 - Magnet
	 * 6 - Mirror
	 * 7 - Heavy
	 * 8 - Death
	 * 9 - Illumination
	 * **/

	// Use this for initialization
	void Start () {
		gameControl = GameObject.Find ("_GameController").GetComponent<GameController> ();
	}
	
	// Update is called once per frame
	void Update () {
		}

	public void OnTriggerEnter(Collider c){
				if (c.CompareTag ("Player")) {
						gameControl.SpellUpgrade (Upgrade, UpgradeNum);
				}
		}
}
