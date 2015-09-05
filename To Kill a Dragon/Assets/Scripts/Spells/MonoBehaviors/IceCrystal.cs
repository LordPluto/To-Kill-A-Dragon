using UnityEngine;
using System.Collections;

public class IceCrystal : MonoBehaviour {

	public int Life;
	public int Speed;

	private Vector3 random;

	public Sprite[] crystals;

	// Use this for initialization
	void Start () {
				int degree = (4 + (180 - (int)transform.rotation.eulerAngles.y) / 90) % 4;
				
				switch (degree) {
				case 0:
						random = new Vector3 (Random.Range (0, 2 * Speed + 1) - Speed,
			                     0,
			                     -(Random.Range (0, Speed + 1)));
						break;
				case 1:
						random = new Vector3 (Random.Range (0, Speed + 1),
			                     0,
			                     Random.Range (0, 2 * Speed + 1) - Speed);
						break;
				case 2:
						random = new Vector3 (Random.Range (0, 2 * Speed + 1) - Speed,
			                     0,
			                     Random.Range (0, Speed + 1));
						break;
				case 3:
						random = new Vector3 (-(Random.Range (0, Speed + 1)),
			                     0,
			                     Random.Range (0, 2 * Speed + 1) - Speed);
						break;
				}
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
