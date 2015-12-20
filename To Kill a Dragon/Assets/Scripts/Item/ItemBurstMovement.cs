using UnityEngine;
using System.Collections;

public class ItemBurstMovement : MonoBehaviour {

	private float BurstTime;
	private float counter;

	private double xInc;
	private double zInc;

	public double yInc;
	private double grav;

	// Use this for initialization
	void Start () {
				float burstDistance = Random.Range (1, 6);
				float direction = Random.Range (0, 359);

				BurstTime = Random.Range (30, 60);
				counter = 0;

				float distanceX = Mathf.Cos (direction) * burstDistance;
				float distanceZ = Mathf.Sin (direction) * burstDistance;

				//Check here to see if it clips through the wall
				Vector3 vectorDirection = new Vector3 (distanceX, 0, distanceZ);
		
				Ray ray = new Ray (transform.position, vectorDirection);
				RaycastHit hit;
		
				if (Physics.Raycast (ray, out hit, vectorDirection.magnitude, (1 << 13 | 1 << 15))) {
						distanceX = (hit.point.x - transform.position.x);
						distanceZ = (hit.point.z - transform.position.z);
						distanceX += (distanceX < 0 ? GetComponent<Collider>().bounds.size.x : -GetComponent<Collider>().bounds.size.x);
						distanceZ += (distanceZ < 0 ? GetComponent<Collider>().bounds.size.z : -GetComponent<Collider>().bounds.size.z);
				}

				xInc = (double)distanceX / (double)BurstTime;
				zInc = (double)distanceZ / (double)BurstTime;

				grav = -yInc / (double)(BurstTime / 2);
		}
	
	// Update is called once per frame
	void Update () {
				counter++;

				yInc += grav;

				Vector3 change = new Vector3 ((float)xInc, (float)yInc, (float)zInc);

				transform.position += change;

				if (counter >= BurstTime - 1) {
						this.enabled = false;
				}
		}
}
