using UnityEngine;
using System.Collections;

public class Magnet : MonoBehaviour {

	private GameController gameControl;

	// Use this for initialization
	void Start () {
				gameControl = GameObject.Find ("_GameController").GetComponent<GameController> ();
				gameControl.SetMagnet (true);

				Vector3 direction = Vector3.zero;
				int degree = (4 + (180 - (int)transform.rotation.eulerAngles.y) / 90) % 4;
		
				switch (degree) {
				case 0:
						direction = new Vector3 (0, 0, -1);
						break;
				case 1:
						direction = new Vector3 (1, 0, 0);
						break;
				case 2:
						direction = new Vector3 (0, 0, 1);
						break;
				case 3:
						direction = new Vector3 (-1, 0, 0);
						break;
				}
		
				Ray ray = new Ray (transform.position, direction);
				RaycastHit hit;

				if (Physics.Raycast (ray, out hit, 24, (1 << 12 | 1 << 13))) {
						if (hit.collider.gameObject.CompareTag ("MagnetPillar")) {
								gameControl.SetMagnetDirection (direction);
						}
				}
		}
	
	// Update is called once per frame
	void Update () {
				bool destroyMe = Input.GetAxis ("CastSpell") < 0.01;
		
				if (destroyMe) {
			gameControl.SetMagnet(false);
						Destroy (gameObject);
				}
		}
}
