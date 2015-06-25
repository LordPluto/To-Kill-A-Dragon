using UnityEngine;
using System.Collections;

public class LightningStrike : MonoBehaviour {

	private Vector3 target;
	public Transform LightningSegment;
	private float segmentLength = 501;

	// Use this for initialization
	void Start () {
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

				Physics.Raycast (ray, out hit, Mathf.Infinity, ~(1 << 11));

		float i = 0;
				if (hit.distance > segmentLength / 100) {

						
						for (i = 0; i<(100 * hit.distance - segmentLength); i+= segmentLength) {
								Instantiate (LightningSegment, this.transform.position + direction * i / 100, this.transform.rotation);
						}
				}

				float percentage = 100 * (hit.distance % (segmentLength / 100)) / segmentLength;

				LightningSegment.localScale = new Vector3 (1,
		                                           (direction.x * percentage == 0 ? 
		 													(direction.z * percentage == 0 ? 1 : Mathf.Abs(direction.z * percentage)) : 
		 													Mathf.Abs(direction.x * percentage)),
		                                          	1);
				
				Instantiate (LightningSegment, this.transform.position + direction * i / 100, this.transform.rotation);

				LightningSegment.localScale = new Vector3 (1, 1, 1);

				Destroy (gameObject);
		}

	
	// Update is called once per frame
	void Update () {
	
	}
}
