using UnityEngine;
using System.Collections;

public class WarpDestinationController : MonoBehaviour {

	public bool forceGrounded;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/**
	 * <para>Retrieves the position of the object.</para>
	 * <para>If the 'Force grounded' flag is checked, Y-position is the ground.</para>
	 * **/
	public Vector3 getPosition(){
		Vector3 returnedPosition = transform.position;
		if (forceGrounded) {
			Ray ray = new Ray (returnedPosition, Vector3.down);
			RaycastHit hit;

			if (Physics.Raycast (ray, out hit, Mathf.Infinity, (1 << 13))) {
				returnedPosition.y = hit.point.y + 0.1f;		//The 0.1f is for smoothing purposes. Makes it look nicer.
			}
		}
		return returnedPosition;
	}

	/**
	 * <para>Returns this object's rotation
	 * <returns>The current rotation of the object</returns>
	 * **/
	public Quaternion GetRotation() {
		return transform.rotation;
	}

}
