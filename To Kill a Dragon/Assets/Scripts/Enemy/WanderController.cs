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
				float minX = GetComponent<Renderer>().bounds.center.x - GetComponent<Renderer>().bounds.extents.x;
				float maxX = GetComponent<Renderer>().bounds.center.x + GetComponent<Renderer>().bounds.extents.x;

				float minZ = GetComponent<Renderer>().bounds.center.z - GetComponent<Renderer>().bounds.extents.z;
				float maxZ = GetComponent<Renderer>().bounds.center.z + GetComponent<Renderer>().bounds.extents.z;

				float ranX = Random.Range (minX, maxX);
				float ranZ = Random.Range (minZ, maxZ);

				return new Vector3 (ranX, GetComponent<Renderer>().bounds.center.y, ranZ);
		}
}
