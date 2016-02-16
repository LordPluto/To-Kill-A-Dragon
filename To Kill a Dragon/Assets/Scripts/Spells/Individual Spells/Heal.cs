using UnityEngine;
using System.Collections;

public class Heal : SelfSpell {

	[Range (0.0f, 1.0f)]
	public float percentageHealed;

	private float timer;

	private GameController gameControl;

	// Use this for initialization
	void Start () {
		gameControl = GameObject.Find ("_GameController").GetComponent<GameController> ();
	}

	void Awake () {
		timer = CastDuration;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (new Vector3 (0, 360 * Time.deltaTime, 0));

		timer -= Time.deltaTime;
		gameControl.HealPlayerHealthPercent (percentageHealed * (Time.deltaTime / CastDuration));

		if (timer <= 0) {
			Destroy (gameObject);
		}
	}
}
