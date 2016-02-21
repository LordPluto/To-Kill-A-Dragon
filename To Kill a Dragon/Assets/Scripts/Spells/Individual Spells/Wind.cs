using UnityEngine;
using System.Collections;

public class Wind : SelfSpell {

	[Range (1, 10)]
	public float BoostDuration=5;

	private float timer;
	private PlayerMasterController playerMaster;

	// Use this for initialization
	void Start () {
		GameObject player = GameObject.Find ("Player");
		playerMaster = player.GetComponent<PlayerMasterController> ();

		transform.SetParent (player.transform);

		playerMaster.ToggleWindBoost (true);
	}

	void Awake () {
		timer = BoostDuration;
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		if (timer <= 0) {
			playerMaster.ToggleWindBoost (false);
			Destroy (gameObject);
		}
	}

	/**
	 * <para>Renews the duration of the wind spell.</para>
	 * **/
	public void RenewDuration() {
		timer = BoostDuration;
	}
}
