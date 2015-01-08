using UnityEngine;
using System.Collections;

public class WanderController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	/**
	 * Returns a random point from the surface of the plane.
	 * **/
	public Vector3 getRandomPoint() {
				float minX = renderer.bounds.center.x - renderer.bounds.extents.x;
				float maxX = renderer.bounds.center.x + renderer.bounds.extents.x;

				float minZ = renderer.bounds.center.z - renderer.bounds.extents.z;
				float maxZ = renderer.bounds.center.z + renderer.bounds.extents.z;

				float ranX = Random.Range (minX, maxX);
				float ranZ = Random.Range (minZ, maxZ);

				return new Vector3 (ranX, renderer.bounds.center.y, ranZ);
		}
}
