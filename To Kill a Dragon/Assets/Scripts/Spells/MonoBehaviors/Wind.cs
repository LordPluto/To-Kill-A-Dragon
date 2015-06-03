using UnityEngine;
using System.Collections;

public class Wind : MonoBehaviour {

	private PlayerMasterController playerControl;

	private float Timer = 20f;

	// Use this for initialization
	void Start () {
				playerControl = GameObject.Find ("Player").GetComponent<PlayerMasterController> ();
				playerControl.WindBoost (true);
		}
	
	// Update is called once per frame
	void Update () {
				transform.position = playerControl.getPosition ();

				transform.Rotate (new Vector3 (0, 0, 9));

				Timer -= Time.deltaTime;
				if (Timer <= 0) {
						DestroyThis ();
				}
		}

	void DestroyThis () {
				playerControl.WindBoost (false);
				Destroy (gameObject);
		}
}
