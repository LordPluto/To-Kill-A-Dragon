using UnityEngine;
using System.Collections;

public class Heal : MonoBehaviour {

	private int Timer = 100;
	private PlayerMasterController playerControl;

	private float percentageHealed;

	// Use this for initialization
	void Start () {
				playerControl = GameObject.Find ("Player").GetComponent<PlayerMasterController> ();
				percentageHealed = 50;
		}
	
	// Update is called once per frame
	void Update () {
				Timer--;

				transform.Rotate (new Vector3 (0, 9, 0));

				if (Timer < 0) {
						DestroyThis ();
				}
		}

	private void DestroyThis(){
				playerControl.HealSpell (percentageHealed);
				Destroy (gameObject);
		}
}
