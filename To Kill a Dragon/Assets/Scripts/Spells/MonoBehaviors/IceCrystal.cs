using UnityEngine;
using System.Collections;

public class IceCrystal : MonoBehaviour {

	public int Life;
	public int Speed;

	private Vector3 random;

	public Sprite[] crystals;

	// Use this for initialization
	void Start () {
		GetComponent<SpriteRenderer> ().sprite = crystals [Random.Range (0, crystals.Length)];

		random = new Vector3 (Random.Range (0, 2*Speed + 1) - Speed, 0, Random.Range (0, 2*Speed + 1) - Speed);
	}
	
	// Update is called once per frame
	void Update () {
		Life--;
		if (Life <= 0) {
			Destroy (gameObject);
		}

		transform.position += random/10;
	}
}
